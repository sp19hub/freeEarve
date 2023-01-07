namespace freeArve.util
{
    internal class InvalidBankAccountException : Exception
    {
        public InvalidBankAccountException() :base() { }
        public InvalidBankAccountException(string msg) :base(msg) { }
    }
}
