namespace TestProject.Models;

public class MUserInviteResponse
{
	public string InvitationEmail { get; set; } = string.Empty;
	public Guid InvitationCode { get; set; }
	public Guid TenantId { get; set; }

}
