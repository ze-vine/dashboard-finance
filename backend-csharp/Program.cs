using backend_csharp.Interfaces;
using backend_csharp.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// 1. Adiciona suporte aos Controllers
builder.Services.AddControllers();

// 2. Registro da Injeção de Dependência (Crucial para o Banco Mercantil)
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

// 3. Configura o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Configura o pipeline de requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();