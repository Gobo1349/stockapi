namespace stockapi.HttpModels;

// класс - сущность для метода добавления/ Модель для данных, поступающих извне 
public class StockItemModel
{
    public string ItemName { get; set; }
    public int Quantity { get; set; }
}