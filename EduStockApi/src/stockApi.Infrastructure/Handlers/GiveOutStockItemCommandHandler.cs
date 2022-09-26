using MediatR;
using stockapi.Domain.AggregationModels.StockItemAggregate;
using stockApi.Infrastructure.Commands.GiveOutStockItem;

namespace stockApi.Infrastructure.Handlers;

// обработчик команды - при отправке сообще в медиатр посредник понимает, комы это сообщ
public class GiveOutStockItemCommandHandler : IRequestHandler<GiveOutStockItemCommand> // передаем тип команды 
{
    private readonly IStockItemRepository _stockItemRepository;

    public GiveOutStockItemCommandHandler(IStockItemRepository stockItemRepository)
    {
        _stockItemRepository = stockItemRepository;
    }

    public async Task<Unit> Handle(GiveOutStockItemCommand request, CancellationToken cancellationToken) // отдаем товары по запросу 
    {
        // получаем ску из базы 
        var stockItem = await _stockItemRepository.FindByIdAsync(new Sku(request.Sku), cancellationToken);
        if (stockItem is null) 
            throw new Exception($"Not found with sku {request.Sku}");
        
        stockItem.DecreaseQuantity(request.Quantity);
        await _stockItemRepository.UpdateAsync(stockItem, cancellationToken);
        await _stockItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Unit.Value;
    }
}