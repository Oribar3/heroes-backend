using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using heroesBackend.models;
using Microsoft.AspNetCore.Mvc;
using heroesBackend.Repositories;
using heroesBackend.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace heroesBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
    
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] signUpModel signupModel)
        {
            var res = await _accountRepository.SignUp(signupModel);
            if (string.IsNullOrWhiteSpace(res))
            {
                return Unauthorized();
            }
            return Ok(res);
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] loginModel signinModel)
        {
            var res = await _accountRepository.Login(signinModel);
            if (string.IsNullOrWhiteSpace(res))
            {
                return Unauthorized();
            }
            return Ok(res);
        }

      
    }
}

