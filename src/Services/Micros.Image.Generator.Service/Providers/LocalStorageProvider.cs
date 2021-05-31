using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Micros.Image.Generator.Service.Providers
{
    public class LocalStorageProvider : IStorageProvider
    {
        private readonly ILogger<LocalStorageProvider> _logger;
        private static readonly string StorageDir = Environment.GetEnvironmentVariable("STORAGE_DIR");

        public LocalStorageProvider(ILogger<LocalStorageProvider> logger)
        {
            _logger = logger;
        }

        public string Save(byte[] data)
        {
            return Save(data, "file");
        }

        public string Save(byte[] data, string fileName)
        {
            CreateStorageDirectory();
            var filePath = Path.Combine(StorageDir, $"{fileName}.jpg");
            try
            {
                File.WriteAllBytes(filePath, data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Can't save the file [{filePath}]");
            }

            return filePath;
        }

        private void CreateStorageDirectory()
        {
            try
            {
                if (Directory.Exists(StorageDir))
                {
                    _logger.LogInformation($"Storage Directory [{StorageDir}] exists already.");
                    return;
                }

                Directory.CreateDirectory(StorageDir);
                _logger.LogInformation("The directory was created successfully at {0}.", Directory.GetCreationTime(StorageDir));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Storage Directory {StorageDir} wasn't created.");
            }
        }
    }
}