namespace stockapi.HttpModels
{
    // класс - сущность для метода добавления/ Модель для данных, поступающих извне 
    public class StockItemModel
{
    public long Sku { get; init; }
    public string Name  { get; init; }
    public int StockItemType  { get; init; }
    public int ClothingSize { get; init; }
    public int Quantity  { get; init; }
    public int MinimalQuantity  { get; init; }
    public string Tags { get; init; }
}}

public class StockItemDemandModel // модель для запроса мерча 
{
    public string ItemName { get; set; }
    public int Quantity { get; set; }
}

public class StockItemDeliveryModel // модель для ответа на запрос мерча 
{
    public long ItemID { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; } // когда был выдан мерч
}

