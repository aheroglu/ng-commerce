using Business.Abstract;
using Business.Concrete;
using Business.MailServices;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.SeedDatas;
using Entity.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AppCors", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+ğüşıöçĞÜŞİÖÇ";
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<IProductService, ProductManager>();

builder.Services.AddScoped<IProductImageDal, EfProductImageDal>();
builder.Services.AddScoped<IProductImageService, ProductImageManager>();

builder.Services.AddScoped<IReviewDal, EfReviewDal>();
builder.Services.AddScoped<IReviewService, ReviewManager>();

builder.Services.AddScoped<ICityDal, EfCityDal>();
builder.Services.AddScoped<ICityService, CityManager>();

builder.Services.AddScoped<IDistrictDal, EfDistrictDal>();
builder.Services.AddScoped<IDistrictService, DistrictManager>();

builder.Services.AddScoped<IOrderDal, EfOrderDal>();
builder.Services.AddScoped<IOrderService, OrderManager>();

builder.Services.AddScoped<IOrderItemDal, EfOrderItemDal>();
builder.Services.AddScoped<IOrderItemService, OrderItemManager>();

builder.Services.AddScoped<IFavouriteDal, EfFavouriteDal>();
builder.Services.AddScoped<IFavouriteService, FavouriteManager>();

builder.Services.AddScoped<INewsletterDal, EfNewsletterDal>();
builder.Services.AddScoped<INewsletterService, NewsletterManager>();

builder.Services.AddScoped<IMailDal, EfMailDal>();
builder.Services.AddScoped<IMailService, MailManager>();

builder.Services.AddScoped<AccountCreatedMailService>();
builder.Services.AddScoped<OrderCompletedMailService>();
builder.Services.AddScoped<NewsletterMailService>();
builder.Services.AddScoped<CancelNewsletterSubscriptionMailService>();
builder.Services.AddScoped<AccountDeletedMailService>();
builder.Services.AddScoped<AdminAccountCreatedMailService>();
builder.Services.AddScoped<SendForSubscribers>();
builder.Services.AddScoped<SendForMembers>();
builder.Services.AddScoped<SendForAdmins>();
builder.Services.AddScoped<ForgotPasswordMailService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AppCors");

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    var context = services.GetRequiredService<AppDbContext>();

    await SeedRoles.SetData(roleManager);
    await SeedUsers.SetData(userManager);
    await SeedCategories.SetData(context);
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();

    await SeedProducts.SetData(context);
    await SeedProductImages.SetData(context);
    await SeedReviews.SetData(context, userManager);
    await SeedCities.SetData(context);
    await SeedDistricts.SetData(context);
}

app.Run();
