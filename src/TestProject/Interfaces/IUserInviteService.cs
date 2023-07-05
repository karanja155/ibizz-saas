using Microsoft.AspNetCore.Mvc;
using TestProject.Models;

namespace TestProject.Interfaces;

public interface IUserInviteService
{
	Task<IActionResult> SendInvitationAsync(MUserInviteInfo info, Guid userId);
	Task<IActionResult> VerifyInvitationAsync();
	Task<MUserInviteResponse> FetchInvitationAsync(Guid InvitationCode);
}
