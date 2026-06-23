//phase1
//here, application is being bootstrapped.
using EmployeeApp.API;
using EmployeeApp.API.Data;
using EmployeeApp.API.Mappings;
using EmployeeApp.API.Middlewares;
using EmployeeApp.API.Repository;
using EmployeeApp.API.Repository.Implementation;
using EmployeeApp.API.Services;
using EmployeeApp.API.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args); //identify config files, creates a host

// Add services to the container.

builder.Services.AddControllers();//setting an evironment to run the controller
builder.Services.AddControllers()
.AddJsonOptions(options =>

options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();//swashbuckle for swagger. delete this
//upto here phase 1
//all the registrations in phase 1
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DbCon"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => { 
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    var jwt = builder.Configuration.GetSection("Jwt");
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwt["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwt["Audience"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!)),
        ClockSkew= TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();


builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService,EmployeeService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddCors(p =>
{
    p.AddPolicy("CorsPolicy", cfg =>
    {
        cfg.WithOrigins("url").AllowAnyHeader().AllowAnyMethod();
        //AllowanyOrigin allows all
    });
});
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});


var app = builder.Build();
app.UseExceptionHandler();
using (var scope = app.Services.CreateScope()) { 

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedRoleAsync(roleManager);

}

//phase2 checking env variables
//middleware pipeline
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
//middlewares working back and forth
app.UseHttpsRedirection(); // http-> https
app.UseAuthentication();
app.UseAuthorization();//authentications

app.MapControllers();// setting the routes

app.UseMiddleware<MyMiddleware>();
app.Use(async (context,next) =>
{
    Console.WriteLine("Hello World before processing request");
    var comp=Environment.GetEnvironmentVariable("Company");
    Console.WriteLine($"{comp}.");
    await next(context);
    Console.WriteLine("After");
}); // inline middleware/custom middleware

app.Run();//run
