using Final_project_WEB_API_3.Dto;
using Final_project_WEB_API_3.Interface;
using Final_project_WEB_API_3.Models;
using System.Text.Json;

namespace Final_project_WEB_API_3.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _usersDatabaseFile;

        public UserRepository()
        {
                _usersDatabaseFile = $"{Environment.CurrentDirectory}\\user_dataset_js.json";
        }

        public List<Users> ReadDatabase()
        {
            var deserilizedDb = JsonSerializer.Deserialize<List<Users>>(File.ReadAllText(_usersDatabaseFile));
            return deserilizedDb;
        }

        public Users Get(string name, string password)
        {
            var file = ReadDatabase();
            var userSearched = file.Where(x => x.Username == name && x.Password == password).FirstOrDefault();
            return (userSearched);
        }

        public Users Insert(UserDto entity)
        {
            var file = ReadDatabase();

            var lastId = file.OrderBy(x => x.Id).Last().Id + 1;

            var userToInsert = new Users
            {
                Id = lastId,
                Name = entity.Name,
                Username = entity.Username,
                Password = entity.Password,
            };

            file.Add(userToInsert);

            var serialized = JsonSerializer.Serialize(file);
            File.WriteAllText(_usersDatabaseFile, serialized);

            return userToInsert;
        }
    }
}
