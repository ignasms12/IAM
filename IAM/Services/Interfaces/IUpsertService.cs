using IAM.Models.DTO;

namespace IAM.Services
{
	public interface IUpsertService
	{
		public IEnumerable<UpsertResponseDTO> UpsertEntity(UpsertEntityDTO requestBody);
		public IEnumerable<UpsertResponseDTO> UpsertEntityRelations(UpsertEntityRelationDTO requestBody);
	}
}

