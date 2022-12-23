namespace Application.Common.Exceptions;

public class DuplicateException : Exception
{
	public DuplicateException(string entity) 
		: base($"{entity} already exists.")
	{
	}
}
