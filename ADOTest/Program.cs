
using ADOTest.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<DataBaseContext>(
    //op => op.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudentGrade;Trusted_Connection=True;"));
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.AddScoped<IEmloyeeRepository, EmployeeRepository>();

builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = "768525300122-k158r51l17psmt8dnhb85l44tpv9ogm0.apps.googleusercontent.com";
    options.ClientSecret = "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";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMvc();

app.Run();
