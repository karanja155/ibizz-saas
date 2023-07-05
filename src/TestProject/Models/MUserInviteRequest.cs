namespace TestProject.Models;

public record MUserInviteRequest
{
	public Guid InvitationCode { get; init; }
}
