using BookAPI.Persistence;
using BookAPI.Features.Books;
using BookAPI.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext (SQLite)
builder.Services.AddDbContext<BookContext>(opt =>
    opt.UseSqlite("Data Source=books.db"));

// FluentValidation
builder.Services.AddScoped<IValidator<CreateBookRequest>, CreateBookValidator>();

// Handlers for Book Object
builder.Services.AddScoped<CreateBookHandler>();
builder.Services.AddScoped<GetAllBooksHandler>();
builder.Services.AddScoped<GetBookByIdHandler>();
builder.Services.AddScoped<UpdateBookHandler>();
builder.Services.AddScoped<DeleteBookHandler>();

//API Setup
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BookContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Mapping endpoints

//GET /books?page=1&pageSize=10
app.MapGet("/books", async (int page, int pageSize, GetAllBooksHandler handler) =>
{
    var result = await handler.HandleAsync(page, pageSize);
    return Results.Ok(result);
});

//POST book
app.MapPost("/books", async (CreateBookRequest req, IValidator<CreateBookRequest> validator, CreateBookHandler handler) =>
{
    var validation = await validator.ValidateAsync(req);
    if (!validation.IsValid)
        return Results.BadRequest(validation.Errors);
    var created = await handler.HandleAsync(req);
    return Results.Ok(created);
});

//GET book
app.MapGet("/books/{id}", async (int id, GetBookByIdHandler handler) =>
{
    var book = await handler.HandleAsync(id);
    return book is null ? Results.NotFound() : Results.Ok(book);
});

//PUT book
app.MapPut("/books/{id}", async (int id, CreateBookRequest req, UpdateBookHandler handler, IValidator<CreateBookRequest> validator) =>
{
    var validation = await validator.ValidateAsync(req);
    if (!validation.IsValid)
        return Results.BadRequest(validation.Errors);

    var updated = await handler.HandleAsync(id, req);
    return updated is null ? Results.NotFound() : Results.Ok(updated);
});

//DELETE book
app.MapDelete("/books/{id}", async (int id, DeleteBookHandler handler) =>
{
    var deleted = await handler.HandleAsync(id);
    return deleted ? Results.Ok() : Results.NotFound();
});

app.Run();