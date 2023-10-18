using Microsoft.Extensions.Options;
using MongoDB.Driver;
using User.Infrastructure.Abstract;
using User.Infrastructure.Models;

namespace User.Infrastructure;

internal sealed class AuditDomainService : IAuditDomainService
{
    private readonly IMongoCollection<AuditStoreDatabase> _auditStoreCollection;

    public AuditDomainService(IOptions<AuditStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        MongoClient mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);

        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);

        _auditStoreCollection = mongoDatabase.GetCollection<AuditStoreDatabase>
            (bookStoreDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task CreateAsync(AuditStoreDatabase auditStore, CancellationToken cancellation = default) 
        => await _auditStoreCollection.InsertOneAsync(auditStore, cancellationToken:cancellation);
}
