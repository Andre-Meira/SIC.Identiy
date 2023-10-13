using Microsoft.Extensions.Options;
using MongoDB.Driver;
using User.Infrastructure.Models;

namespace User.Infrastructure;

internal sealed class AuditDomainService 
{
    private readonly IMongoCollection<AuditStoreDatabase> _auditStoreCollection;

    public AuditDomainService(IOptions<AuditStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _auditStoreCollection = mongoDatabase.GetCollection<AuditStoreDatabase>
            (bookStoreDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task CreateAsync(AuditStoreDatabase auditStore) 
        => await _auditStoreCollection.InsertOneAsync(auditStore);
}
