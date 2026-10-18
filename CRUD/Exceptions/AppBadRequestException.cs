namespace CRUD.Exceptions
{
    public class AppBadRequestException : Exception
    {
        public AppBadRequestException(string message) : base(message)
        {

        }

        public AppBadRequestException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
