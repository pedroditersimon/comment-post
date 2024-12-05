namespace CommentPost.Application.Exceptions;

public class UpdateResourceException : Exception
{
	public UpdateResourceException()
	{
	}

	public UpdateResourceException(string message)
		: base(message)
	{
	}

	public UpdateResourceException(string name, object key)
	: base($"Failed to update resource \"{name}\" ({key}).")
	{
	}
}
