using stockapi.Domain.Events;
using stockapi.Domain.Models;

namespace stockapi.Domain.AggregationModels.StockItemAggregate;
// бизнес модель - доменная модель - логика 
// элемент на складе
public class StockItem : Entity
{
    public StockItem(Sku sku, Name name, Item itemType,
        ClothingSize size, Quantity quantity, Quantity minimalQuantity, Tag tag)
    {
        Sku = sku;
        Name = name;
        ItemType = itemType;
        SetClothingSize(size);
        Quantity = quantity;
        MinimalQuantity = minimalQuantity;
        Tag = tag; 
    }
    
    public Sku Sku { get; }
    public Name Name { get; }
    public Item ItemType { get; } // можно создать табличку, Entity 
    public ClothingSize ClothingSize { get; private set; } // только для одежды, для остальных null 
    public Quantity Quantity { get; private set; }
    public Quantity MinimalQuantity { get; private set; }
    public Tag Tag { get; set; }

    // увеличение количества на складе 
    public void IncreaseQuantity(int valueToIncrease)
    {
        if (valueToIncrease < 0)
            throw new Exception($"{nameof(valueToIncrease)} is negative");
        Quantity = new Quantity(this.Quantity.Value + valueToIncrease);
    }
    
    // уменьшение количества на складе 
    public void DecreaseQuantity(int valueToDecrease)
    {
        if (valueToDecrease < 0)
            throw new Exception($"{nameof(valueToDecrease)} is negative");
        if (Quantity.Value < valueToDecrease)
            throw new Exception($"Not enough items");
        Quantity = new Quantity(this.Quantity.Value - valueToDecrease);
        
        // если опустились ниже минимального - доменное событие 
           if (Quantity.Value < MinimalQuantity.Value)
            AddReachedMinimumDomainEvent(Sku);
    }
    
    // установка размера одежды 
    public void SetClothingSize(ClothingSize size)
    {
        if (size is not null && (
                ItemType.Type.Equals(StockItemAggregate.ItemType.TShirt) ||
                ItemType.Type.Equals(StockItemAggregate.ItemType.SweatShirt)))
            ClothingSize = size;
        else if (size is null)
            ClothingSize = null;
        else
        {
            throw new Exception($" cannot get size"); // если условие не выполняется - ошибка 

        }
    }
    
    // добавление доменного ивента в хранилище  ивентов для сущности (3-20 воркшоп)
    private void AddReachedMinimumDomainEvent(Sku sku)
    {
        var orderStartedDomainEvent = new ReachMinimumDomainEvent(sku); // создаем событие 
        this.AddDomainEvent(orderStartedDomainEvent); // добавляем в очередь (см класс Entity)
    }

}