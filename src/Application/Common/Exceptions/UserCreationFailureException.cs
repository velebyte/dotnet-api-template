namespace Application.Common.Exceptions;

public class UserCreationFailureException : Exception
{
    public UserCreationFailureException() : base("User failed to create")
    {
    }
}