using CommentPost.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.API.Controllers;

[ApiController]
[Route("comments")]
public class CommentController
{

	[HttpGet("{id}")]
	public ActionResult<Comment> Get(int id)
	{
		return new Comment() { ID = id, Text = "comment text" };
	}
}
