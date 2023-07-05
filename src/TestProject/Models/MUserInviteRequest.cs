using TestProject.Interfaces;

namespace TestProject.Models;

public record MUserInviteRequest : IUserInviteRequest
{
	public Guid InvitationCode { get; init; }
}
