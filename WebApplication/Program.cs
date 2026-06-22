using Microsoft.EntityFrameworkCore;
using WebApplication.DataContext;
using WebApplication.Interfaces;
using WebApplication.Services;

//var builder = WebApplication.CreateBuilder(args);
var builder = global::Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configuração de injeção de dependência para os serviços
builder.Services.AddScoped<FuncionarioInterface, FuncionarioService>();
builder.Services.AddScoped<AuthInterface, AuthService>();
builder.Services.AddScoped<SenhaInterface, SenhaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
