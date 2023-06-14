using NoteApp.Entities;

namespace NoteApp.Context
{
    internal class DbInitiaizer
    {
        internal static void Initialize(NoteDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));

            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;
            }

            var roles = new Role[]
            {
                new Role()
                {
                    RoleName = "Admin",
                    Description = "Role for admin"
                },
                new Role()
                {
                    RoleName = "AppUser",
                    Description = "Role for normal app user",
                }
            };

            foreach (var r in roles)
            {
                context.Roles.Add(r);
            }

            context.SaveChanges();

            var password = "p@ssword1";
            var admin = context.Roles.Where(r => r.RoleName == "Admin").SingleOrDefault();
            var users = new User[]
            {
                new User()
                {
                    UserName = "admin",
                    PhoneNumber = "+2349155061616",
                    Password = password,
                    Email = "admin@gmail.com",
                    RoleId = admin.Id,
                    DateJoined = DateTime.Now
                }
            };

            foreach (var u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();
        }
    }
}
