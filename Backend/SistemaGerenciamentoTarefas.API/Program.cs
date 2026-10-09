using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGerenciamentoTarefas.API.Data;
using SistemaGerenciamentoTarefas.API.DTOs;
using SistemaGerenciamentoTarefas.API.Repositories;
using SistemaGerenciamentoTarefas.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITarefaService, TarefaService>();

builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.InvalidModelStateResponseFactory = context => {
        var erros = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .ToDictionary(x => x.Key, x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

        var resposta = new RespostaPadraoDto {
            Sucesso = false,
            Mensagem = "Verifique os campos informados.",
            Erros = erros
        };

        return new BadRequestObjectResult(resposta);
    };
});

builder.Services.AddCors(options => {
    options.AddPolicy("PoliticaAngular", policy => {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        if (context.Response.HasStarted)
        {
            throw;
        }

        int status;
        string mensagem;

        if (ex is KeyNotFoundException)
        {
            status = 404;
            mensagem = ex.Message;
        }
        else if (ex is ArgumentException)
        {
            status = 400;
            mensagem = ex.Message;
        }
        else
        {
            status = 500;
            mensagem = "Ocorreu um erro interno. Tente novamente.";

            app.Logger.LogError(ex, "Erro inesperado na API.");
        }

        context.Response.Clear();
        context.Response.StatusCode = status;

        await context.Response.WriteAsJsonAsync(new RespostaPadraoDto
        {
            Sucesso = false,
            Mensagem = mensagem
        });
    }
});

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PoliticaAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
