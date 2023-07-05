using TestProject.Interfaces;
using TestProject.Models;

namespace Saas.Permissions.Service.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserInvitationController : ControllerBase
{
	private readonly IUserInviteService _userInviteService;

	public UserInvitationController(IUserInviteService userInviteService)
	{
		_userInviteService = userInviteService;

	}

	// This is the endpoint that is called by Azure AD B2C to get alle the custom claims defined for a specific user.
	[HttpPost("inviteInfo")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> inviteInfo(MUserInviteRequest? request)
	{
		// Get all the permissions defined for the specific user with requested objectId from the database.
		MUserInviteResponse response = await _userInviteService.FetchInvitationAsync(request.InvitationCode);

		return new OkObjectResult(response);
	}


}

