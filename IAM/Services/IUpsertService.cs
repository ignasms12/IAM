using System;
using IAM.Models;

namespace IAM.Services
{
    public interface IUpsertService
    {
        public IEnumerable<Function> UpsertFunction();
    }
}

