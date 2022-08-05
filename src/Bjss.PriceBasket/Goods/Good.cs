using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Bjss.PriceBasket.Goods;

[ExcludeFromCodeCoverage]
public class Good
{
    public const string Apples = "Apples";
    public const string Soup = "Soup";
    public const string Bread = "Bread";
    public const string Milk = "Milk";

    private static List<string>? _goods;

    public static bool Exists(string good)
    {
        return GetValues().Contains(good);
    }

    private static IEnumerable<string> GetValues()
    {
        if (_goods != null && _goods.Any())
        {
            return _goods;
        }

        _goods = typeof(Good)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(x => (string)x.GetRawConstantValue()!)
            .ToList();

        return _goods;
    }
}
