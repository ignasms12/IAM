using IAM.Helpers;
using Dapper;
using System.Data;
using IAM.Models.DTO;

namespace IAM.Services
{
	public class AuthService : IAuthService
	{
		private readonly DapperContext _context;

		public AuthService(DapperContext context)
		{
			_context = context;
		}

		public Tuple<Res_LoginDTO, int> GetLoginInfo(string username, string ipAddress)
        {
			using(var conn = _context.CreateConnection())
            {
				var procedure = "[ID].[getLoginInfo]";

				var parameters = new DynamicParameters();
                parameters.Add("@RetVal", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
				parameters.Add("@username", username, dbType: DbType.String, direction: ParameterDirection.Input);
				parameters.Add("@ipAddress", ipAddress, dbType: DbType.String, direction: ParameterDirection.Input);

				Res_LoginDTO result = conn.Query<Res_LoginDTO>(procedure, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

				conn.Close();

				int returnValue = parameters.Get<int>("@RetVal");

				//LoginEntityDTO login = new LoginEntityDTO() { Username = username, IpAddress = "192.168.0.1"};

				//Res_LoginDTO result = conn.Query<Res_LoginDTO>(procedure, login, commandType: CommandType.StoredProcedure).FirstOrDefault();

				return Tuple.Create<Res_LoginDTO, int>(result, returnValue);
            }
        }

	}
}

