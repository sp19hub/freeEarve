namespace freeArve.util
{
    internal class ValidationException : Exception
    {
        public ValidationException() :base() { }
        public ValidationException(string msg) :base(msg) { }
    }
}
