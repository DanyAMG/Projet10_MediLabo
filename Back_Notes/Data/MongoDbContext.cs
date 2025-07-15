using Back_Notes.Configuration;
using Back_Notes.Model;
using MongoDB.Driver;

namespace Back_Notes.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var settings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            var client = new MongoClient(settings.ConnectionStrings);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<Note> Notes => _database.GetCollection<Note>("Notes");

    }
}
