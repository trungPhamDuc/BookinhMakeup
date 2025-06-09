using Microsoft.EntityFrameworkCore;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

builder.Services.AddScoped<MakeupArtistService>();
builder.Services.AddScoped<MakeupServices>();
builder.Services.AddScoped<ArtistMediaService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<FavoriteService>();
builder.Services.AddScoped<MembershipService>();


builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<GeminiApiSettings>(builder.Configuration.GetSection("GeminiApi"));
builder.Services.Configure<GeminiPromptSettings>(builder.Configuration.GetSection("GeminiPrompt"));
builder.Services.Configure<PayOsSetting>(builder.Configuration.GetSection("PayOsKey"));


builder.Services.AddDbContext<BookingMakeupContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyConStr"))
);


builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Home/View", "");
});

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

app.MapRazorPages();
app.Run();