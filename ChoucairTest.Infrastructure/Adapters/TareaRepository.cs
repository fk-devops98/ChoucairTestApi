using ChoucairTest.Domain.Entities;
using ChoucairTest.Domain.Ports;
using ChoucairTest.Infrastructure.Adapters.Base;
using ChoucairTest.Infrastructure.Context;

namespace ChoucairTest.Infrastructure.Adapters;

public class TareaRepository : GenericRepository<Tarea>, ITareaRepository
{
    public TareaRepository(PersistenceContext context) : base(context)
    {
    }
}
