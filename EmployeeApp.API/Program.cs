//phase1
//here, application is being bootstrapped.
using EmployeeApp.API.Data;
using EmployeeApp.API.Mappings;
using EmployeeApp.API.Middlewares;
using EmployeeApp.API.Repository;
using EmployeeApp.API.Repository.Implementation;
using EmployeeApp.API.Services;
using EmployeeApp.API.Services.Implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); //identify config files, creates a host

// Add services to the container.

builder.Services.AddControllers();//setting an evironment to run the controller
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();//swashbuckle for swagger. delete this
//upto here phase 1
//all the registrations in phase 1
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DbCon"));
});
builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService,EmployeeService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});


var app = builder.Build();

//phase2 checking env variables
//middleware pipeline
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
//middlewares working back and forth
app.UseHttpsRedirection(); // http-> https

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
