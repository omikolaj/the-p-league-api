using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueAPI.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  public class AdminController : ThePLeagueBaseController
  {
    [HttpPost("login")]
    public async Task<ActionResult> AdminLogin([FromBody] AuthViewModel adminLogin, CancellationToken ct = default(CancellationToken))
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      return new OkObjectResult("OK");
    }

  }
}