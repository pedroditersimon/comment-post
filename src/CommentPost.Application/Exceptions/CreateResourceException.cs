namespace CommentPost.Application.Exceptions;

public class CreateResourceException : Exception
{
	public CreateResourceException()
	{
	}

	public CreateResourceException(string message)
		: base(message)
	{
	}

	public CreateResourceException(string name, object key)
	: base($"Failed to create resource \"{name}\" ({key}).")
	{
	}
}
