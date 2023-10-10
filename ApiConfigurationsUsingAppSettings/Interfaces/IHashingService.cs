namespace ApiConfigurationsUsingAppSettings.Interfaces
{
    public interface IHashingService
    {
        string HashUsingPbkdf2(byte[] password);
        string ComputeHmacUsingSha256(byte[] data);
    }
}