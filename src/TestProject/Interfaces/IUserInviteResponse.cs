namespace TestProject.Interfaces;

public interface IUserInviteResponse
{
	public string InvitationEmail { get; set; }
	public Guid InvitationCode { get; set; }
	public Guid TenantId { get; set; }
}