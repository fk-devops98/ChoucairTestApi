using ChoucairTest.Application.Tareas.Dtos;
using MediatR;
using Dapper;
using System.Data;
using ChoucairTest.Application.Helpers;

namespace ChoucairTest.Application.Tareas.Consultas;

public class OneQueryHandler : IRequestHandler<OneQuery, TareaDto>
{
    private readonly IDbConnection _dapperSource;

    public OneQueryHandler(IDbConnection dapperSource)
    {
        _dapperSource = dapperSource;
    }

    public async Task<TareaDto> Handle(OneQuery request, CancellationToken cancellationToken)
    {
        var result = await _dapperSource.QueryFirstOrDefaultAsync<TareaDto>(
            DbItems.StoredProcedures.CONSULTA_TAREA_POR_ID,
            request,
            commandType: CommandType.StoredProcedure);

        return result;
    }
}