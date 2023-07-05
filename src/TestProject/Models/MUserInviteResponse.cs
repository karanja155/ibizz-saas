using TestProject.Interfaces;

namespace TestProject.Models;

public class MUserInviteResponse: IUserInviteResponse
{
	public string InvitationEmail { get; set; } = string.Empty;
	public Guid InvitationCode { get; set; }
	public Guid TenantId { get; set; }

}
