using stockapi.Domain.AggregationModels.StockItemAggregate;

namespace stockapi.Domain.StockTests;

public class QuantityValueObjectTests
{
    // создаем класс
    [Fact]
    public void CreateEntityInstanceSuccess()
    {
        // Arrange 
        var quantity = 10;
        var minimalQuantity = 2;
        
        // Act 
        var result = new Quantity(quantity, minimalQuantity);
        
        // Assert - проверка 
        Assert.Equal(quantity, result.Value);
    }
    
    // второй тест - без минимального значения 
    [Fact]
    public void CreateEntityInstanceWhithoutMinimalSuccess()
    {
        // Arrange 
        var quantity = 10;
        
        // Act 
        var result = new Quantity(quantity);
        
        // Assert - проверка 
        Assert.Equal(quantity, result.Value);
    }
}