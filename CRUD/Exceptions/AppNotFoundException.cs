namespace CRUD.Exceptions
{
    public class AppNotFoundException : Exception
    {

        public AppNotFoundException(string message) : base(message)
        {

        }

        public AppNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
