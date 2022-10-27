using System.Reflection;

namespace SKit.LiqPay.SDK;

/// <summary>
/// A base class for an enumeration that allows advanced processing
/// </summary>
/// <remarks>Базовий клас для перерахування, що допускає розширену обробку</remarks>
public abstract class Enumeration : IComparable
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    protected Enumeration(int id, string name) => (Id, Name) = (id, name);

    /// <summary>
    /// Name of enum item
    /// </summary>
    /// <remarks>Назва елемента перерахування</remarks>
    public string Name { get; private set; }

    /// <summary>
    /// Id of enum item
    /// </summary>
    public int Id { get; private set; }

    /// <inheritdoc/>
    public override string ToString() => Name;

    /// <summary>
    /// Getting all list names
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <remarks>Отримання всіх імен перерахування</remarks>
    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                    .Select(f => f.GetValue(null))
                    .Cast<T>();

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    /// <inheritdoc/>
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// Absolute difference between enum element IDs
    /// </summary>
    /// <param name="firstValue"></param>
    /// <param name="secondValue"></param>
    /// <returns></returns>
    /// <remarks>Абсолютна різниця між ідентифікаторами елементів перерахування</remarks>
    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
        var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
        return absoluteDifference;
    }

    /// <summary>
    /// Parser of an element from an integer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Парсер елемента з цілого</remarks>
    public static T FromValue<T>(int value) where T : Enumeration
    {
        var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
        return matchingItem;
    }

    /// <summary>
    /// Parser of an element from the string named
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <remarks>Парсер елемента зі строки з іменем</remarks>
    public static T FromName<T>(string name) where T : Enumeration
    {
        var matchingItem = Parse<T, string>(name, "name", item => item.Name == name);
        return matchingItem;
    }

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
            throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

        return matchingItem;
    }

    /// <inheritdoc/>
    public int CompareTo(object? other) => Id.CompareTo(((Enumeration?)other)?.Id ?? int.MinValue);
}
