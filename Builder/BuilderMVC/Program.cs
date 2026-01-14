using BLL.Abstract;
using BLL.Concrete;
using BLL.Mapper;
using DAL.Abstract;
using DAL.Concrete;
using DAL.Database;
using Entities.TableModels.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Cookie Settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<Map>();
});

// DAL Dependencies
builder.Services.AddScoped<IHomeSliderDAL, HomeSliderDAL>();
builder.Services.AddScoped<IAboutDAL, AboutDAL>();
builder.Services.AddScoped<IServiceCategoryDAL, ServiceCategoryDAL>();
builder.Services.AddScoped<IServiceDAL, ServiceDAL>();
builder.Services.AddScoped<ITeamCategoryDAL, TeamCategoryDAL>();
builder.Services.AddScoped<ITeamMemberDAL, TeamMemberDAL>();
builder.Services.AddScoped<IProjectCategoryDAL, ProjectCategoryDAL>();
builder.Services.AddScoped<IProjectDAL, ProjectDAL>();
builder.Services.AddScoped<IBlogCategoryDAL, BlogCategoryDAL>();
builder.Services.AddScoped<IBlogPostDAL, BlogPostDAL>();
builder.Services.AddScoped<IContactSubmissionDAL, ContactSubmissionDAL>();
builder.Services.AddScoped<ISiteSettingsDAL, SiteSettingsDAL>();
builder.Services.AddScoped<IPageBannerDAL, PageBannerDAL>();

// BLL Dependencies
builder.Services.AddScoped<IHomeSliderService, HomeSliderManager>();
builder.Services.AddScoped<IAboutService, AboutManager>();
builder.Services.AddScoped<IServiceCategoryService, ServiceCategoryManager>();
builder.Services.AddScoped<IServiceService, ServiceManager>();
builder.Services.AddScoped<ITeamCategoryService, TeamCategoryManager>();
builder.Services.AddScoped<ITeamMemberService, TeamMemberManager>();
builder.Services.AddScoped<IProjectCategoryService, ProjectCategoryManager>();
builder.Services.AddScoped<IProjectService, ProjectManager>();
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryManager>();
builder.Services.AddScoped<IBlogPostService, BlogPostManager>();
builder.Services.AddScoped<IContactSubmissionService, ContactSubmissionManager>();
builder.Services.AddScoped<ISiteSettingsService, SiteSettingsManager>();
builder.Services.AddScoped<IPageBannerService, PageBannerManager>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Area Route
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();