using Microsoft.AspNetCore.Http.HttpResults;
using Common.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

var labels = new List<Label>() { new Label("prio1", "#000000", "#AA2200") };

app.MapGet("/api/labels", () =>
{
    return TypedResults.Ok(labels);
})
.WithName("GetLabels")
.WithOpenApi();

app.MapPut("/api/labels", (List<Label> recievedLabels) =>
{
    labels = recievedLabels;
    return TypedResults.Ok();
})
.WithName("PostLabels")
.WithOpenApi();

app.MapDelete("/api/labels/{id}", Results<Ok, NotFound> (string id) =>
{
    var labelToRemove = labels.Find(l => l.Id == id);
    if (labelToRemove == null) { return TypedResults.NotFound(); }

    labels.Remove(labelToRemove);
    return TypedResults.Ok();
})
.WithName("RemoveLabels")
.WithOpenApi();

app.Run();
