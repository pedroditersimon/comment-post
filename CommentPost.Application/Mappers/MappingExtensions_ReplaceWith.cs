namespace CommentPost.Application.Mappers;

public static partial class MappingExtensions
{
	// Generic method that replaces values
	public static TModel ReplaceWith<TModel, TReplace>(this TModel original, TReplace replaceModel, bool skipNullValues = false)
		where TModel : ICloneable
	{
		if (original == null || replaceModel == null)
			throw new NullReferenceException();

		TModel originalClone = (TModel)original.Clone();

		// Gets the properties of both models
		var originalProperties = typeof(TModel).GetProperties();
		var replaceProperties = typeof(TReplace).GetProperties();

		foreach (var replaceProp in replaceProperties)
		{
			// Attempts to get the property from the original model
			var originalProp = Array.Find(originalProperties,
				p => p.Name == replaceProp.Name);

			if (originalProp != null)
			{
				// Gets the replacement value
				var replaceValue = replaceProp.GetValue(replaceModel);

				// skip null replace values
				if (skipNullValues && replaceValue == null)
					continue;

				originalProp.SetValue(originalClone, replaceValue);
			}
		}

		// Returns the original model with replaced values
		return originalClone;
	}
}
