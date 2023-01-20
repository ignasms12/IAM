using System;
using Microsoft.AspNetCore.Mvc;
using IAM.Services;
using IAM.Models.DTO;
using IAM.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace IAM.Controllers
{
	[ApiController]
	[Route("api/getData")]
    //[Authorize]
    public class GetDataController : ControllerBase
	{
		private readonly IGetDataService _getDataService;

		public GetDataController(IGetDataService getDataService)
		{
			_getDataService = getDataService;
		}

        [HttpGet("users")]
        public IResult GetUsers([FromQuery] string userId)
        {
            if (userId == null || userId.Length == 0)
            {
                return Results.BadRequest();
            }

            Guid user_guid = Guid.Parse(userId);

            Tuple<IEnumerable<User>, StatusInfo> results = _getDataService.GetUsers(user_guid);

            if(results.Item2.StatusCode != 0)
            {
                return Results.Json(data: results.Item2.StatusMessage, statusCode: results.Item2.StatusCode);
            }

            return Results.Json(results.Item1, statusCode: results.Item2.StatusCode);
        }

        [HttpGet("userInfo")]
        public IResult GetUserInfo([FromQuery] string userId, [FromQuery] string objectId, [FromQuery] string mode)
        {
            if (userId == null || userId.Length == 0 || objectId == null || objectId.Length == 0 || mode == null || mode.Length == 0)
            {
                return Results.BadRequest();
            }

            Guid user_guid = Guid.Parse(userId);
            Guid object_guid = Guid.Parse(objectId);


            //List<string> modesList = JsonSerializer.Deserialize<List<string>>(mode);

            (Res_GetDetailedInfoDTO, StatusInfo) results = _getDataService.GetUserInfoAsync(user_guid, object_guid, mode).Result;

            if (results.Item2.StatusCode != 0)
            {
                return Results.Json(data: results.Item2.StatusMessage, statusCode: results.Item2.StatusCode);
            }

            return Results.Json(results.Item1, statusCode: results.Item2.StatusCode);
        }

        [HttpGet("roles")]
        public IResult GetRoles([FromQuery] string userId)
        {
            if(userId == null || userId.Length == 0)
            {
                return Results.BadRequest();
            }

            Guid user_guid = Guid.Parse(userId);

            Tuple<IEnumerable<Role>, StatusInfo> results = _getDataService.GetRoles(user_guid);

            if (results.Item2.StatusCode != 0)
            {
                return Results.Json(data: results.Item2.StatusMessage, statusCode: results.Item2.StatusCode);
            }

            return Results.Json(results.Item1, statusCode: results.Item2.StatusCode);
        }

        [HttpGet("roleInfo")]
        public IResult GetRoleInfo([FromQuery] string userId, [FromQuery] string objectId, [FromQuery] string mode)
        {
            if (userId == null || userId.Length == 0)
            {
                return Results.BadRequest();
            }

            Guid user_guid = Guid.Parse(userId);
            Guid object_guid = Guid.Parse(objectId);

            Tuple<Res_GetDetailedInfoDTO, StatusInfo> results = _getDataService.GetRoleInfoAsync(user_guid, object_guid, mode).Result;

            if (results.Item2.StatusCode != 0)
            {
                return Results.Json(data: results.Item2.StatusMessage, statusCode: results.Item2.StatusCode);
            }

            return Results.Json(results.Item1, statusCode: results.Item2.StatusCode);
        }

        [HttpGet("functions")]
        public IResult GetFunctions([FromQuery] string userId)
        {
            if (userId == null || userId.Length == 0)
            {
                return Results.BadRequest();
            }

            Guid user_guid = Guid.Parse(userId);

            Tuple<IEnumerable<Function>, StatusInfo> results = _getDataService.GetFunctions(user_guid);

            if (results.Item2.StatusCode != 0)
            {
                return Results.Json(data: results.Item2.StatusMessage, statusCode: results.Item2.StatusCode);
            }

            return Results.Json(results.Item1, statusCode: results.Item2.StatusCode);
        }

        [HttpGet("functionInfo")]
        public IResult GetFunctionInfo([FromQuery] string userId, [FromQuery] string objectId, [FromQuery] string mode)
        {
            if (userId == null || userId.Length == 0)
            {
                return Results.BadRequest();
            }

            Guid user_guid = Guid.Parse(userId);
            Guid object_guid = Guid.Parse(objectId);

            Tuple<Res_GetDetailedInfoDTO, StatusInfo> results = _getDataService.GetFunctionInfoAsync(user_guid, object_guid, mode).Result;

            if (results.Item2.StatusCode != 0)
            {
                return Results.Json(data: results.Item2.StatusMessage, statusCode: results.Item2.StatusCode);
            }

            return Results.Json(results.Item1, statusCode: results.Item2.StatusCode);
        }
    }
}

