using System.Threading.Tasks;
using Application.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Username = username }));
        }

        [HttpPut]
        public async Task<IActionResult> EditProfile(Profile profile)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { Profile = profile }));
        }

        [HttpGet("{username}/activities")]
        public async Task<IActionResult> GetUserActivites(string username, string predicate)
        {
            return HandleResult(await Mediator.Send(new ListActivities.Query
            {
                Username = username,
                Predicate = predicate
            }));
        }
    }
}