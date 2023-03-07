namespace Domain.Exceptions
{
    public class InsufficientNumberEventParticipantsException : Exception
    {
        public InsufficientNumberEventParticipantsException()
        {
        }

        public InsufficientNumberEventParticipantsException(string message)
            : base(message)
        {
        }

        public InsufficientNumberEventParticipantsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}