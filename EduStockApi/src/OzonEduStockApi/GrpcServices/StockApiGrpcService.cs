using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using stockapi.Grpc;
using stockapi.Services;

namespace stockapi.GrpcServices;
// GRPC метод
public class StockApiGrpcService : StockApiGrpc.StockApiGrpcBase // из proto файла 
{
    private readonly IStockService _stockService;

    public StockApiGrpcService(IStockService stockService)
    {
        _stockService = stockService;
    }
    
    // в отличие от контроллеров - можем оверрайдить методы 
    public override async Task<GetAllStockItemsResponce> GetAllStockItems(GetAllStockItemsRequest request, ServerCallContext context)
    {
        var stockItems = await _stockService.GetAll(context.CancellationToken); // получаем наши items 
        return new GetAllStockItemsResponce()
        {
            Stocks = { stockItems.Select(x => new GetAllStockItemsResponceUnit
            {
                ItemId =x.ItemID,
                ItemName = x.ItemName,
                Quantity = x.Quantity
            })}
        };
    }

    /*public override async Task<StockItemDemandResponce> StockItemDemand(StockItemDemandRequest request, ServerCallContext context)
    {
        
    }
    
    public override async Task<GetInfoResponce> GetInfo(GetInfoRequest request, ServerCallContext context)
    {
        var stockDemandItems = await _stockService.GetAll(context.CancellationToken); // получаем наши items 
        return new GetAllStockItemsResponce()
        {
            Stocks = { stockDemandItems.Select(x => new GetAllStockItemsResponceUnit
            {
                ItemId =x.ItemID,
                ItemName = x.ItemName,
                Quantity = x.Quantity
            })}
        };
    }*/
}
