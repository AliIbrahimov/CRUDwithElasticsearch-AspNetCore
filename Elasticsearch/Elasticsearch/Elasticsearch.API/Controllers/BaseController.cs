using Elasticsearch.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Elasticsearch.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    [NonAction]

    public IActionResult CreateActionResult<T>(ResponseDTO<T> response)
    {
        if (response.code == HttpStatusCode.NoContent)
            return new ObjectResult(null) { StatusCode = response.code.GetHashCode() };

        return new ObjectResult(response) { StatusCode=response.code.GetHashCode() };
    }

}
