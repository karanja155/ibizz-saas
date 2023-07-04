using Microsoft.AspNetCore.Mvc;
using TestProject.Interfaces;
using TestProject.Models;
using System.Data;
using Azure.Communication.Email;
using Microsoft.Data.SqlClient;
using Saas.Identity.Services;
using Saas.Permissions.Service.Models;

namespace TestProject.Services;

public class UserInviteService : IUserInviteService
{
	private readonly IConfiguration _config;
	private readonly ICustomTenantService _tenantService;
	private readonly string _connectionString;

	public UserInviteService(IConfiguration config, ICustomTenantService tenantService)
	{
		_config = config;
		_tenantService = tenantService;
		_connectionString = "Server=tcp:sqldb-asdk-dev-lsg5.database.windows.net,1433;Initial Catalog=iBusinessSaasTests;Persist Security Info=False;User ID=sqlAdmin;Password=UnK307r8DUW0zvlOjli4d1SW+wB138HNA4xgA/oPLe/uA2zmrXzh1NspSEBWM8W6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

	}

	public async Task<IActionResult> SendInvitationAsync(MUserInviteInfo info, Guid userId)
	{

		//string mask = "iBusinessSaasTests";
		//sqlConnectionString = sqlConnectionString.Replace(mask, "tenant_Spike");

		using (SqlConnection con = new(_connectionString))
		{
			con.Open();

			using (SqlCommand command = new("spSaveInviteUserInfo", con))
			{

				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("fullName", SqlDbType.NVarChar).Value = info.FullName;
				command.Parameters.AddWithValue("emailAddress", SqlDbType.NVarChar).Value = info.EmailAddress;
				command.Parameters.AddWithValue("telephone", SqlDbType.NVarChar).Value = info.Telephone;
				command.Parameters.AddWithValue("country", SqlDbType.NVarChar).Value = info.Country;
				command.Parameters.AddWithValue("extras", SqlDbType.NVarChar).Value = info.Extras;
				using (SqlDataReader reader = await command.ExecuteReaderAsync())
				{

					while (reader.Read())
					{
						Console.WriteLine(reader.GetString(0));
					}

					reader.Close();
				}
			}

			con.Close();
		}


		MUserInvited userInvited = new(){
			EmailAddress = info.EmailAddress,
			Code = Guid.NewGuid().ToString(),
			TenantId = userId,
			ExpiryDate = DateTime.Now
			};

		string connectionString = "endpoint=https://ibusinesscommunicationservice.communication.azure.com/;accesskey=S0ZwxgSVJ0zh1Ih2WcRe+hCgshA7JzNM6vJhsg9PQSkVqj4Jsm+lpi/M/A7mPmCk1lGNFdDiUNYHfWxFZQpkOw==";
		EmailClient emailClient = new(connectionString);

		//Replace with your domain and modify the content, recipient details as required

		var subject = "Welcome to Azure Communication Service Email APIs.";
		var htmlContent = "<html>" +
							"<body>" +
								"<h1>Quick send email test</h1>" +
								"<br/>" +
								"<h4>Invitation to iBusiness</h4>" +
								"<p>You have been invited by to join iBusiness as a user to tenant ID"+userInvited.TenantId+". Please confirm your acceptance by clicking the link below.</p>" +
								"<p>https://www.ibusiness.net/inviteuser/</p>"+
							"</body>"+
							"</html>";
		var sender = "test@92422de4-90fc-41bc-b5ea-1c2e7ce81bca.azurecomm.net";
		var recipient = "karanja155@outlook.com";

		try
		{
			Console.WriteLine("Sending email...");
			EmailSendOperation emailSendOperation = await emailClient.SendAsync(
				Azure.WaitUntil.Completed,
				sender,
				recipient,
				subject,
				htmlContent);
			EmailSendResult statusMonitor = emailSendOperation.Value;

			if (statusMonitor.Status == "Succeeded")
			{

				using (SqlConnection con = new(_connectionString))
				{
					con.Open();

					using (SqlCommand command = new("spSaveActiveInvitations", con))
					{

						command.CommandType = CommandType.StoredProcedure;
						command.Parameters.AddWithValue("tenantId", SqlDbType.UniqueIdentifier).Value = userInvited.TenantId;
						command.Parameters.AddWithValue("emailAddress", SqlDbType.NVarChar).Value = info.EmailAddress;
						command.Parameters.AddWithValue("verificationCode", SqlDbType.NVarChar).Value = userInvited.Code;
						command.Parameters.AddWithValue("expiryDate", SqlDbType.Date).Value = userInvited.ExpiryDate;
						using (SqlDataReader reader = await command.ExecuteReaderAsync())
						{

							while (reader.Read())
							{
								Console.WriteLine(reader.GetString(0));
							}

							reader.Close();
						}
					}

					con.Close();
				}
			}

			Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

			/// Get the OperationId so that it can be used for tracking the message for troubleshooting
			string operationId = emailSendOperation.Id;
			Console.WriteLine($"Email operation id = {operationId}");
		}
		catch (Exception ex)
		{
			/// OperationID is contained in the exception message and can be used for troubleshooting purposes
			Console.WriteLine(ex.Message);
		}

		return new OkObjectResult(userInvited);

	}

	public async Task<IActionResult> VerifyInvitationAsync()
	{
		await Task.Delay(1000);
		return new OkResult();
	}

	public async Task<MUserInviteResponse> FetchInvitationAsync(Guid InvitationCode)
	{
		MUserInviteResponse response = new();

		try
		{
			using (SqlConnection con = new(_connectionString))
			{
				con.Open();

				using (SqlCommand command = new("spfetchActiveInvitation", con))
				{

					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("invitationCode", SqlDbType.UniqueIdentifier).Value = InvitationCode;

					using (SqlDataReader reader = await command.ExecuteReaderAsync())
					{

						while (reader.Read())
						{
							if (reader.GetString(0) == "01")
							{
								throw new Exception();

							}
							Console.WriteLine(reader.GetString(0));
							response.InvitationEmail = reader.GetString(0);
							response.InvitationCode = reader.GetGuid(1);
							response.TenantId = reader.GetGuid(2);
						}

						reader.Close();
					}
				}

				con.Close();
			}

		} catch (Exception Ex)
		{
			Console.WriteLine(Ex.Message);
		}

		return response;
	}
}
