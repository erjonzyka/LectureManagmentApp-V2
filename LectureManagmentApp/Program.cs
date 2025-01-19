using LectureAppLibrary;
using LectureAppLibrary.Interfaces;
using LectureAppLibrary.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews(); 
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<ISecretaryService, SecretaryService>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Inicializimi i admin nese nuk ekziston
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MyContext>();
    ApplicationDbContextSeed.SeedAdminUser(context); 
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSession(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
