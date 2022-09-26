using stockapi.Domain.Models;

namespace stockapi.Domain.AggregationModels.StockItemAggregate;

public class ItemType : Enumeration   // отдельная табл - проще добавлять новый тип - лучше расширение 
{
    public static ItemType TShirt = new(1, nameof(TShirt));
    public static ItemType Bag = new(2, nameof(Bag));
    public static ItemType SweatShirt = new(3, nameof(SweatShirt));
    public static ItemType Pen = new(4, nameof(Pen));
    public static ItemType Notepad = new(5, nameof(Notepad));

    public ItemType(int id, string name) : base(id, name)
    {
        
    }
}