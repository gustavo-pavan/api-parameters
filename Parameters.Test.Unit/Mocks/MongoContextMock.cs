namespace Parameters.Test.Unit.Mocks;

public class MongoContextMock
{
    public static Mock<IMongoContext> Mock<T>(IEnumerable<T> accounts) where T : BaseEntity
    {
        var cursorMock = new Mock<IAsyncCursor<T>>();
        var mongoContextMock = new Mock<IMongoContext>();
        var mongoCollectionMock = new Mock<IMongoCollection<T>>();

        cursorMock.Setup(_ => _.Current).Returns(accounts);

        cursorMock
            .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true)
            .Returns(false);

        cursorMock
            .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true))
            .Returns(Task.FromResult(false));

        mongoCollectionMock.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<T>>(),
                It.IsAny<FindOptions<T, T>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursorMock.Object);

        mongoContextMock.Setup(x => x.AddCommand(It.IsAny<T>(), It.IsAny<Func<Task>>()));

        mongoContextMock.Setup(x => x.GetCollection<T>(It.IsAny<string>()))
            .Returns(mongoCollectionMock.Object);
        return mongoContextMock;
    }
}