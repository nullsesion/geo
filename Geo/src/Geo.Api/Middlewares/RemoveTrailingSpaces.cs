namespace Geo.Api.Middlewares
{
	public class RemoveTrailingSpaces
	{
		private readonly RequestDelegate _next;

		public RemoveTrailingSpaces(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			string qs = context.Request.QueryString.ToString();
			if (qs.EndsWith("%20"))
				context.Response.Redirect(
					qs.Substring(0, qs.LastIndexOf("%20", StringComparison.Ordinal))
					, true
				);
			else
				await _next.Invoke(context);
		}

	}
}
