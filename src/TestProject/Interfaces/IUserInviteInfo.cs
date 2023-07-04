namespace TestProject.Interfaces;

public interface IUserInviteInfo
{
	public string FullName { get; set; }
	public string EmailAddress { get; set; }
	public string Telephone { get; set; }
	public string Country { get; set; }
	public string Extras { get; set; }
}
