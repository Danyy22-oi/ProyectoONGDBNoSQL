using ProyectoONGDBNoSQL.Data;
using ProyectoONGDBNoSQL.Repositories; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<ProyectoONGDBNoSQL.Data.MongoDbContext>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<VoluntarioRepository>();
builder.Services.AddScoped<DonanteRepository>();
builder.Services.AddScoped<DonacionRepository>();
builder.Services.AddScoped<ProyectoRepository>();
builder.Services.AddScoped<RecursoRepository>();
builder.Services.AddScoped<InventarioRepository>();
builder.Services.AddScoped<DistribucionRepository>();
builder.Services.AddScoped<BeneficiarioRepository>();
builder.Services.AddScoped<ComunicacionRepository>();
builder.Services.AddScoped<IncidenciaRepository>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
