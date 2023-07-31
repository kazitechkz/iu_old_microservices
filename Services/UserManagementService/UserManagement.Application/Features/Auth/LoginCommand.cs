using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.IRepositories;
using UserManagement.Application.DTO.AuthDTO;
using UserManagement.Application.DTO.ResponseDTO;
using UserManagement.Application.Helpers;
using UserManagement.Domain;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.Auth
{
    public class LoginCommand : IRequest<ResponseRDTO<string>>
    {
        public LoginCommand(LoginDTO model)
        {
            this.model = model;
        }
        public LoginDTO model;
        

    }


    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseRDTO<string>>
    {

        private readonly IUserRepository userRepository;
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;
        private readonly AppConfig appConfig;

        public LoginCommandHandler(IClientRepository clientRepository,IUserRepository userRepository, IUserRoleRepository userRoleRepository, IMapper mapper, IOptions<AppConfig> appConfig)
        {
            this.clientRepository = clientRepository;
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
            this.mapper = mapper;
            this.appConfig = appConfig.Value;
        }

        public async Task<ResponseRDTO<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ClientModel client =  await clientRepository.GetByClientId(request.model.ClientId);
                if (client == null)
                {
                    return new ResponseRDTO<string>
                    {
                        StatusCode = 403,
                        Success = false,
                        Message = "Forbidden",
                    };
                }
                if(!SecurityHelper.VerifyPassword(client.ClientSecret, request.model.ClientSecret))
                {
                    return new ResponseRDTO<string>
                    {
                        StatusCode = 403,
                        Success = false,
                        Message = "Forbidden",
                    };
                }

                UserModel user = await userRepository.GetUserByEmail(request.model.Email);
                if (user == null)
                {
                    return new ResponseRDTO<string>
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Неверный Email",
                    };
                }
                if (user.Status != 1 || user.IsDeleted)
                {
                    return new ResponseRDTO<string>
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Вход для данного пользователя не доступен",
                    };
                }
                if (!SecurityHelper.VerifyPassword(user.Password, request.model.Password))
                {
                    return new ResponseRDTO<string>
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Неверный пароль",
                    };
                }
                IReadOnlyCollection<UserRoleModel> userModels = await userRoleRepository.GetActualUserRole(user.Id);
                if( !userModels.Any() )
                {
                    return new ResponseRDTO<string>
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Пользователь не найден",
                    };
                }
                string role = client.RoleCode;
                IEnumerable<long?> schools = userModels.Where(p=>p.Role.Code.Equals(role)).Where(p=>!p.SchoolId.Equals(null)).Select(p=>p.SchoolId).ToArray();
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfig.SecurityKey));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var expirationTimeStamp = DateTime.Now.AddMinutes(120);


                


                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Name, user.Name),
                        new Claim(JwtRegisteredClaimNames.FamilyName, user.Surname),
                        new Claim(JwtRegisteredClaimNames.Birthdate, user.BirthDate.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(ClaimTypes.MobilePhone, user.Phone.ToString()),
                        new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Role, role),
                        new Claim(type:"SchoolId", JsonSerializer.Serialize(schools)),

            };

                var tokenOptions = new JwtSecurityToken(
                            issuer: appConfig.ValidIssuer,
                            claims: claims,
                            expires: expirationTimeStamp,
                            signingCredentials: signingCredentials);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return new ResponseRDTO<string>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = tokenString,
                };
            }
            catch (Exception ex) {

                return new ResponseRDTO<string>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = ex.Message,
                    Detail = ex.ToString(),
                };
            
            
            }
        }
    }
}
