using CommentPost.Domain.Enums;

namespace CommentPost.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,
	AllowMultiple = false, Inherited = false)]
public class NeedAuthorizationAttribute : Attribute
{
	public Role[] Roles { get; } = Array.Empty<Role>();

	/// <summary>
	/// The user must be logged in, and any role will be allowed to access the resource. </summary>
	public NeedAuthorizationAttribute() { }

	/// <summary>
	/// Only the specified role will be allowed to access the resource. </summary>
	/// <param name="role">The role required to access the resource.</param>
	public NeedAuthorizationAttribute(Role role)
	{
		Roles = new Role[] { role };
	}

	/// <summary>
	/// Only the specified roles will be allowed to access the resource. </summary>
	/// <param name="roles">The roles allowed to access the resource.</param>
	public NeedAuthorizationAttribute(params Role[] roles)
	{
		Roles = roles;
	}
}
