using IAM.Models;


namespace IAM.Services
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();
    }
}

