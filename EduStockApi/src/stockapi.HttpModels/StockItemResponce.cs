namespace stockapi.HttpModels;

public class StockItemResponce // идентичен тому, который в бизнес слое 
{
    public long ItemID { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
}

public class StockItemDeliveryResponce
{
    public long ItemID { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; } // когда был выдан мерч
}

public class StockItemDemandRequest
{
    public string ItemName { get; set; }
    public int Quantity { get; set; }
}