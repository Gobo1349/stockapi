using Grpc.Net.Client;
using stockapi.Grpc;

// устанавливаем данные для GRPC, канал, клиент и один метод 
var channel = GrpcChannel.ForAddress("https://localhost:5001"); // канал, по которому клиент обращается к нашему API 
var client = new StockApiGrpc.StockApiGrpcClient(channel); // клиент 

var responce = await client.GetAllStockItemsAsync(new GetAllStockItemsRequest(), cancellationToken: CancellationToken.None); // вызов метода 
foreach (var item in responce.Stocks)
{
    Console.WriteLine($"item id {item.ItemId} - quantity {item.Quantity}");
}