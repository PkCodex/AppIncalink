using DinkToPdf;
using DinkToPdf.Contracts;
using AppIncalink.Datos;
using AppIncalink.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<actividadesDatos>();
builder.Services.AddScoped<grupoDatos>();

//var context = new CustomAssemblyLoadContext();
//context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "LibreriaPDF/libwkhtmltox.dll"));
//builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
// pattern: "{controller=reportes}/{action=VistaParaPDFGrupo}/{id?}");
pattern: "{controller=calendario}/{action=Calendario}/{id?}");

app.Run();
