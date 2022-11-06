using System;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IAM.Services;
using IAM.Entities;
using System.Text.Json;

namespace IAM.Controllers
{
    [ApiController]
    [Route("upsert")]

    public class UpsertController : ControllerBase
    {
        private readonly IUpsertService _upsertService;

        public UpsertController(IUpsertService upsertService)
        {
            _upsertService = upsertService;
        }

        [HttpPost]
        public IEnumerable<UpsertResponseDTO> Upsert([FromBody] string requestBody)
        {
            Console.WriteLine(requestBody);

            //string reqBody = "{ \"EntityType\":\"functions\",\"UserId\":\"e58a86db-ea89-425c-acf9-f770f3dd9f60\",\"Content\":\"<Function><Name>AD.functions.upsert</Name><Description>Used for upserting (inserting OR updating) functions</Description></Function>\"}";

            UpsertRequestDTO req = JsonSerializer.Deserialize<UpsertRequestDTO>(requestBody);

            UpsertRequestDTO serviceBody = new UpsertRequestDTO() { EntityType = req.EntityType, UserId = req.UserId, Content = req.Content };
            var results = _upsertService.Upsert(serviceBody);
            return results;
        }
    }
}

