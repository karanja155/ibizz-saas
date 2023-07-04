using TestProject.Interfaces;

namespace TestProject.Models;

public class MUserInvited : IUserInvited
{
	public Guid TenantId { get; set; }
	public string EmailAddress { get; set; } = string.Empty;
	public string Code { get; set; } = string.Empty;
	public DateTime ExpiryDate { get; set; }
}
