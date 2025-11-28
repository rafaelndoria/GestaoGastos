using GestaoGastos.Infra.IoC;
using GestaoGastos.WebApp.Middlewares;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Injeção de dependência
InjecaoDependencia.AddInfra(builder.Services, builder.Configuration);

// URLs minúsculas
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

// JWT + redirecionamentos personalizados
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

    // Eventos do JWT para redirecionar
    opt.Events = new JwtBearerEvents
    {
        // Se token não existe ou é inválido → 401
        OnChallenge = context =>
        {
            context.HandleResponse(); // impede retorno padrão 401
            context.Response.Redirect("/usuario/acessonegado?codigoErro=401");
            return Task.CompletedTask;
        },

        // Se usuário está autenticado, mas sem permissão → 403
        OnForbidden = context =>
        {
            context.Response.Redirect("/usuario/acessonegado?codigoErro=403");
            return Task.CompletedTask;
        }
    };
});

// MVC
builder.Services.AddControllersWithViews();

// Sessão
builder.Services.AddSession();

var app = builder.Build();

// Tratamento de erros
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Sessão + Middleware JWT
app.UseSession();
app.UseMiddleware<JwtSessionMiddleware>();

// Autenticação e Autorização
app.UseAuthentication();
app.UseAuthorization();

// Rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Inicio}/{id?}");

app.Run();
