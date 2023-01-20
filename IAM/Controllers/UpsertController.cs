using System;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IAM.Services;
using IAM.Models.DTO;
using System.Text.Json;

namespace IAM.Controllers
{
	[ApiController]
	[Route("api/upsert")]
	public class UpsertController : ControllerBase
	{
		private readonly IUpsertService _upsertService;

		public UpsertController(IUpsertService upsertService)
		{
			_upsertService = upsertService;
		}

		[HttpPost("entity")]
		public IEnumerable<UpsertResponseDTO> UpsertEntity([FromBody] string requestBody)
		{
            UpsertEntityDTO req = JsonSerializer.Deserialize<UpsertEntityDTO>(requestBody);

            UpsertEntityDTO serviceBody = new UpsertEntityDTO() { EntityType = req.EntityType, UserId = req.UserId, Content = req.Content };

			var results = _upsertService.UpsertEntity(serviceBody);

			return results;
		}

		[HttpPost("entityRelation")]
		public IEnumerable<UpsertResponseDTO> UpsertEntityRelations([FromBody] string requestBody)
		{
            UpsertEntityRelationDTO req = JsonSerializer.Deserialize<UpsertEntityRelationDTO>(requestBody);

            UpsertEntityRelationDTO serviceBody = new UpsertEntityRelationDTO() { DestEntity = req.DestEntity, RelationType = req.RelationType, UserId = req.UserId, Content = req.Content };

			var results = _upsertService.UpsertEntityRelations(serviceBody);

			return results;
		}
	}
}

