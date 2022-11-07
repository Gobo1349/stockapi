using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using stockapi.Domain.AggregationModels.StockItemAggregate;
using stockapi.HttpModels;
using stockApi.Infrastructure.Commands.GiveOutStockItem;
using stockapi.Services;
using StockItem = stockapi.Services.StockItem;

namespace stockapi.Controllers;

// связь логики методов и http запросов - отображается в сваггере - сами методы в StockService
[ApiController]
[Route("v1/api/stocks")]
public class StockController : ControllerBase // за обработку HTTP запроса отвечают контроллеры - классы
{
    private readonly IStockService _stockService;
    private readonly IMediator _mediator;

    public StockController(IStockService stockService, IMediator mediator)
    {
        _stockService = stockService;
        _mediator = mediator;
    }
    
    /// <summary>
    /// Вывести все товары
    /// </summary>
    [HttpGet] // какой тип запроса будет приходить 
    public async Task<ActionResult<List<StockItem>>> GetAll(CancellationToken token) // IActionResult - описывает результат с кодом,
    // ActionResult - чтобы в сваггере увидеть то, что нам может вернуться (список полей)
    {
        var stockitems = await _stockService.GetAll(token);
        return Ok(stockitems);
    }
    
    /// <summary>
    /// Получить товар по ID
    /// </summary>
    [HttpGet("{id:long}")] // какой тип запроса будет приходить 
    public async Task<ActionResult<StockItem>> GetById(long id, CancellationToken token)
    {
        var stockitem = await _stockService.GetById(id, token);
        if (stockitem is null)
        {
            return NotFound();
        }

        return stockitem;
    }

    /*// метод добавления 
    [HttpPost]
    public async Task<ActionResult<StockItem>> Add(StockItemModel model, CancellationToken token)
    {
       // throw new CustomException();
        // PL знает о бизнесе, но не наоборот 
        var createdStockItem = await _stockService.Add(new StockItemCreationModel
        {
            ItemName = model.ItemName,
            Quantity = model.Quantity
        }, token);
        return Ok(createdStockItem);
    }*/
    
    /// <summary>
    /// Добавить новый товар 
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<int>> Add(StockItemModel model, CancellationToken token)
    {
        var createStockItemCommand = new CreateStockItemCommand() // создаем объект команды
        {
            Sku = model.Sku,
            Name = model.Name
            // и другие поля
        };
        var result = await _mediator.Send(createStockItemCommand, token); // отправляем команду через медиатор, он смотрит хендлер, привязанный к этой команде 
        return Ok(result);
    }
    
    /// <summary>
    /// Запросить мерч
    /// </summary>
    [HttpPost]
    [Route("/Demand")]
    public async void StockItemDemand(StockItemDemandModel model, CancellationToken token) // void - пока не понятно, что надо возвращать на запрос мерча
    {
        var domainService = new DomainService()
        {

        };
        // throw new NotImplementedException();
        //DomainServiceStockItemDemand();
        domainService.DomainServiceStockItemDemand(model, token);
    }
    
    /// <summary>
    /// Получить информацию о выдаче мерча
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet] 
    [Route("/GetInfo")]
    public async Task<ActionResult<List<StockItemDeliveryModel>>> GetInfo(CancellationToken token) 
    {
        throw new NotImplementedException();
        /*var stockitems = await _stockService.GetAll(token);
        return Ok(stockitems);*/
    }
    
    public class CustomException : Exception
    {
        public CustomException() : base("some custom exception")
        {
            
        }
    }
}


