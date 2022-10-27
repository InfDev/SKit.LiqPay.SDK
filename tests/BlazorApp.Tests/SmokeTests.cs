using BlazorApp.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Http;
using System.Text.Json;
using FluentAssertions;
using BlazorApp.Tests;

namespace SKit.LiqPay.SDK.Tests
{
    public class SmokeTests //: IClassFixture<PlaygroundApplication>
    {
        //private readonly PlaygroundApplication application;

        //public SmokeTests(PlaygroundApplication app)
        //{
        //    application = app;
        //}
        
        [Fact]
        public async Task GetSwaggerDoc_Ok()
        {
            await using var application = new PlaygroundApplication();
            var client = application.CreateClient();

            var response = await client.GetAsync("/swagger/v1/swagger.json");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task OrderRestApi()
        {
            await using var application = new PlaygroundApplication();
            var client = application.CreateClient();

            HttpResponseMessage? response = null;

            // Insert
            var order = TestHelper.NewOrder();
            response = await client.PostAsJsonAsync("/api/order", order);
            response.EnsureSuccessStatusCode();
            var currentOrder = await response.Content.ReadFromJsonAsync<Order>();

            // Update
            var newPhone = TestHelper.NewOrder().BuyerPhone;
            currentOrder!.BuyerPhone = newPhone;
            response = await client.PutAsJsonAsync("/api/order", currentOrder);
            response.EnsureSuccessStatusCode();
            // Get by id
            response = await client.GetAsync($"/api/order/{currentOrder.OrderId}");
            response.EnsureSuccessStatusCode();
            currentOrder = await response.Content.ReadFromJsonAsync<Order>();
            currentOrder!.BuyerPhone.Should().Be(newPhone);

            // Get all
            response = await client.GetAsync("/api/order");
            response.EnsureSuccessStatusCode();
            var orders = await response.Content.ReadFromJsonAsync<Order[]>();
            orders!.Should().HaveCountGreaterThan(0);

            // Delete
            response = await client.DeleteAsync($"/api/order/{currentOrder.OrderId}");
            response.EnsureSuccessStatusCode();
            // Get by id
            response = await client.GetAsync($"/api/order/{currentOrder.OrderId}");

            Action mustBeException = () => response.EnsureSuccessStatusCode();
            mustBeException.Should().Throw<Exception>();
        }

        [Fact]
        public async Task OrderItemRestApi()
        {
            await using var application = new PlaygroundApplication();
            var client = application.CreateClient();

            HttpResponseMessage? response = null;

            // Insert order
            var order = TestHelper.NewOrder();
            response = await client.PostAsJsonAsync("/api/order", order);
            response.EnsureSuccessStatusCode();
            var currentOrder = await response.Content.ReadFromJsonAsync<Order>();

            // Insert order item
            var orderItem = TestHelper.NewOrderItem();
            orderItem.OrderId = currentOrder!.OrderId;
            response = await client.PostAsJsonAsync("/api/order-item", orderItem);
            response.EnsureSuccessStatusCode();
            var currentOrderItem = await response.Content.ReadFromJsonAsync<OrderItem>();

            // Update order item
            var newName = "USB";
            currentOrderItem!.Name = newName;
            response = await client.PutAsJsonAsync("/api/order-item", currentOrderItem);
            response.EnsureSuccessStatusCode();
            // Get by id
            response = await client.GetAsync($"/api/order-item/{currentOrderItem.OrderItemId}");
            response.EnsureSuccessStatusCode();
            currentOrderItem = await response.Content.ReadFromJsonAsync<OrderItem>();
            currentOrderItem!.Name.Should().Be(newName);

            // Get items of order
            response = await client.GetAsync($"/api/order-item/order/{currentOrder.OrderId}");
            response.EnsureSuccessStatusCode();
            var orderItems = await response.Content.ReadFromJsonAsync<OrderItem[]>();
            orderItems!.Should().HaveCountGreaterThan(0);

            // Delete order item
            response = await client.DeleteAsync($"/api/order-item/{currentOrderItem.OrderItemId}");
            response.EnsureSuccessStatusCode();
            // Get by id
            response = await client.GetAsync($"/api/order-item/{currentOrderItem.OrderItemId}");

            Action mustBeException = () => response.EnsureSuccessStatusCode();
            mustBeException.Should().Throw<Exception>();

            // Delete order with items
            orderItem = TestHelper.NewOrderItem();
            orderItem.OrderId = currentOrder!.OrderId;
            response = await client.PostAsJsonAsync("/api/order-item", orderItem);
            response.EnsureSuccessStatusCode();
            response = await client.DeleteAsync($"/api/order/{currentOrder.OrderId}");
            response.EnsureSuccessStatusCode();
            // Get by id
            response = await client.GetAsync($"/api/order/{currentOrder.OrderId}");
            mustBeException.Should().Throw<Exception>();

        }
    }

}
