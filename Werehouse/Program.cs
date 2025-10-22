using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
using Warehouse.Data;
using Warehouse.Data;
using Warehouse.Mappings;
using Warehouse.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WarehouseContext>(
 Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("werehouseDatabase")));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<WarehouseContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";          
    options.AccessDeniedPath = "/Account/AccessDenied"; 
});
builder.Services.AddScoped<IAccountService, AccountServices>();
builder.Services.AddScoped<IUserservices, UserServices>();
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddScoped<ICityServices, CityServices>();
builder.Services.AddScoped<ICountryServices, CountryServices>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IWarehouseItemsServices, WarehouseItemsServices>();
builder.Services.AddScoped<IRequestServices, RequestServices>();

builder.Services.AddScoped<IGroupServices, GroupServices>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
        policy.RequireClaim("Group", "Admin"));

    options.AddPolicy("Manager", policy =>
        policy.RequireAssertion(context =>
           
            context.User.HasClaim("Group", "Manager")));

    options.AddPolicy("Warehouse Manager", policy =>
    policy.RequireAssertion(context =>
        
        context.User.HasClaim("Group", "Warehouse Manager")));

    options.AddPolicy("Employee", policy =>
  policy.RequireAssertion(context =>

      context.User.HasClaim("Group", "Employee")));

});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
 
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=LogInPage}/{id?}")
    .WithStaticAssets();


app.Run();
