using BlazorApp;
using BlazorApp.Data;
using BlazorApp.Endpoints;
using BlazorApp.Models;
using BlazorApp.Repositories;
using BlazorApp.Services;
using BlazorApp.Extensions;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Data.Sqlite;
using MiniValidation;
using SKit.LiqPay.SDK;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(logging => {
    logging.LoggingFields = 
        HttpLoggingFields.RequestMethod | HttpLoggingFields.RequestPath | 
        HttpLoggingFields.RequestQuery | HttpLoggingFields.RequestBody | HttpLoggingFields.ResponseBody;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<WeatherForecastService>();

#region Registration Entity repositories, Order manager, CheckoutStateService, ConsoleService
var connectionString = builder.Configuration.GetConnectionString("AppDb") ?? "Data Source=app.db;Cache=Shared";
builder.Services.AddScoped(_ => new SqliteConnection(connectionString));
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<OrderManager>();
builder.Services.AddSingleton<CheckoutStateService>();
builder.Services.AddSingleton<ConsoleService>();
#endregion

#region Registration LiqPay services
builder.Services.AddLiqPay(builder.Configuration);
var liqPayCallbackOptions = new LiqPayCallbackOptions();
builder.Configuration.GetSection(LiqPayCallbackOptions.LiqPayCallbackSection).Bind(liqPayCallbackOptions);
builder.Services.AddSingleton<LiqPayCallbackOptions>(liqPayCallbackOptions);
#endregion

builder.Services.AddBootstrapBlazor();

#region CORS
var corsPolicyName = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName,
        policy =>
        {
            policy
                .WithOrigins(new string[] {
                    (new Uri(LiqPayConsts.DefaultBaseApiUrl)).Origin(),
                    "http://infdev.pp.ua:5195"
                })
                .WithMethods("POST", "GET");
                 //.AllowAnyOrigin()
                 //.AllowAnyMethod()
                 //.AllowAnyHeader()
                 //.AllowCredentials());
});
});
#endregion

var app = builder.Build();

// Create database if it doesn't exist
await EnsureDb(app.Services, app.Logger);

app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.MapGet("/error", () => Results.Problem("An error occurred.", statusCode: 500))
   .ExcludeFromDescription();

app.MapSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseRouting();
// The call to app.UseCors() must go between app.UseRouting() and app.UseEndpoints(...)
app.UseCors();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

#region Misc endpoints
app.MapGet("/misc/uri", (HttpRequest request) => request.GetDisplayUrl())
    .WithName("ReturnCurrentURI").WithTags("Misc");

app.MapGet("/misc/uri/json", (HttpRequest request) =>
{
    var displayUrl = request.GetDisplayUrl();
    var uri = new Uri(displayUrl);


    var obj = new Dictionary<string, object>
    {
        { "request", new Dictionary<string, object> {
                { "Scheme", request.Scheme },
                { "Host", request.Host },
                { "PathBase", request.PathBase },
                { "Path", request.Path },
                { "QueryString", request.QueryString },
                { "DisplayUrl", displayUrl }

            }
        },
        { "uri", new Dictionary<string, object> {
                { "Scheme", uri.Scheme },
                { "UserInfo", uri.UserInfo },
                { "Host", uri.Host },
                { "Authority", uri.Authority },
                { "LocalPath", uri.LocalPath },
                { "Segments", uri.Segments },
                { "Query", uri.Query },
                { "Fragment", uri.Fragment },
                { "PathAndQuery", uri.PathAndQuery },
                { "AbsolutePath", uri.AbsolutePath },
                { "AbsoluteUri", uri.AbsoluteUri },
                { "uri", uri }
            }
        },
    };
    return Results.Ok(obj);
})
    .WithName("ReturnPartsOfCurrentURI").WithTags("Misc");
#endregion

#region Order endpoints
app.MapGet("/api/order", async (IOrderRepository repo) => await repo.GetAllAsync())
    .WithName("GetOrderAll").WithTags("Orders");

app.MapGet("/api/order/{id}", async (int id, IOrderRepository repo) =>
    {
        var order = await repo.GetAsync(id);
        return (order != null) ? Results.Ok(order) : Results.NotFound();
    })
    .WithName("GetOrderById").WithTags("Orders")
    .Produces(StatusCodes.Status404NotFound);

app.MapPost("/api/order", async (Order order, IOrderRepository repo) => { 
        if (!MiniValidator.TryValidate(order, out var errors))
            return Results.ValidationProblem(errors);
        var newOrder = await repo.InsertAsync(order);
        return Results.Ok(newOrder);
    })
    .WithName("CreateOrder").WithTags("Orders")
    .ProducesValidationProblem()
    .Produces<Order>(StatusCodes.Status201Created);

app.MapPut("/api/order", async (Order order, IOrderRepository repo) =>
    {
        if (!MiniValidator.TryValidate(order, out var errors))
            return Results.ValidationProblem(errors);

        return await repo.UpdateAsync(order) ? Results.NoContent() : Results.NotFound();
    })
    .WithName("UpdateOrder").WithTags("Orders")
    .ProducesValidationProblem()
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound);

