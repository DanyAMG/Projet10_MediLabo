namespace Back_Notes.Configuration
{
    public class MongoDbSettings
    {
        public string ConnectionStrings { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string NotesCollectionName { get; set; } = null!;
    }
}
