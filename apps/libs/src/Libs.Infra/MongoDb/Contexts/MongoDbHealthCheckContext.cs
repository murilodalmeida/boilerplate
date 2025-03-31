using MongoDB.Driver;

namespace FwksLabs.Libs.Infra.MongoDb.Contexts;

public sealed class MongoDbHealthCheckContext
{
    public MongoDbHealthCheckContext(string connectionString)
    {
        var mongoUrl = MongoUrl.Create(connectionString);

        DatabaseName = mongoUrl.DatabaseName;
        Client = new MongoClient(mongoUrl);
    }

    public int MaxRetries { get; } = 2;
    public bool IsAdmin { get; init; } = true;
    public string DatabaseName { get; }
    public IMongoClient Client { get; }
}