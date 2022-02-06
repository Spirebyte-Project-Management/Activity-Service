using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spirebyte.Services.Activities.Application.Activities.DTO;
using Spirebyte.Services.Activities.Application.Activities.Queries;
using Spirebyte.Services.Activities.Application.Contexts;
using Swashbuckle.AspNetCore.Annotations;

namespace Spirebyte.Services.Activities.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ActivitiesController : Controller
{
    private readonly IAppContext _appContext;
    private readonly IDispatcher _dispatcher;

    public ActivitiesController(IDispatcher dispatcher, IAppContext appContext)
    {
        _dispatcher = dispatcher;
        _appContext = appContext;
    }

    [HttpGet]
    [SwaggerOperation("Browse activities")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedResult<ActivityDto>>> Index([FromQuery] BrowseActivities query)
    {
        return Ok(await _dispatcher.QueryAsync(query));
    }
}