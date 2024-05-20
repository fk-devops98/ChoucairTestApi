using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChoucairTest.Api.Controllers.Base;


[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
