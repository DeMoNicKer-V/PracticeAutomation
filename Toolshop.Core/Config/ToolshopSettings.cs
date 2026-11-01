public class ToolshopSettings
{
    public string UIBaseUrl { get; set; } = string.Empty;
    public string ApiBaseUrl { get; set; } = string.Empty;
    public UserCredentials Customer { get; set; } = new();


    public class UserCredentials
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public void Validate()
    {
        (string Key, string Value)[] required =
        [
            (nameof(UIBaseUrl), UIBaseUrl),
            (nameof(ApiBaseUrl), ApiBaseUrl),
            ($"{nameof(Customer)}:{nameof(Customer.Email)}", Customer.Email),
            ($"{nameof(Customer)}:{nameof(Customer.Password)}", Customer.Password),
        ];

        var missing = required
            .Where(x => string.IsNullOrWhiteSpace(x.Value))
            .Select(x => x.Key)
            .ToList();

        if (missing.Count > 0)
            throw new InvalidOperationException(
                $"Missing settings in 'ToolshopSettings': {string.Join(", ", missing)}");
    }
}