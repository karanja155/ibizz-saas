using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using Saas.Identity.Services;
using Saas.Shared.DataHandler;
using System.Text;
using TestProject.Interfaces;
using TestProject.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<string>(builder.Configuration.GetRequiredSection("CommServicePrimaryKey"));


// Configure authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = "https://asdkdevlsg5.b2clogin.com/78efba5c-2036-4f8d-a59d-e2531da9a187/v2.0/",
		ValidAudience = "f5a0640e-4328-4770-97dd-7d7e44bcc523",
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value))
	};
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserInviteService, UserInviteService>();
//Add database handler. To directly query and a database using stored procedures or direct queries
builder.Services.AddScoped<IDatabaseHandler, DatabaseHandler>();

//Add custom tenant service handler
builder.Services.AddScoped<ICustomTenantService, CustomTenantService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseCors(ops =>
	{
		string[] origins = {
						"http://localhost:3000",
						"http://localhost:3000/",
						"https://localhost:3000",
						"https://localhost:3000/"
					};
		ops.WithOrigins(origins).AllowCredentials().WithMethods("POST", "GET", "PUT", "DELETE").AllowAnyHeader();
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
