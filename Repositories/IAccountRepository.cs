using System;
using heroesBackend.models;
using Microsoft.AspNetCore.Identity;

namespace heroesBackend.Repositories
{
    public interface IAccountRepository
    {
        Task<string> SignUp(signUpModel signupModel);
        Task<string> Login(loginModel loginModel);
    }
}

