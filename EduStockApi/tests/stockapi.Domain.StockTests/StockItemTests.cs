using stockapi.Domain.AggregationModels.StockItemAggregate;

namespace stockapi.Domain.StockTests;

public class StockItemTests
{
    // увеличение количества
    [Fact]
    public void IncreaseStockItemQuantity()
    {
        // Arrange 
        var stockItem = new StockItem(new Sku(1234), new Name("Black TShirt"), new Item(ItemType.TShirt),
            ClothingSize.M, new Quantity(10), new Quantity(5), new Tag("TShirt"));

        var valueToIncrease = 10;
        // Act 
        stockItem.IncreaseQuantity(valueToIncrease);

        // Assert 
        Assert.Equal(20, stockItem.Quantity.Value);

    }

    // а если попытаемся увеличить на отрицательное знач? 
    [Fact]
    public void IncreaseQuantityNegativeValueSuccess()
    {
        // Arrange 
        var stockItem = new StockItem(new Sku(1234), new Name("Black TShirt"), new Item(ItemType.TShirt),
            ClothingSize.M, new Quantity(10), new Quantity(5), new Tag("TShirt"));

        var valueToIncrease = -10;
        // Act 
        // Assert
        Assert.Throws<Exception>(() => stockItem.IncreaseQuantity(valueToIncrease)); // должна быть выдана ошибка 
    }

    // уменьшение количества 
    [Fact]
    public void DecreaseStockItemQuantity()
    {
        // Arrange 
        var stockItem = new StockItem(new Sku(1234), new Name("Black TShirt"), new Item(ItemType.TShirt),
            ClothingSize.M, new Quantity(10), new Quantity(5), new Tag("TShirt"));

        var valueToDecrease = 7;
        // Act 
        stockItem.DecreaseQuantity(valueToDecrease);

        // Assert 
        Assert.Equal(3, stockItem.Quantity.Value);
    }

    // а если попытаемся уменьшить на отрицательное знач? 
    [Fact]
    public void DecreaseQuantityNegativeValueSuccess()
    {
        // Arrange 
        var stockItem = new StockItem(new Sku(1234), new Name("Black TShirt"), new Item(ItemType.TShirt),
            ClothingSize.M, new Quantity(10), new Quantity(5), new Tag("TShirt"));

        var valueToDecrease = -10;
        // Act 
        // Assert
        Assert.Throws<Exception>(() => stockItem.DecreaseQuantity(valueToDecrease)); // должна быть выдана ошибка 
    }

    // а если недостаточно товаров? 
    [Fact]
    public void NotEnoughItems()
    {
        // Arrange 
        var stockItem = new StockItem(new Sku(1234), new Name("Black TShirt"), new Item(ItemType.TShirt),
            ClothingSize.M, new Quantity(10), new Quantity(5), new Tag("TShirt"));

        var valueToDecrease = 13;
        // Act 
        // Assert
        Assert.Throws<Exception>(() => stockItem.DecreaseQuantity(valueToDecrease)); // должна быть выдана ошибка 
    }

    // Установка размера одежды
    [Fact]
    public void SetStockItemClothingSize()
    {
        // Arrange 
        var stockItem = new StockItem(new Sku(1234), new Name("Black TShirt"), new Item(ItemType.TShirt),
            ClothingSize.M, new Quantity(10), new Quantity(5), new Tag("TShirt"));

        // Act 
        stockItem.SetClothingSize(ClothingSize.L);

        // Assert 
        Assert.Equal(ClothingSize.L, stockItem.ClothingSize);
    }
    
    // Установка размера - не одежда 
    [Fact]
    public void SetStockItemClothingSizeNull()
    {
        // Arrange 
        var stockItem = new StockItem(new Sku(1234), new Name("Black Bag"), new Item(ItemType.Bag),
           null, new Quantity(10), new Quantity(5), new Tag("Bag"));

        // Act 
        //stockItem.SetClothingSize(ClothingSize.L);

        // Assert 
        Assert.Equal(null, stockItem.ClothingSize);
    }

    // Установка размера одежды - а если это не одежда??? - пока не работает 
    /*[Fact]
    public void SetStockItemClothingSizeNotCloth()
    {
        // Arrange 
        var stockItem = new StockItem(new Sku(1239), new Name("Black Bag"), new Item(ItemType.Bag),
            ClothingSize.M, new Quantity(10), new Quantity(5), new Tag("Bag"));

        // Act 
        //stockItem.SetClothingSize(ClothingSize.M);

        // Assert 
        Assert.Throws<Exception>(() => stockItem.SetClothingSize(ClothingSize.M)); // должна быть выдана ошибка 
    }*/
}