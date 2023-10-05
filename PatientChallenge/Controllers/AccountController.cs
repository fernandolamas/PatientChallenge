using Microsoft.AspNetCore.Mvc;
using PatientChallenge.Service.AccountService;
using PatientChallenge.Shared.Model;

namespace PatientChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) {
            _accountService = accountService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetToken(UserLogin userLogin)
        {
            try
            {
                UserToken token =  await _accountService.RetrieveToken(userLogin);
                return Ok(new { Message = "Welcome", token.UserName, token });
            }catch(ArgumentException arEx){ return Unauthorized(arEx.Message);
            }catch(Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}