using CommentPost.API.Extensions;
using CommentPost.API.Middlewares;
using CommentPost.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

// set env varaibles to appsetting.json
builder.Configuration.AddEnvironmentVariables();

// load the PostgreDBSettings from appsettings.json
builder.Services.Configure<PostgreSQLSettings>(builder.Configuration.GetSection("PostgreSQLSettings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<Auth0Settings>(builder.Configuration.GetSection("Auth0Settings"));

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddApplicationDBContext();
builder.Services.AddInfrastructureServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped<AuthenticationMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MigrateDbContext();

app.UseRouting();
app.MapControllers();

//app.UseHttpsRedirection();
app.UseMiddleware<AuthenticationMiddleware>();

app.Run();
