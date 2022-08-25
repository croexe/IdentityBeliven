using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Database;
using TaskManager.Infrastructure.MappingProfiles;
using TaskManager.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TaskDbContext>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddAutoMapper(typeof(ClientProfile));

var connectionString = builder.Configuration.GetConnectionString("Default");
var jwtAudience = builder.Configuration.GetSection("JWT:ValidAudience").Value;
var jwtIssuer = builder.Configuration.GetSection("JWT:ValidIssuer").Value;
var jwtSecret = builder.Configuration.GetSection("JWT:Secret").Value;

builder.Services.AddDbContext<TaskDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TaskDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidAudience = jwtAudience,
         ValidIssuer = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
     };
 });

var app = builder.Build();
var RoleManager = app.Services.GetRequiredService<RoleManager<IdentityRole>>();
var UserManager = app.Services.GetRequiredService<UserManager<IdentityUser>>();

async Task CreateUser()
{
    var _user = await UserManager.FindByEmailAsync("irinejs@gmail.com");

    Task<bool> hasAdminRole = RoleManager.RoleExistsAsync("ProjectManager");
    hasAdminRole.Wait();
    Task<IdentityResult> roleResult;

    if (!hasAdminRole.Result)
    {
        roleResult = RoleManager.CreateAsync(new IdentityRole("ProjectManager"));
        roleResult.Wait();
    }

    string[] roleNames = { "Admin", "ProjectManager", "Financije", "Urbanizam", "Gospodarstvo", "SuperUser" };

    foreach (var roleName in roleNames)
    {
        var roleExist = await RoleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            //create the roles and seed them to the database: Question 1
            roleResult = RoleManager.CreateAsync(new IdentityRole(roleName));
            roleResult.Wait();
        }
    }

    if (_user == null)
    {
        var res = await UserManager.CreateAsync(new IdentityUser("irinejs@gmail.com") { Email = "irinejs@gmail.com" }, "xxx@");
        _user = await UserManager.FindByEmailAsync("irinejs@gmail.com");
        await UserManager.AddToRoleAsync(_user, "ProjectManager");
    }

    var myRole = RoleManager.FindByNameAsync("ProjectManager");
    Console.WriteLine(_user.Email);
    Console.WriteLine(myRole);
}

CreateUser();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
