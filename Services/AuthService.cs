using Microsoft.EntityFrameworkCore;
using SalesService.Data;
using SalesService.Models;
using SalesService.Models.Entities;

namespace SalesService.Services
{
    public interface IAuthService
    {
        Task<LoginResult> ValidateUserAsync(string username, string password, string connectionString);
    }

    public class AuthService : IAuthService
    {
        public async Task<LoginResult> ValidateUserAsync(string username, string password, string connectionString)
        {
            try
            {
                // Create DbContext with dynamic connection string
                using (var context = new ApplicationDbContext(connectionString))
                {
                    // Query using Entity Framework
                    var user = await context.Logins
                        .Where(l => l.Username == username && l.Password == password)
                        .FirstOrDefaultAsync();

                    if (user != null)
                    {
                        return new LoginResult
                        {
                            Success = true,
                            EmployeeId = user.EmployeeId,
                            Username = user.Username,
                            Message = "Login successful"
                        };
                    }
                    else
                    {
                        return new LoginResult
                        {
                            Success = false,
                            Message = "Invalid username or password"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }
    }
}