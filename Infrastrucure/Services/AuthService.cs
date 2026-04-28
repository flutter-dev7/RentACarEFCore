using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastrucure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastrucure.Services;

// Infrastructure/Services/AuthService.cs
using BCrypt.Net; // Не забудь добавить
using Domain.Entities;
using Infrastrucure.Data;
using Microsoft.EntityFrameworkCore;
// ... остальные using

public class AuthService : IAuthService
{
    private readonly AppDbContext _context; // Твой контекст БД
    private readonly IConfiguration _config;

    public AuthService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        // 1. Ищем пользователя в реальной БД
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        // 2. Если пользователя нет — выходим
        if (user == null) return null;

        // 3. ПРОВЕРКА ПАРОЛЯ (Verify)
        // Сравниваем "чистый" пароль из формы и "хеш" из базы данных
        bool isPasswordValid = BCrypt.Verify(password, user.PasswordHash);
        
        if (!isPasswordValid) return null;

        // 4. Генерируем JWT (если проверка прошла успешно)
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] 
            { 
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role) // Роль берем из БД (Admin/User)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
