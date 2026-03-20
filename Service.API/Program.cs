using Microsoft.EntityFrameworkCore;
using Service.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF + CORS + Controllers (restul deja le am)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("ui", p =>
        p.SetIsOriginAllowed(origin =>
        {
            // permite orice port de pe localhost (http sau https)
            try
            {
                var uri = new Uri(origin);
                return uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);
            }
            catch { return false; }
        })
        .AllowAnyHeader()
        .AllowAnyMethod()
    // .AllowCredentials() // doar dacă folosești cookies/autentificare
    );
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

// Swagger UI (trebuie pornit mereu ca sa nu ma incurc cu mediul)
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("ui");
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles(); // ca să poți accesa /uploads/poza.jpg

app.Run();
