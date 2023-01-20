using IAM.Models;
using IAM.Models.DTO;

namespace IAM.Services
{
    public interface IGetDataService
    {
        public Tuple<IEnumerable<User>, StatusInfo> GetUsers(Guid userId);
        public Task<(Res_GetDetailedInfoDTO, StatusInfo)> GetUserInfoAsync(Guid userId, Guid objectId, string mode);
        public Tuple<IEnumerable<Role>, StatusInfo> GetRoles(Guid userId);
        public Task<Tuple<Res_GetDetailedInfoDTO, StatusInfo>> GetRoleInfoAsync(Guid userId, Guid objectId, string mode);
        public Tuple<IEnumerable<Function>, StatusInfo> GetFunctions(Guid userId);
        public Task<Tuple<Res_GetDetailedInfoDTO, StatusInfo>> GetFunctionInfoAsync(Guid userId, Guid objectId, string mode);
    }
}

