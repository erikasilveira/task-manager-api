using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CREATE - Adicionar nova tarefa
app.MapPost("/tasks", async (AppDbContext db, TaskItem task) =>
{
    db.Tasks.Add(task);
    await db.SaveChangesAsync();
    return Results.Created($"/tasks/{task.Id}", task);
});

// READ - Listar todas as tarefas
app.MapGet("/tasks", async (AppDbContext db) =>
    await db.Tasks.ToListAsync());

// READ (por ID) - Buscar tarefa específica
app.MapGet("/tasks/{id}", async (AppDbContext db, int id) =>
{
    var task = await db.Tasks.FindAsync(id);
    return task is not null ? Results.Ok(task) : Results.NotFound();
});

// UPDATE - Atualizar tarefa existente
app.MapPut("/tasks/{id}", async (AppDbContext db, int id, TaskItem updatedTask) =>
{
    var task = await db.Tasks.FindAsync(id);
    if (task is null) return Results.NotFound();

    task.Title = updatedTask.Title;
    task.Description = updatedTask.Description;
    task.IsCompleted = updatedTask.IsCompleted;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// DELETE - Excluir tarefa
app.MapDelete("/tasks/{id}", async (AppDbContext db, int id) =>
{
    var task = await db.Tasks.FindAsync(id);
    if (task is null) return Results.NotFound();

    db.Tasks.Remove(task);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();