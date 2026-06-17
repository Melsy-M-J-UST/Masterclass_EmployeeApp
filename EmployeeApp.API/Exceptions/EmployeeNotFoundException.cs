namespace EmployeeApp.API.Exceptions
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException(string Message): base(Message) { }
    }
}
