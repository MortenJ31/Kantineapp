using Core.Models;

namespace Kantineapp.Services
{
    public class UserServiceInMemory : IUserService
    {
        private readonly List<User> _users;

        public UserServiceInMemory()
        {
            // Mockdata til test
            _users = new List<User>
            {
                new User
                {
                    Id = "user1",
                    Name = "Morten",
                    Email = "morten@mail.dk",
                    Password = "1234",
                    Role = Role.Administrator,
                    MySkills = new List<string>(), // Administrator har ingen direkte skills
                    NotificationMethod = "Email"
                },
                new User
                {
                    Id = "user2",
                    Name = "Sigurd",
                    Email = "sigurd@mail.dk",
                    Password = "5678",
                    Role = Role.Kantineleder,
                    MySkills = new List<string> { "Planlægning", "Organisering" }, // Kantineleder skills
                    NotificationMethod = "SMS"
                },
                new User
                {
                    Id = "user3",
                    Name = "Thomas",
                    Email = "thomas@mail.dk",
                    Password = "abcd",
                    Role = Role.Medarbejder,
                    MySkills = new List<string> { "Madlavning", "Rengøring", "Servering" }, // Flere skills
                    NotificationMethod = "Email"
                }
            };
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return Task.FromResult(_users.AsEnumerable());
        }

        public Task<User?> GetUserByIdAsync(string id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<IEnumerable<User>> GetUsersByRoleAsync(Role role)
        {
            var usersByRole = _users.Where(u => u.Role == role);
            return Task.FromResult(usersByRole);
        }

        public Task<User> AddUserAsync(User newUser)
        {
            newUser.Id = Guid.NewGuid().ToString(); // Generer unikt ID
            _users.Add(newUser);
            return Task.FromResult(newUser);
        }
    }
}
