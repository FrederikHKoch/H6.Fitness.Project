using Fitbod.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Fitbod.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

//Add standard dbcontext
builder.Services.AddDbContext<FitbodContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FitbodContext") ?? throw new InvalidOperationException("Connection string 'FitbodContext' not found.")));

//Adding default identity with SignIn requirement.
builder.Services.AddIdentity<FitbodUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultUI()
    .AddEntityFrameworkStores<FitbodContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<FitbodUser>, ApplicationUserClaimsPrincipalFactory>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adminrights", policy =>
        policy.RequireRole("Admin")      
    );
    options.AddPolicy("userrights", policy =>
    policy.RequireRole("Bruger")
    );
    options.AddPolicy("superuserrights", policy =>
    policy.RequireRole("Superbruger")
    );
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

app.UseAuthentication();;
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();