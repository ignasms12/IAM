using Dapper;
using IAM.Helpers;
using IAM.Models;
using IAM.Models.DTO;
using System.Data;

namespace IAM.Services
{
    public class GetDataService : IGetDataService
    {
        private readonly DapperContext _context;

        public GetDataService(DapperContext context)
        {
            _context = context;
        }

        public Tuple<IEnumerable<User>, StatusInfo> GetUsers(Guid userId)
        {
            using(var conn = _context.CreateConnection())
            {
                var procedure = "[AD].[getUsers]";

                var parameters = new DynamicParameters();
                parameters.Add(name: "@userId", value: userId, dbType: DbType.Guid, direction: ParameterDirection.Input);
                parameters.Add(name: "@statusMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 60);
                parameters.Add(name: "@rv", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                IEnumerable<User> users = conn.Query<User>(procedure, parameters, commandType: CommandType.StoredProcedure);

                conn.Close();

                int returnValue = parameters.Get<int>("@rv");
                string statusMessage = parameters.Get<string>("@statusMessage");

                StatusInfo sInfo = new StatusInfo()
                {
                    StatusCode = returnValue,
                    StatusMessage = statusMessage
                };

                return Tuple.Create(users, sInfo);
            }
        }

        public async Task<(Res_GetDetailedInfoDTO, StatusInfo)> GetUserInfoAsync(Guid userId, Guid objectId, string mode)
        {

            using (IDbConnection conn = _context.CreateConnection())
            {
                string procedure = "[AD].[getUserInfo]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(name: "@userId", value: userId, dbType: DbType.Guid, direction: ParameterDirection.Input);
                parameters.Add(name: "@targetUserId", value: objectId, dbType: DbType.Guid, direction: ParameterDirection.Input);
                parameters.Add(name: "@mode", value: mode, dbType: DbType.String, direction: ParameterDirection.Input, size: 40);
                parameters.Add(name: "@statusMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 60);
                parameters.Add(name: "@rv", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                Res_GetDetailedInfoDTO res = new Res_GetDetailedInfoDTO();

                using (var reader = await conn.QueryMultipleAsync(procedure, parameters, commandType: CommandType.StoredProcedure))
                {
                    //IEnumerable<User> users = reader.Read<User>().ToList();

                    res.users = await reader.ReadAsync<User>();

                    res.roles = await reader.ReadAsync<Role>();

                    res.functions = await reader.ReadAsync<Function>();

                    //if (reader.IsConsumed)
                    //{
                    //}
                    //if (reader.IsConsumed)
                    //{
                    //    res.functions = reader.Read<Function>().ToList();
                    //}

                    conn.Close();
                }

                int returnValue = parameters.Get<int>("@rv");
                string statusMessage = parameters.Get<string>("@statusMessage");

                StatusInfo sInfo = new StatusInfo()
                {
                    StatusCode = returnValue,
                    StatusMessage = statusMessage
                };

                return (res, sInfo);
            }
        }

        public Tuple<IEnumerable<Role>, StatusInfo> GetRoles(Guid userId)
        {
            using (var conn = _context.CreateConnection())
            {
                var procedure = "[AD].[getRoles]";

                var parameters = new DynamicParameters();
                parameters.Add(name: "@userId", value: userId, dbType: DbType.Guid, direction: ParameterDirection.Input);
                parameters.Add(name: "@statusMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 60);
                parameters.Add(name: "@rv", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                IEnumerable<Role> roles = conn.Query<Role>(procedure, parameters, commandType: CommandType.StoredProcedure);

                conn.Close();

                int returnValue = parameters.Get<int>("@rv");
                string statusMessage = parameters.Get<string>("@statusMessage");

                StatusInfo sInfo = new StatusInfo()
                {
                    StatusCode = returnValue,
                    StatusMessage = statusMessage
                };

                return Tuple.Create(roles, sInfo);
            }
        }

        public async Task<Tuple<Res_GetDetailedInfoDTO, StatusInfo>> GetRoleInfoAsync(Guid userId, Guid objectId, string mode)
        {
            using (var conn = _context.CreateConnection())
            {
                var procedure = "[AD].[getRoleInfo]";

                var parameters = new DynamicParameters();
                parameters.Add(name: "@userId", value: userId, dbType: DbType.Guid, direction: ParameterDirection.Input);
                parameters.Add(name: "@targetRoleId", value: objectId, dbType: DbType.Guid, direction: ParameterDirection.Input);
                parameters.Add(name: "@mode", value: mode, dbType: DbType.String, direction: ParameterDirection.Input, size: 40);
                parameters.Add(name: "@statusMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 60);
                parameters.Add(name: "@rv", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                var results = await conn.QueryMultipleAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

                IEnumerable<Role> roles = results.Read<Role>().ToList();

                Res_GetDetailedInfoDTO res = new Res_GetDetailedInfoDTO()
                {
                    roles = roles
                };

                if (!results.IsConsumed)
                {
                    res.users = results.Read<User>().ToList();
                }
                if (!results.IsConsumed)
                {
                    res.functions = results.Read<Function>().ToList();
                }

                conn.Close();

                int returnValue = parameters.Get<int>("@rv");
                string statusMessage = parameters.Get<string>("@statusMessage");

                StatusInfo sInfo = new StatusInfo()
                {
                    StatusCode = returnValue,
                    StatusMessage = statusMessage
                };

                return Tuple.Create(res, sInfo);
            }
        }

        public Tuple<IEnumerable<Function>, StatusInfo> GetFunctions(Guid userId)
        {
            using (var conn = _context.CreateConnection())
            {
                var procedure = "[AD].[getFunctions]";

                var parameters = new DynamicParameters();
                parameters.Add(name: "@userId", value: userId, dbType: DbType.Guid, direction: ParameterDirection.Input);
                parameters.Add(name: "@statusMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 60);
                parameters.Add(name: "@rv", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                IEnumerable<Function> functions = conn.Query<Function>(procedure, parameters, commandType: CommandType.StoredProcedure);

                conn.Close();

                int returnValue = parameters.Get<int>("@rv");
                string statusMessage = parameters.Get<string>("@statusMessage");

                StatusInfo sInfo = new StatusInfo()
                {
                    StatusCode = returnValue,
                    StatusMessage = statusMessage
                };

                return Tuple.Create(functions, sInfo);
            }
        }

        public async Task<Tuple<Res_GetDetailedInfoDTO, StatusInfo>> GetFunctionInfoAsync(Guid userId, Guid objectId, string mode)
        {
            using (var conn = _context.CreateConnection())
            {
                var procedure = "[AD].[getFunctionInfo]";

                var parameters = new DynamicParameters();
                parameters.Add(name: "@userId", value: userId, dbType: DbType.Guid, direction: ParameterDirection.Input);
                parameters.Add(name: "@targetFunctionId", value: objectId, dbType: DbType.Guid, direction: ParameterDirection.Input);
                parameters.Add(name: "@mode", value: mode, dbType: DbType.String, direction: ParameterDirection.Input, size: 40);
                parameters.Add(name: "@statusMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 60);
                parameters.Add(name: "@rv", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                var results = await conn.QueryMultipleAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

                IEnumerable<Function> functions = results.Read<Function>().ToList();

                Res_GetDetailedInfoDTO res = new Res_GetDetailedInfoDTO()
                {
                    functions = functions
                };

                if (results.IsConsumed)
                {
                    res.users = results.Read<User>().ToList();
                }
                if (results.IsConsumed)
                {
                    res.roles = results.Read<Role>().ToList();
                }

                conn.Close();

                int returnValue = parameters.Get<int>("@rv");
                string statusMessage = parameters.Get<string>("@statusMessage");

                StatusInfo sInfo = new StatusInfo()
                {
                    StatusCode = returnValue,
                    StatusMessage = statusMessage
                };

                return Tuple.Create(res, sInfo);
            }
        }
    }
}

