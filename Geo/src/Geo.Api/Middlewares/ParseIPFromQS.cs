namespace Geo.Api.Middlewares
{
	public class ParseIPFromQS
	{
		private readonly RequestDelegate _next;

		public ParseIPFromQS(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Query.TryGetValue("ip", out var ipValues))
			{
				string ip = ipValues.ToString();
				string sanitized = new(ip.Where(c => Char.IsDigit(c) || c == '.').ToArray());

				if (sanitized != ip)
				{
					var newQuery = string.Join('&', context.Request.Query
						.Select(q => q.Key == "ip" ? $"ip={sanitized}" : $"{q.Key}={q.Value}"));

					context.Response.Redirect(
						$"{context.Request.Path}?{newQuery}", true);
					return;
				}
			}

			await _next.Invoke(context);
		}

	}
}
