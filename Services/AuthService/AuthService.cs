using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace TaskManagement.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Register(RegisterUserDto newUser)
        {

            var response = new ServiceResponse<string>();

            if (await UserExists(newUser.Email))
            {
                response.Success = false;
                response.Message = $"User with the given email '{newUser.Email}' already exists!";
                return response;
            }

            CreatePasswordHash(newUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = _mapper.Map<UserModel>(newUser);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            response.Data = CreateToken(user);
            return response;
        }

        public async Task<ServiceResponse<string>> Login(LoginUserDto loginUser)
        {
            var response = new ServiceResponse<string>();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(loginUser.Email.ToLower()));

            if (user is null || !VerifyPasswordHash(loginUser.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Incorrect email or password.";
                return response;
            }

            response.Data = CreateToken(user);

            return response;
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
            {
                return true;
            }

            return false;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private string CreateToken(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var securityToken = _configuration.GetSection("Token:SecurityToken").Value;
            if (securityToken is null)
            {
                throw new Exception("Security Token is null");
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(securityToken));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
