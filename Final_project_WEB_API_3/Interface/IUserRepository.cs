using Final_project_WEB_API_3.Dto;
using Final_project_WEB_API_3.Models;

namespace Final_project_WEB_API_3.Interface
{
    public interface IUserRepository
    {
        public List<Users> ReadDatabase();

        public Users Insert(UserDto entity);

        public Users Get(string name, string password);
    }
}
