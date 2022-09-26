using stockapi.Domain.Models;

namespace stockapi.Domain.AggregationModels.StockItemAggregate;

public class Item : Entity // оборачиваем ItemType в Item для упрощения добавления новых типов 
{
    public ItemType Type { get; }

    public Item(ItemType type)
    {
        Type = type;
    }
}