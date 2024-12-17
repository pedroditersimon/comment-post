namespace CommentPost.Application.Exceptions;

public class ResourceConflictException : Exception
{
	public ResourceConflictException()
		: base("A conflict occurred with the resource.") { }

	public ResourceConflictException(string message)
		: base(message) { }

	public ResourceConflictException(string message, Exception innerException)
		: base(message, innerException) { }
}
