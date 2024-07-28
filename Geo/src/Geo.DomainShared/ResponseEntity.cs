namespace Geo.DomainShared
{
	public class ResponseEntity<T>
	{
		public bool IsSuccess { get; set; } = true;
		public string ErrorDetail { get; set; } = string.Empty;
		public T? Entity { get; set; }
	}
}
