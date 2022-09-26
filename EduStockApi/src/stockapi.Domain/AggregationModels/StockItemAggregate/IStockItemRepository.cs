using System.Threading;
using System.Threading.Tasks;
using stockapi.Domain.Contracts;


namespace stockapi.Domain.AggregationModels.StockItemAggregate;

// контракт для нашего репозитория
public interface IStockItemRepository : IRepository<StockItem>
{
    Task<StockItem> CreateAsync(StockItem stockItem, CancellationToken cancellationToken = default);
    Task<StockItem> UpdateAsync(StockItem stockItem, CancellationToken cancellationToken = default);
    Task<StockItem> FindByIdAsync(Sku sku, CancellationToken cancellationToken = default);
    Task<StockItem> FindBySkuAsync(Sku sku, CancellationToken cancellationToken = default);
    
        //Void DeleteSkuAsync(Sku sku, CancellationToken cancellationToken = default);

}