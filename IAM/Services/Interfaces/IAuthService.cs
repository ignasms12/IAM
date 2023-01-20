using IAM.Models.DTO;

namespace IAM.Services
{
    public interface IAuthService
    {
        public Tuple<Res_LoginDTO, int> GetLoginInfo(string username, string ipAddress);
    }
}

