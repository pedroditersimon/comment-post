namespace CommentPost.Application.Exceptions;

public class SaveChangesException : Exception
{
	public SaveChangesException()
	{
	}

	public SaveChangesException(string message)
		: base(message)
	{
	}

}
