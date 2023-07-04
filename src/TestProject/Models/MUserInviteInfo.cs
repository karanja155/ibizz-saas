using TestProject.Interfaces;

namespace TestProject.Models;

public class MUserInviteInfo : IUserInviteInfo
{
	public string FullName { get; set; } = string.Empty;
	public string EmailAddress { get; set; } = string.Empty;
	public string Telephone { get; set; } = string.Empty;
	public string Country { get; set; } = string.Empty;
	public string Extras { get; set; } = string.Empty;
}
