namespace Domain.Exceptions
{
    public class NotFoundRecordException : BaseException
    {
        public NotFoundRecordException()
        {
        }

        public NotFoundRecordException(string message)
            : base(message)
        {
        }

        public NotFoundRecordException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}