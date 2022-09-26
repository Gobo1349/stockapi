using MediatR;
using stockapi.Domain.AggregationModels.StockItemAggregate;

namespace stockapi.Domain.Events;

// доменное событие 
public class ReachMinimumDomainEvent : INotification
{
    // что должны передать в сообщении 
    public ReachMinimumDomainEvent(Sku stockItemSku)
    {
        StockItemSku = stockItemSku;
    }

    public Sku StockItemSku { get;  } // у этого товара достигнут минимум 
}