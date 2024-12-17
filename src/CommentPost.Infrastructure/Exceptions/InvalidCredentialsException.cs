namespace CommentPost.Application.Exceptions;

public class InvalidCredentialsException : Exception
{
	public InvalidCredentialsException()
		: base("The provided credentials are invalid.") { }

	public InvalidCredentialsException(string message)
		: base(message) { }

	public InvalidCredentialsException(string message, Exception innerException)
		: base(message, innerException) { }
}
