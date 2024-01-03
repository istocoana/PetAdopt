using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PetAdopt.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EditOrDeletePolicy", policy =>
    {
        policy.AddRequirements(new OperationAuthorizationRequirement { Name = "Edit" });
        policy.AddRequirements(new OperationAuthorizationRequirement { Name = "Delete" });
    });
});


builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeFolder("/Animals");
    options.Conventions.AllowAnonymousToPage("/Animals/Index");
    options.Conventions.AllowAnonymousToPage("/Animals/Details");
    options.Conventions.AuthorizeFolder("/Animals/Edit", "EditOrDeletePolicy");
    options.Conventions.AuthorizeFolder("/Animals/Delete", "EditOrDeletePolicy");



    options.Conventions.AuthorizeFolder("/Posts");
    options.Conventions.AllowAnonymousToPage("/Posts/Index");
    options.Conventions.AllowAnonymousToPage("/Posts/Details");
    options.Conventions.AuthorizeFolder("/Posts/Edit", "EditOrDeletePolicy");
    options.Conventions.AuthorizeFolder("/Posts/Delete", "EditOrDeletePolicy");
});


builder.Services.AddDbContext<PetAdoptContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetAdoptContext") ?? throw new InvalidOperationException("Connection string 'PetAdoptContext' not found.")));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetAdoptContext") ?? throw new InvalidOperationException("Connection string 'PetAdoptContext' not found.")));


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<LibraryIdentityContext>()
        .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
