namespace Shared.Encryptions
{
    public interface IEncryptionService
    {
        string HashingPassword(string password, byte[] salt);
        byte[] GenerateSalt();
    }
}
