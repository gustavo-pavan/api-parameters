namespace Parameters.Helper.Events.EventBus;

public class SingletonTransaction
{
    private Guid _transactionId;

    public Guid TransactionId
    {
        get
        {
            if (Guid.Empty.Equals(_transactionId))
                _transactionId = Guid.NewGuid();

            return _transactionId;
        }
    }
}