app.MapDelete("/api/order/{id}", async (int id, IOrderRepository repo) =>
    await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound())
    .WithName("DeleteOrder").WithTags("Orders")
    .Produces(StatusCodes.Status204NoContent);

#endregion

#region OrderItem endpoints
app.MapGet("/api/order-item", async (IOrderItemRepository repo) => await repo.GetAllAsync())
    .WithName("GetOrderItemsAll").WithTags("Order items");

app.MapGet("/api/order-item/{id}", async (int id, IOrderItemRepository repo) =>
{
    var orderItem = await repo.GetAsync(id);
    return (orderItem != null) ? Results.Ok(orderItem) : Results.NotFound();
})
    .WithName("GetOrderItemById").WithTags("Order items")
    .Produces(StatusCodes.Status404NotFound);

app.MapGet("/api/order-item/order/{orderid}", async (int orderid, IOrderItemRepository repo) =>
{
    var orderItems = await repo.GetOrderItemsAsync(orderid);
    return orderItems;
})
    .WithName("GetOrderItemsOfOrder").WithTags("Order items");

app.MapPost("/api/order-item", async (OrderItem orderItem, IOrderItemRepository repo) => {
    if (!MiniValidator.TryValidate(orderItem, out var errors))
        return Results.ValidationProblem(errors);
    var newOrderItem = await repo.InsertAsync(orderItem);
    return Results.Ok(newOrderItem);
})
    .WithName("CreateOrderItem").WithTags("Order items")
    .ProducesValidationProblem()
    .Produces<OrderItem>(StatusCodes.Status201Created);

app.MapPut("/api/order-item", async (OrderItem orderItem, IOrderItemRepository repo) =>
{
    if (!MiniValidator.TryValidate(orderItem, out var errors))
        return Results.ValidationProblem(errors);
    var result = await repo.UpdateAsync(orderItem);
    return  result ? Results.NoContent() : Results.NotFound();
})
    .WithName("UpdateOrderItem").WithTags("Order items")
    .ProducesValidationProblem()
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound);

app.MapDelete("/api/order-item/{id}", async (int id, IOrderItemRepository repo) =>
    await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound())
    .WithName("DeleteOrderItem").WithTags("Order items")
    .Produces(StatusCodes.Status204NoContent);
#endregion

#region LiqPay Payment endpoints
app.MapPost("payments/liqpay/pdt", async (LiqPayPackCallback pack, HttpContext context, 
        ILiqPayPdtListener listener, OrderManager orderManager, ConsoleService consoleService) =>
    await Endpoints.PostLiqPayPdtHandler(pack, context, listener, orderManager, consoleService))
    .WithName("ReceivePdtFromLiqPay").WithTags("Payments LiqPay")
    .RequireCors(corsPolicyName);

app.MapPost("payments/liqpay/ipn", async (LiqPayPackCallback pack, HttpContext context, 
        ILiqPayIpnListener listener, OrderManager orderManager, ConsoleService consoleService) =>
        await Endpoints.PostLiqPayIpnHandler(pack, context, listener, orderManager, consoleService))
        .WithName("ReceiveIpnFromLiqPay").WithTags("Payments LiqPay")
        .RequireCors(corsPolicyName);
#endregion

app.Run();

// Create database if it doesn't exist
async Task EnsureDb(IServiceProvider services, ILogger logger)
{
    logger.LogInformation("Ensuring database exists at connection string '{connectionString}'", connectionString);

    using var db = services.CreateScope().ServiceProvider.GetRequiredService<SqliteConnection>();

    var sql = $@"CREATE TABLE IF NOT EXISTS Orders (
                  {nameof(Order.OrderId)} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                  {nameof(Order.BuyerEmail)} TEXT,
                  {nameof(Order.BuyerPhone)} TEXT,
                  {nameof(Order.PaymentStatus)} INTEGER DEFAULT 0 NOT NULL,
                  {nameof(Order.LiqPayPaymentStatus)} TEXT,
                  {nameof(Order.CreateAt)} TEXT NOT NULL,
                  {nameof(Order.ModifyAt)} TEXT NOT NULL
                 );

                CREATE TABLE IF NOT EXISTS OrderItems (
                  {nameof(OrderItem.OrderItemId)} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                  {nameof(OrderItem.OrderId)} INTEGER,
                  {nameof(OrderItem.Name)} TEXT NOT NULL,
                  {nameof(OrderItem.Quantity)} INTEGER DEFAULT 0 NOT NULL,
                  {nameof(OrderItem.Price)} REAL DEFAULT 0 NOT NULL,

                  CONSTRAINT fk_orders
                    FOREIGN KEY ({nameof(OrderItem.OrderId)}) 
                    REFERENCES Orders ({nameof(Order.OrderId)})
                    ON DELETE CASCADE
            );";
    await db.ExecuteAsync(sql);

    // Clear old orders
    var maxId = Convert.ToInt32(await db.ExecuteScalarAsync("SELECT MAX(OrderId) FROM Orders"));
    if (maxId > 20)
        maxId -= 20;
    await db.ExecuteScalarAsync($"DELETE FROM Orders WHERE OrderId < {maxId};");
}


// Make the implicit Program class public so test projects can access it
public partial class Program { }