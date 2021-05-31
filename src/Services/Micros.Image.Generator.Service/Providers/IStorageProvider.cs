namespace Micros.Image.Generator.Service.Providers
{
    public interface IStorageProvider
    {
        string Save(byte[] data);

        string Save(byte[] data, string fileName);
    }
}