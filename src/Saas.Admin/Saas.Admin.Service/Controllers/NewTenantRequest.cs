﻿using Saas.Admin.Service.Data.Models.OnBoarding;

namespace Saas.Admin.Service.Controllers;

public class NewTenantRequest
{
    private UserInfo userInfo;
    private UserTenant userTenant;

    public string Name { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty;
    public string CreatorEmail { get; set; } = string.Empty;
    public int ProductTierId { get; set; }
    public int CategoryId { get; set; }

    //Extra fields
    public string Question { get; set; } = string.Empty;

    public string Answer { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string TimeZone { get; set; } = string.Empty;

    public string Profession { get; set; } = string.Empty;

    public int NoofEmployees { get; set; }

    internal Tenant ToTenant()
    {
        Tenant tenant = new Tenant()
        {
            Company = Name,
            Route = Route,
            CreatedUser = CreatorEmail,
            ConcurrencyToken = null,
            CreatedDate = null,
            Industry = CategoryId,
            ProductTierId = ProductTierId,
            Employees = NoofEmployees,
            Country = Country,
            TimeZone = TimeZone,

        };
        return tenant;
    }

    internal UserInfo UserInfo
    {
        get { return userInfo; }
        set { 

            //Update the remaining user info
            value.Question = Question;
            value.Answer = Answer;
            userInfo = value;
        } 
    }

    internal UserTenant UserTenant 
    { 
        
        get { return userTenant; } 
        set 
        {
            value.RegSource = "AB2C";
            value.PrincipalUser = true;
            value.CreatedDate = DateTime.UtcNow;
            value.Tenant = ToTenant();
            value.UserInfo = UserInfo;

            userTenant = value;
        } 
    }
}
