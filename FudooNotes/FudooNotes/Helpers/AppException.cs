namespace FudooNotes.Helpers
{
    public class ApplicationException : Exception
    {
        public ApplicationException(): base()
        {}
        public ApplicationException(string message):base(message){}
    }
}
