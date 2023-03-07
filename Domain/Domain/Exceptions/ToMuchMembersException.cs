namespace Domain.Exceptions
{
    public class ToMuchMembersException : BaseException
    {
        public ToMuchMembersException()
        {
        }

        public ToMuchMembersException(string message)
            : base(message)
        {
        }

        public ToMuchMembersException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}