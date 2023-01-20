using IAM.Helpers;
using Dapper;
using System.Data;
using IAM.Models.DTO;

namespace IAM.Services
{
    public class UpsertService : IUpsertService
	{
		private readonly DapperContext _context;

		public UpsertService(DapperContext context)
		{
			_context = context;
		}

		public IEnumerable<UpsertResponseDTO> UpsertEntity(UpsertEntityDTO requestBody)
        {
			using(var conn = _context.CreateConnection())
            {
				var procedure = "[AD].[upsertWrapper]";

				IEnumerable<UpsertResponseDTO> results;

				results = conn.Query<UpsertResponseDTO>(procedure, requestBody, commandType: CommandType.StoredProcedure);

				return results;
            }
        }

		public IEnumerable<UpsertResponseDTO> UpsertEntityRelations(UpsertEntityRelationDTO requestBody)
		{
			using(var conn = _context.CreateConnection())
			{
				var procedure = "[AD].[upsertEntityRelations]";

				IEnumerable<UpsertResponseDTO> results = conn.Query<UpsertResponseDTO>(procedure, requestBody, commandType: CommandType.StoredProcedure);

				return results;
			}
		}
	}
}

