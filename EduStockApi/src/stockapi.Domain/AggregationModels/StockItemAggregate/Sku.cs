using stockapi.Domain.Models;

namespace stockapi.Domain.AggregationModels.StockItemAggregate;

public class Sku : ValueObject
{
    public long Value { get; }

    public Sku(long sku)
    {
        Value = sku;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}