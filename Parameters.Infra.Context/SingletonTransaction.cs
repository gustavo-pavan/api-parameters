namespace Parameters.Infra.Context;

public static class SingletonTransaction
{
    private static Guid _transactionId;
    private static readonly object _object = new();

    public static Guid TransactionId
    {
        get
        {
            lock (_object)
            {
                if (Guid.Empty.Equals(_transactionId))
                    _transactionId = Guid.NewGuid();

                return _transactionId;
            }
        }
    }
}