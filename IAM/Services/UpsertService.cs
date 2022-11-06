using System;
using IAM.Helpers;
using Dapper;
using System.Data;
using IAM.Entities;
using Microsoft.Data.SqlClient;

namespace IAM.Services
{
	public class UpsertService : IUpsertService
	{
		private readonly DapperContext _context;

		public UpsertService(DapperContext context)
		{
			_context = context;
		}

		public IEnumerable<UpsertResponseDTO> Upsert(UpsertRequestDTO requestBody)
        {
			using(var conn = _context.CreateConnection())
            {
				var procedure = "[AD].[upsertWrapper]";

				IEnumerable<UpsertResponseDTO> results = null;

				try
                {
					results = conn.Query<UpsertResponseDTO>(procedure, requestBody, commandType: CommandType.StoredProcedure);
                }
				catch(SqlException ex)
                {
					Console.WriteLine(ex);
                }

				return results;
            }
        }
	}
}

