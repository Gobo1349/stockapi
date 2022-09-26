using stockapi.Domain.Models;

namespace stockapi.Domain.AggregationModels.StockItemAggregate;

public class ClothingSize : Enumeration
{
    public static ClothingSize S = new(1, nameof(S));
    public static ClothingSize L = new(2, nameof(L));
    public static ClothingSize M = new(3, nameof(M));
   // public static ClothingSize None = new(3, nameof(None));


    public ClothingSize(int id, string name) : base(id, name)
    {
        
    }
}