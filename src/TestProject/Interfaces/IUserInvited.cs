namespace TestProject.Interfaces;

public interface IUserInvited
{
	public Guid TenantId { get; set; }
	public string EmailAddress { get; set; }
	public string Code { get; set; }
	public DateTime ExpiryDate { get; set; }
}
