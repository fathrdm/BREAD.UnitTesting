using Mahasiswa.Application.Services;
using Mahasiswa.Domain.Interface;
using Mahasiswa.Infrastructure.Data;
using Mahasiswa.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MahasiswaDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MahasiswaDbContext") ??
        throw new InvalidOperationException("Connection string 'MahasiswaDbContext' not found.")));


builder.Services.AddScoped<IMahasiswaRepository, MahasiswaRepository>();
builder.Services.AddScoped<IMahasiswaServices, MahasiswaServices>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
