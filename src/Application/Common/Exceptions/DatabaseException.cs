namespace Application.Common.Exceptions;

public class DatabaseException : Exception
{
	public DatabaseException() : base("Database error")
	{
	}
}
