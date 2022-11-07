using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using stockapi.Domain.AggregationModels.StockItemAggregate;
using stockapi.Domain.Contracts;
using stockApi.Infrastructure.Handlers;

namespace stockApi.Infrastructure.Extensions;

// для регистрации методов в стартапе - регистрация сваггера - ???
// добавляем в проект медиатор 
public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service)
    {
        service.AddMediatR(typeof(CreateStockItemCommandHandler).Assembly); // смотрим всю сборку 
        service.AddScoped<IStockItemRepository, Repo>();
        return service;
    }
}

public class Repo : IStockItemRepository
{
    public IUnitOfWork UnitOfWork { get; }
    public Task<StockItem> CreateAsync(StockItem stockItem, CancellationToken cancellationToken = default)
    {
        throw new Exception($"CreateAsync");
       // throw new System.NotImplementedException();
    }

    public Task<StockItem> UpdateAsync(StockItem stockItem, CancellationToken cancellationToken = default)
    {
        throw new Exception($"UpdateAsync");
       // throw new System.NotImplementedException();
    }

    public Task<StockItem> FindByIdAsync(Sku sku, CancellationToken cancellationToken = default)
    {
        throw new Exception($"FindByIdAsync");
       // throw new System.NotImplementedException();
    }
    
    public Task<StockItem> FindBySkuAsync(Sku sku, CancellationToken cancellationToken = default)
    {
        throw new Exception($"FindBySkuAsync");
        // throw new System.NotImplementedException();
    }

    /*public void DeleteSkuAsync(Sku sku, CancellationToken cancellationToken = default)
    {
        //throw new Exception($"1111111");
        throw new System.NotImplementedException();
    }*/
}