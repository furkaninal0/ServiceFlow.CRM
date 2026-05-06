using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceFlow.CRM.Data;
using ServiceFlow.CRM.Services;

var builder = WebApplication.CreateBuilder(args);

// Connection string'i appsettings.json içinden alıyoruz
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DefaultConnection bulunamadı.");

// Veritabanı bağlantısı
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Development ortamında migration/database hatalarını daha net gösterir
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity kullanıcı sistemi
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Service kayıtları
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IDealService, DealService>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Önce Authentication, sonra Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Identity login/register sayfaları için gerekli
app.MapRazorPages();

app.Run();