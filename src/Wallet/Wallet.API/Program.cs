using Microsoft.EntityFrameworkCore;
using Wallet.BLL.Interfaces;
using Wallet.BLL.Services;
using Wallet.DAL.Context;
using Wallet.DAL.Entities;
using Wallet.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IRepository<User>, GeneralRepository<User>>();
builder.Services.AddTransient<IRepository<Transaction>, TransactionRepository>();
builder.Services.AddTransient<ITransactionService, TransactionService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
