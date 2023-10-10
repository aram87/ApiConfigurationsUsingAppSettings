namespace ApiConfigurationsUsingAppSettings.Models
{
    public class ApiSettingsModel
    {
        public bool? TestingEndpointEnabled { get; set; }
        public string? ApiKeyHash { get; set; }
        public int PbKDF2IterationsCount { get; set; }
    }
}