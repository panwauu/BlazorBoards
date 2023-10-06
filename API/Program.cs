var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var labels = new List<Label>() { new Label("prio1", "#000000", "#AA2200") };

app.MapGet("/labels", () =>
{
    return labels;
})
.WithName("GetLabels")
.WithOpenApi();

app.MapPost("/labels", (Label label) =>
{
    labels.Add(label);
    return labels;
})
.WithName("PostLabels")
.WithOpenApi();

app.MapDelete("/labels", (Label label) =>
{
    labels.Remove(label);
    return labels;
})
.WithName("RemoveLabels")
.WithOpenApi();

app.Run();
