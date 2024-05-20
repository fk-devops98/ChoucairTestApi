
using ChoucairTest.Domain.Entities;
using ChoucairTest.Domain.Exceptions;
using ChoucairTest.Domain.Ports;
using ChoucairTest.Domain.Services;
using ChoucairTest.Domain.Test.EstadoTarea;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace ChoucairTest.Domain.Test.Tarea;

[TestClass]
public class TareaServiceTest
{
    //Middleware 
    ILoginMiddleware _loginMiddleware = default!;

    //Repositorios
    ITareaRepository _tareaRepository = default!;
    IEstadoTareaRepository _estadoTareaRepository = default!;

    //Servicios
    TareaService _tareaService = default!;


    [TestInitialize]
    public void Init() 
    {
        _loginMiddleware = Substitute.For<ILoginMiddleware>();

        _tareaRepository = Substitute.For<ITareaRepository>();
        _estadoTareaRepository = Substitute.For<IEstadoTareaRepository>();

        _tareaService = new TareaService(_tareaRepository, _estadoTareaRepository, _loginMiddleware);
    }

    [TestMethod]
    public async Task RegistrarTareaExitosaAsync() 
    {
        Domain.Entities.Tarea tarea = new TareaDataBuilder()
            .WithId(Guid.NewGuid())
            .WithTitulo("Tarea Exitosa")
            .WithDescripcion("Descripcion de tarea exitosa")
            .WithFechaVencimiento(DateTime.Now)
            .Build();

        List<Domain.Entities.EstadoTarea> estadosTarea = new EstadoTareaDataBuilder()
            .WithId(Guid.NewGuid())
            .WithCodigo("C")
            .WithNombre("CREADA")
            .BuildList();

        _estadoTareaRepository
            .GetAsync()
            .Returns(estadosTarea);

        _tareaRepository
            .AddAsync(Arg.Any<Domain.Entities.Tarea>())
            .Returns(Task.FromResult(tarea));

        var result = await _tareaService.GuardarTareaAsync(tarea);

        Assert.IsTrue((result != null) && result?.Id is not null);
    }

    [TestMethod]
    public async Task ModificarTareaExitosaAsync() 
    {
        Domain.Entities.Tarea tarea = new TareaDataBuilder()
            .WithId(Guid.NewGuid())
            .WithTitulo("Tarea Exitosa")
            .WithDescripcion("Descripcion de tarea exitosa")
            .WithFechaVencimiento(DateTime.Now)
            .Build();

        _tareaRepository
            .GetByIdAsync(Arg.Any<Guid>())
            .Returns(Task.FromResult(tarea));

        _tareaRepository
            .UpdateAsync(Arg.Any<Domain.Entities.Tarea>())
            .Returns(Task.FromResult(tarea));

        var result = await _tareaService.EditarTareaAsync(tarea);

        Assert.IsTrue((result != null) && result?.Id is not null);
    }

    [TestMethod]
    public async Task EliminarTareaExitosaAsync() 
    {
        Domain.Entities.Tarea tarea = new TareaDataBuilder()
            .WithId(Guid.NewGuid())
            .WithTitulo("Tarea Exitosa")
            .WithDescripcion("Descripcion de tarea exitosa")
            .WithFechaVencimiento(DateTime.Now)
            .Build();

        _tareaRepository
            .GetByIdAsync(Arg.Any<Guid>())
            .Returns(Task.FromResult(tarea));

        _tareaRepository
            .UpdateAsync(Arg.Any<Domain.Entities.Tarea>())
            .Returns(Task.FromResult(tarea));

        try
        {
            await _tareaService.EliminarTareaAsync(tarea.Id);

            Assert.IsTrue((tarea != null) && tarea?.Id is not null);
        }
        catch (Exception ex)
        {
            Assert.IsFalse(ex is TareaNotFoundException);
        }
    }
}
