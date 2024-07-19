// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using TestTask.Models.Utility;
// using TestTask.Service.IService;
//
// namespace TestTask.ApiControllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class ProductChangesApiController(IProductChangeService productChangeService) : ControllerBase
//     {
//         /// <summary>
//         /// Gets Product Changes
//         /// </summary>
//         /// <remarks>
//         /// https://localhost:44372/api/ProductChangesApi?from=7%2F13%2F2024%2012%3A00%3A00%20AM&to=7%2F14%2F2024%2012%3A00%3A00%20AM
//         /// </remarks>
//         /// <param name="from">DateTime</param>
//         /// <param name="to">DateTime</param>
//         /// <returns>Returns ProductChanges</returns> 
//         /// <response code="200">Success</response>
//         /// <response code="401">If the user is unauthorized</response>
//         [HttpGet]
//         [Authorize(Roles = Constants.RoleAdmin)]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//         public async Task<IActionResult> Get([FromQuery(Name = "from")] DateTime from, [FromQuery(Name = "to")] DateTime to)
//         {
//             
//             return Ok(await productChangeService.GetProductChangesForRangeAsync(from, to));
//         }
//     }
// }
