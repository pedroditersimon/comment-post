namespace CommentPost.Application.Filters;

public class Pagination
{
	public int Offset { get; set; } = 0;

	/// <summary>
	/// 0 = no limit </summary>
	public int Limit { get; set; } = 0;
}
