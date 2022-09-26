using stockapi.Domain.Models;

namespace stockapi.Domain.AggregationModels.StockItemAggregate;

public class Quantity : ValueObject
{
    public Quantity(int value, int minimalQuantity)
    {
        Value = value;
     //   MinimalQuantity = minimalQuantity; // если меньше - то событие, что нужно пополнять стоки 
    }
    
    // второй конструктор - для того чтобы не указывать minimalQuantity 
    public Quantity(int value)
    {
        Value = value;
     //   MinimalQuantity = 1;
    }
    public int Value { get; }
    /*public int MinimalQuantity { get; }

    public void Increase(int valueToIncrease) // увеличиваем количество 
    {
        if (valueToIncrease < 0)
            throw new Exception($"{nameof(valueToIncrease)} is negative");
        Value += valueToIncrease;
    }*/
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    //    yield return MinimalQuantity;
    }
}