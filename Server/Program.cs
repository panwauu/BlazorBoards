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


var data = new BlazorBoardData();
data.Labels.Add(new Label("Label 1", "#000000", "#AAAA00") { });
data.Boards.Add(new Board("In progress") { });
data.Boards.Add(new Board("Backlog") { });
data.Boards.Add(new Board("Done") { });
data.Boards[0].AddTask(new TaskItem("Task 1") { });
data.Boards[0].AddTask(new TaskItem("Task 2") { });
data.Boards[1].AddTask(new TaskItem("Task 1") { });
data.Boards[1].AddTask(new TaskItem("Task 2") { });
data.Boards[0].Tasks[0].Labels.Add("Label 1");

app.MapGet("/api/data", () =>
{
    return TypedResults.Ok(data);
})
.WithName("GetData")
.WithOpenApi();

app.MapPut("/api/data", (BlazorBoardData recievedData) =>
{
    data = recievedData;
    return TypedResults.Ok();
})
.WithName("PostData")
.WithOpenApi();

app.Run();
