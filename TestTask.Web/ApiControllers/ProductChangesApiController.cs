using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Dtos;
using TestTask.Core.Interfaces.Repositories;
using Constants = TestTask.Application.Utility.Constants;

namespace TestTask.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class ProductChangesApiController(IProductChangesRepository productChangeRepository, IMapper mapper)
  : ControllerBase
{
  /// <summary>
  ///   Gets Product Changes filtered by date
  /// </summary>
  /// <param name="from">DateTime</param>
  /// <param name="to">DateTime</param>
  /// <returns>Returns ProductChanges</returns>
  /// <remarks>
  ///   Sample request:
  /// 
  ///   GET /api/ProductChangesApi?from=2024-07-10T09%3A01%3A01.913Z&amp;to=2024-07-19T09%3A01%3A01.913Z
  ///
  /// </remarks>
  /// <response code="200">Success</response>
  /// <response code="401">If the user is unauthorized</response>
  [HttpGet]
  [Authorize(Roles = Constants.RoleAdmin)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<ActionResult<ProductChangeDto[]>> Get([FromQuery(Name = "from")] DateTime from,
    [FromQuery(Name = "to")] DateTime to)
  {
    var changes = (await productChangeRepository.GetProductChangesForRangeAsync(from, to, null))
      .Select(mapper.Map<ProductChangeDto>);
    return Ok(changes);
  }
}