using System;
using heroesBackend.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace heroesBackend.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<Trainer> _userManager;
        private readonly SignInManager<Trainer> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<Trainer> userManager, SignInManager<Trainer> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> SignUp(signUpModel signupModel)
        {
            Trainer trainer = new()
            {
                Name = signupModel.Name,
                Email = signupModel.Email,
                UserName = signupModel.Email
            };
            var result = await _userManager.CreateAsync(trainer, signupModel.Password);
            if (result.Succeeded)
            {
                return trainer.Id;
            }
            return null;
        }

        public async Task<string> Login(loginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
           return CreateNewToken(user);
        }

        private string CreateNewToken(Trainer user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

