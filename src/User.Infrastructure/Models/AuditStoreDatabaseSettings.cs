namespace User.Infrastructure.Models;

internal record AuditStoreDatabaseSettings
{
    public string? ConnectionString { get; set; }

    public string? DatabaseName { get; set; }

    public string? BooksCollectionName { get; set; }

    public AuditStoreDatabaseSettings()
    {
        
    }

    public AuditStoreDatabaseSettings(
        string connectionString, 
        string databaseName, 
        string booksCollectionName)
    {
        ConnectionString = connectionString;
        DatabaseName = databaseName;
        BooksCollectionName = booksCollectionName;
    }
}
