using ChoucairTest.Application.Tareas.Dtos;
using MediatR;
using Dapper;
using System.Data;
using ChoucairTest.Application.Helpers;
using ChoucairTest.Domain.Ports;
using ChoucairTest.Domain.Entities;

namespace ChoucairTest.Application.Tareas.Consultas;

public class AllQueryHandler : IRequestHandler<AllQuery, List<TareaGridDto>>
{
    private readonly IDbConnection _dapperSource;
    private readonly ILoginMiddleware _loginMiddleware;

    public AllQueryHandler(IDbConnection dapperSource, ILoginMiddleware loginMiddleware)
    {
        _dapperSource = dapperSource;
        _loginMiddleware = loginMiddleware;
    }

    public async Task<List<TareaGridDto>> Handle(AllQuery request, CancellationToken cancellationToken)
    {
        var userId = _loginMiddleware.GetIdUserLoggerd();

        var result = await _dapperSource.QueryAsync<TareaGridDto>(
            DbItems.StoredProcedures.CONSULTA_TAREAS,
            new
            {
                UsuarioId = userId,
            },
            commandType: CommandType.StoredProcedure);

        return result.ToList();
    }
}