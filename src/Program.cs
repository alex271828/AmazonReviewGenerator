var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel((options) =>
{
    options.AddServerHeader = false;
});

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

var reviewGenerator = new AmazonReviewGenerator.review.ReviewGenerator();

// init our AI using "data" folder
reviewGenerator.Init("data");

app.MapGet("/API/generate", () => { return reviewGenerator.GenerateReview(); });

app.UseStaticFiles();

app.Run();
