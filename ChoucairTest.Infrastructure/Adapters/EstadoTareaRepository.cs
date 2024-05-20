using ChoucairTest.Domain.Entities;
using ChoucairTest.Domain.Ports;
using ChoucairTest.Infrastructure.Adapters.Base;
using ChoucairTest.Infrastructure.Context;

namespace ChoucairTest.Infrastructure.Adapters;

public class EstadoTareaRepository : GenericRepository<EstadoTarea>, IEstadoTareaRepository
{
    public EstadoTareaRepository(PersistenceContext context) : base(context)
    {
    }
}
