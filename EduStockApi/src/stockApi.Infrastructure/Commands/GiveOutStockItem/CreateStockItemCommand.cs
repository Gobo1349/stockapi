using MediatR;
using stockapi.Domain.AggregationModels.StockItemAggregate;

namespace stockApi.Infrastructure.Commands.GiveOutStockItem;

public class CreateStockItemCommand : IRequest<int> // потому что будем возвращать int 
{
    public long Sku { get; init; }
    public string Name  { get; init; }
    public ItemType StockItemType  { get; init; }
    public ClothingSize ClothingSize { get; init; }
    public int Quantity  { get; init; }
    public int MinimalQuantity  { get; init; }
    public string Tags { get; init; }
}