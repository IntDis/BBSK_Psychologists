using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Infrastructure;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BBSK_Psycho.BusinessLayer.Services;

public class AuthServices : IAuthServices
{
    private readonly IClientsRepository _clientsRepository;
    private readonly IPsychologistsRepository _psychologistsRepository;
    private readonly IManagerRepository _managerRepository;

    public AuthServices(IClientsRepository clientsRepository, IPsychologistsRepository psychologistsRepository, IManagerRepository managerRepository)
    {
        _clientsRepository = clientsRepository;
        _psychologistsRepository = psychologistsRepository;
        _managerRepository=managerRepository;
    }

    

    public ClaimModel GetUserForLogin(string email, string password)
    {
        ClaimModel claimModel = new();

        var manager = _managerRepository.GetManagerByEmail(email);

        if (manager is not null && email == manager.Email &&
            PasswordHash.ValidatePassword(password, manager.Password) && !manager.IsDeleted)
        {
            claimModel.Email = email;
            claimModel.Role = Role.Manager.ToString();

        }
        else
        {
            var client = _clientsRepository.GetClientByEmail(email);
            var psychologist = _psychologistsRepository.GetPsychologistByEmail(email);
            

            if (client == null && psychologist == null)
            {
                throw new EntityNotFoundException("User not found");
            }

            dynamic user = client != null ? client : psychologist;
            
            if (!PasswordHash.ValidatePassword(password, user.Password))
            {
                throw new EntityNotFoundException("Invalid  password");
            }
            else
            {
                if(user.IsDeleted)
                {
                    throw new EntityNotFoundException("User not found");
                }
                else
                {
                    claimModel.Email = user.Email;
                    claimModel.Role = client != null ? Role.Client.ToString() : Role.Psychologist.ToString();

                }
                
            }

        }
        if (claimModel is null)
        {
            throw new EntityNotFoundException("Invalid  password");
        }

        return claimModel;
    }

    public string GetToken(ClaimModel model)
    {
        if (model is null || model.Email is null || model.Role is null)
        {
            throw new DataException("Object or part of it is empty");
        }

        var claims = new List<Claim>
        {
            { new Claim(ClaimTypes.Name, model.Email) },
            { new Claim(ClaimTypes.Role, model.Role) },
            { new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()) }
        };
            
            
    
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
