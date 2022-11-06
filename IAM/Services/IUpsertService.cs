using System;
using IAM.Models;
using IAM.Entities;

namespace IAM.Services
{
    public interface IUpsertService
    {
        public IEnumerable<UpsertResponseDTO> Upsert(UpsertRequestDTO requestBody);
    }
}

