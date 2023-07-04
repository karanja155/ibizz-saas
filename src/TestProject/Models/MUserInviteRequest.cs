namespace Saas.Permissions.Service.Models;

public record MUserInviteRequest
{
	public Guid InvitationCode { get; set; }
}
