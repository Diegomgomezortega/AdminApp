using AdminApp.Data;
using AdminApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<AdminAppContext>(builder.Configuration.GetConnectionString("AdminAppConn"));
var app = builder.Build();



app.MapDelete("/api/transaction/{Id}", async ([FromServices] AdminAppContext context,[FromRoute] Guid Id )=>
{
    var transactionActual= context.Transactions.Find(Id);
    if(transactionActual!=null)
    { 
        
        context.Remove(transactionActual);
        await context.SaveChangesAsync();
        return Results.Ok();

    }
    
    return Results.NotFound();

});

app.MapDelete("/api/categories/{Id}", async ([FromServices] AdminAppContext context,[FromRoute] Guid Id )=>
{
    var categoryActual= context.Categories.Find(Id);
    if(categoryActual!=null)
    { 
        
        context.Remove(categoryActual);
        await context.SaveChangesAsync();
        return Results.Ok();

    }
    
    return Results.NotFound();

});




app.MapPost("/api/transaction", async ([FromServices] AdminAppContext context,[FromBody] Transaccion transaccion )=>
{
    transaccion.TransactionId= Guid.NewGuid();
    transaccion.Date= DateTime.Now;
    await context.AddAsync(transaccion);
    await context.SaveChangesAsync();
    return Results.Ok();

});

app.MapPost("/api/categories", async ([FromServices] AdminAppContext context,[FromBody] Category category )=>
{
    category.CategoryId= Guid.NewGuid();
    await context.AddAsync(category);
    await context.SaveChangesAsync();
    return Results.Ok();

});

app.MapPut("/api/categories/{Id}", async ([FromServices] AdminAppContext context,[FromBody] Category category,[FromRoute] Guid Id )=>
{
    var categoryActual= context.Categories.Find(Id);
    if(categoryActual!=null)
    { 
        categoryActual.CategoryId=category.CategoryId;
        categoryActual.AmountAllowed=category.AmountAllowed;
        categoryActual.Description=category.Description;
        categoryActual.Name=category.Name;
        await context.SaveChangesAsync();
        return Results.Ok();

    }
    
    return Results.NotFound();

});


app.MapPut("/api/transaction/{Id}", async ([FromServices] AdminAppContext context,[FromBody] Transaccion transaction,[FromRoute] Guid Id )=>
{
    var transactionActual= context.Transactions.Find(Id);
    if(transactionActual!=null)
    { 
        transactionActual.CategoryId=transaction.CategoryId;
        transactionActual.Date=transaction.Date;
        transactionActual.Description=transaction.Description;
        transactionActual.Title=transaction.Title;
        transactionActual.Type= transaction.Type;
        await context.SaveChangesAsync();
        return Results.Ok();

    }
    
    return Results.NotFound();

});


app.MapGet("/api/transactions", async ([FromServices] AdminAppContext context )=>
{
    return Results.Ok(context.Transactions.Include(p=>p.Category));

});

app.MapGet("/api/categories", async ([FromServices] AdminAppContext context )=>
{
    return Results.Ok(context.Categories);

});


app.MapGet("/dbconexion", async ([FromServices] AdminAppContext context)=>
{
    context.Database.EnsureCreated();
    return Results.Ok("Base de Datos en memoria: " + context.Database.IsInMemory());
});
app.Run();
