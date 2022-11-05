using AutoMapper;
using IAM.Helpers;
using IAM.Models;

namespace IAM.Services
{
    public class UserService : IUserService
    {
        private readonly DapperContext _context;

        private readonly IMapper _mapper;

        public UserService(DapperContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<User> GetUsers()
        {
            return new IEnumerable<User>();
        }

    }
}

