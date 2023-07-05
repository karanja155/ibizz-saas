using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestProject.Interfaces;
using TestProject.Models;

namespace TestProject.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[Controller]")]
public class UserInvitationController : Controller
{

	private readonly IUserInviteService _inviteService;

	public UserInvitationController(IUserInviteService inviteService)
	{
		_inviteService = inviteService;
	}

	[HttpPost]
	[Route("newInvite")]
	public async Task<IActionResult> newInvite([FromBody] MUserInviteInfo info)
	{
		ClaimsPrincipal user = User;
		string usId = user.FindFirst(ClaimTypes.Email)?.Value ?? String.Empty;
		// This is where the user claim id will be passed in order to get fetch current tenancy Id
		Guid userId = Guid.NewGuid();
		return await _inviteService.SendInvitationAsync(info, userId);
	}
}
