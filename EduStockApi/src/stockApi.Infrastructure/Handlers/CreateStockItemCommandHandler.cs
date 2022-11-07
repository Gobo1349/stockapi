using MediatR;
using stockapi.Domain.AggregationModels.StockItemAggregate;
using stockApi.Infrastructure.Commands.GiveOutStockItem;

namespace stockApi.Infrastructure.Handlers;

public class CreateStockItemCommandHandler : IRequestHandler<CreateStockItemCommand, int>
{
    private readonly IStockItemRepository _stockItemRepository;

    public CreateStockItemCommandHandler(IStockItemRepository stockItemRepository)
    {
        _stockItemRepository = stockItemRepository;
    }
    
    // создаем новый товар - нужно проверить, что таких нет - бизнес на уровне инфраструктуры - бизнес модель прозрачна 
    public async Task<int> Handle(CreateStockItemCommand request, CancellationToken cancellationToken)
    {
     //   var stockInDb = await _stockItemRepository.FindByIdAsync(new Sku(request.Sku), cancellationToken);
     //   if (stockInDb is not null)
     //       throw new Exception($"StockItem with sku {request.Sku} already exists");

        var newStockItem = new StockItem(
            new Sku(request.Sku),
            new Name(request.Name),
            new Item(request.StockItemType),
            request.ClothingSize,
            new Quantity(request.Quantity),
            new Quantity((int)request.MinimalQuantity), new Tag("")
        );

        var createResult = await _stockItemRepository.CreateAsync(newStockItem, cancellationToken);
        await _stockItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return newStockItem.Id;
    }
}