namespace TestProject.Interfaces;

public interface IUserInviteRequest
{
	public Guid InvitationCode { get; init; }
}