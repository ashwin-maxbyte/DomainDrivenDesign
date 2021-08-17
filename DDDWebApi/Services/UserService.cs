using AutoMapper;
using DDD.Base.DataManagement;
using DDD.Base.Models;
using DDD.Base.Queries;
using DDD.Base.TransientModels;
using DDDWebApi.Helpers;
using DDDWebApi.Models.Auth;
using DDDWebApi.Models.User;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DDDWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly AbstractTransaction _transaction;
        private readonly IUserQueries _userQueries;
        private readonly IJwtHelper _jwtHelper;
        private readonly IMapper _mapper;

        public UserService(AbstractTransaction transaction, IUserQueries userQueries, IJwtHelper jwtHelper, IConfiguration configuration, IMapper mapper)
        {
            _transaction = transaction;
            _userQueries = userQueries;
            _jwtHelper = jwtHelper;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<string> Authenticate(AuthenticateRequest authenticateRequest)
        {
            var user = await _userQueries.GetUser(authenticateRequest.Email, authenticateRequest.Password);
            if(user == null)
            {
                return null;
            }
            else
            {
                return IssueToken(user.Email, GetFullName(user.FirstName, user.LastName));
            }
        }

        public async Task<string> AuthenticateExternal(AuthenticateExternalRequest authenticateExternalRequest)
        {
            Dictionary<string, string> claims = GetClaims(authenticateExternalRequest.JsonWebToken);
            if (claims == null)
            {
                return null;
            }
            else
            {
                string email = claims["email"];
                string name = claims["name"];
                User user = await _userQueries.GetUser(email);
                if(user == null)
                {
                    TransientUser transientUser = new()
                    {
                        Email = email,
                        Password = null,
                        IsSocialLogin = true,
                        FirstName = name,
                        LastName = null
                    };
                    await _transaction.InsertUser(transientUser);
                }
                return IssueToken(email, name);
            }
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest)
        {
            TransientUser transientUser = new()
            {
                Email = createUserRequest.Email,
                Password = createUserRequest.Password,
                IsSocialLogin = false,
                FirstName = createUserRequest.FirstName,
                LastName = createUserRequest.LastName
            };
            var user = await _transaction.InsertUser(transientUser);
            return _mapper.Map<CreateUserResponse>(user);
        }

        public async Task<bool> DoesEmailExist(string email)
        {
            var user = await _userQueries.GetUser(email);
            return user != null;
        }

        public async Task<string> SignupLogin(SignupLoginRequest signupLoginRequest)
        {
            TransientUser transientUser = new()
            {
                Email = signupLoginRequest.Email,
                Password = signupLoginRequest.Password,
                IsSocialLogin = false,
                FirstName = signupLoginRequest.FirstName,
                LastName = signupLoginRequest.LastName
            };
            await _transaction.InsertUser(transientUser);
            return IssueToken(transientUser.Email, GetFullName(transientUser.FirstName, transientUser.LastName));
        }

        private string IssueToken(string email, string name)
        {
            var claims = new[] { new Claim(ClaimTypes.Email, email), new Claim("name", name) };
            var daysToExpire = Convert.ToInt32(_configuration.GetSection("JsonWebToken:ExpiryDays").Value);
            var expiry = DateTime.Now.AddDays(daysToExpire);
            var signingKey = _configuration.GetSection("JsonWebToken:SiginingKey").Value;
            return _jwtHelper.IssueJwt(claims, expiry, signingKey);
        }

        private Dictionary<string, string> GetClaims(string jsonWebToken)
        {
            string socialLoginSigningKey = _configuration.GetSection("JsonWebToken:SocialLoginSiginingKey").Value;
            bool isValidJwt = _jwtHelper.ValidateJwt(jsonWebToken, socialLoginSigningKey);
            if(isValidJwt)
            {
                return _jwtHelper.GetClaims(jsonWebToken);
            }
            else
            {
                return null;
            }
        }

        private static string GetFullName(string firstName, string lastName) 
        {
            return $"{firstName} {(string.IsNullOrEmpty(lastName) ? string.Empty : lastName)}".TrimEnd();
        }
    }
}
