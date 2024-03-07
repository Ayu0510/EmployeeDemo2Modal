using EmployeeDemo.web;
var builder = WebApplication.CreateBuilder(args);
var starup = new Startup(builder.Configuration, builder.Environment);
// Add services to the container.
starup.ConfigureServices(builder.Services);
var app = builder.Build();
starup.Configure(app, builder.Environment);
// Configure the HTTP request pipeline.