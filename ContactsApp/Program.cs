using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ContactsApp.Data;
using ContactsApp.Models;
using ContactsApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//add DbContext
builder.Services.AddDbContext<ContactsContext>
        (options => options.UseSqlite("Name=ToDoListDB"));

builder.Services.Configure<ContactsAppDatabaseSettings>(
    builder.Configuration.GetSection(nameof(ContactsAppDatabaseSettings)));

builder.Services.AddSingleton<IContactsAppDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<ContactsAppDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("ContactsAppDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IContactsService, ContactsService>();

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

