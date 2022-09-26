using stockapi.Domain.Models;
using stockapi.Domain.AggregationModels;

namespace stockapi.Domain.Contracts;

public interface IRepository<TAggregationRoot>
{
      IUnitOfWork UnitOfWork { get; }
}