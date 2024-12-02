using CommentPost.API.Extensions;

var builder = WebApplication.CreateBuilder(args);


// configure services
builder.Services.AddApplicationDBContext();
builder.Services.AddInfrastructureServices();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

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

app.UseHttpsRedirection();
app.UseAuthorization();

app.Run();
