using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace stockapi.Services;

// DI механизм 
// некоторые модели и методы для бизнес данных 
public interface IStockService
{
    Task<List<StockItem>> GetAll(CancellationToken token);
    Task<StockItem> GetById(long itemId, CancellationToken _);
    Task<StockItem> Add(StockItemCreationModel stockItem, CancellationToken _);

}

// класс - сущность для метода добавления/ Модель для бизнес данных
public class StockItemCreationModel
{
    public string ItemName { get; set; }
    public int Quantity { get; set; }
}

public class StockService : IStockService
{
    // возвращаем список товаров
    private readonly List<StockItem> StockItems = new List<StockItem>
    {
        new StockItem(1, "Футболка", 10),
        new StockItem(1, "Толстовка", 15),
        new StockItem(1, "Кепка", 20)
    };
    
    // метод возвращения списка 
    public Task<List<StockItem>> GetAll(CancellationToken _) => Task.FromResult(StockItems);

    // метод поиска товара по ID 
    public Task<StockItem> GetById(long itemId, CancellationToken _)
    {
        var stockItem = StockItems.FirstOrDefault(x => x.ItemID == itemId);
        return Task.FromResult(stockItem);
    }

    public Task<StockItem> Add(StockItemCreationModel stockItem, CancellationToken _)
    {
        var ItemID = StockItems.Max(x => x.ItemID) + 1; // определяем ID как на 1 больше максимального 
        var newStockItem = new StockItem(ItemID, stockItem.ItemName, stockItem.Quantity);
        StockItems.Add(newStockItem);
        return Task.FromResult(newStockItem); //???
    }
}

public class StockItem
{
    public StockItem(long itemId, string itemName, int quantity)
    {
        ItemID = itemId;
        ItemName = itemName;
        Quantity = quantity;
    }
    public long ItemID { get; }
    public string ItemName { get; }
    public int Quantity { get; }
}