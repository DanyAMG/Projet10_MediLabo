using Microsoft.AspNetCore.Identity;

namespace Auth_Service.Services
{
    /// <summary>
    /// Provides methods to seed initial data such as user roles.
    /// </summary>
    public static class DataSeeder
    {
        /// <summary>
        /// Ensures that predefined roles exist in the identity system.
        /// This method creates the roles "Organisateur" and "Practicien" if they are not already present.
        /// </summary>
        /// <param name="serviceProvider">The service provider used to resolve the RoleManager dependency.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Organisateur", "Practicien" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
