using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spirebyte.Framework.Shared.Handlers;
using Spirebyte.Framework.Shared.Pagination;
using Spirebyte.Services.Activities.Application.Activities.DTO;
using Spirebyte.Services.Activities.Application.Activities.Queries;
using Spirebyte.Services.Activities.Core.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace Spirebyte.Services.Activities.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ActivitiesController : Controller
{
    private readonly IDispatcher _dispatcher;

    public ActivitiesController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    [SwaggerOperation("Browse activities")]
    [Authorize(ApiScopes.ActivitiesRead)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Paged<ActivityDto>>> Index([FromQuery] BrowseActivities query)
    {
        return Ok(await _dispatcher.QueryAsync(query));
    }
}