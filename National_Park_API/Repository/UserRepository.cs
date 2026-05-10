using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using National_Park_API.Data;
using National_Park_API.Models;
using National_Park_API.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace National_Park_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public UserRepository(ApplicationDbContext context,IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var userInDb = _context.Users
                            .FirstOrDefault(u => u.Username == username && u.Password == password); 
            if (userInDb == null) return null;

            //***
            var tokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userInDb.id.ToString()),
                    new Claim(ClaimTypes.Role, userInDb.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userInDb.Token = tokenHandler.WriteToken(token);
            //**
            userInDb.Password = "";
            return userInDb;
        }

        public bool IsUniqueUser(string username)
        {
            var userIndb = _context.Users .FirstOrDefault(u=>u.Username == username);
            if (userIndb == null) return true; return false;
        }

        public User Register(string username, string password)
        {
            var user = new User()
            {
                Username = username,
                Password = password,
                Role = "Admin"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;

        }
    }
}
