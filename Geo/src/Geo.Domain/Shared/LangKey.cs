using CSharpFunctionalExtensions;

namespace Geo.Domain.Shared;

public record LangKey
{
	public string Key { get; private set; }
	private static List<string> _keys = new List<string>()
	{
		"de",
		"en",
		"es",
		"fr",
		"fr",
		"ja",
		"pt-BR",
		"ru",
		"zh-CN"
	};

	private LangKey(string key)
	{
		Key = key;
	}

	public static Result<LangKey> Create(string key)
	{
		if (string.IsNullOrWhiteSpace(key))
		{
			return Result.Failure<LangKey>("language key must not be empty");
		}

		if (_keys.Contains(key))
		{
			return Result.Failure<LangKey>("language key is not support");
		}
		
		return Result.Success<LangKey>(new LangKey(key));
	}
	public IReadOnlyCollection<string> AvailableLanguages => _keys;
}