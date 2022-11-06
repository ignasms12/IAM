using Microsoft.EntityFrameworkCore;
using IAM.Services;
using IAM.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSingleton<DapperContext>(config => { return new DapperContext(builder.Configuration); });

builder.Services.AddSingleton<DapperContext>(config =>
{
    string connectionString = builder.Configuration.GetConnectionString("MsSql");
    return new DapperContext(connectionString);
});

//builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUpsertService, UpsertService>();

//builder.Services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("IAM"));
//builder.Services.AddDbContext<UserContext>(opt => opt);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

