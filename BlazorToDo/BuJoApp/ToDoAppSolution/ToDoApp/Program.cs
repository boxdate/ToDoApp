using ToDoApp.Components;
using ToDoApp.Application;
using ToDoApp.Domain;
using ToDoApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 古い登録を置き換える:
// builder.Services.AddScoped<ITodoService, JsonTodoService>();

// この新しい登録に置き換える:
builder.Services.AddScoped<ITodoService>(sp => new JsonTodoService("todos.json"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
