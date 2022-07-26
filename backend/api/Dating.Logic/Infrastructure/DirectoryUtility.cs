using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Dating.Logic.Infrastructure
{
    public class DirectoryUtility : IDirectoryUtility
    {
        private readonly IConfiguration _configurtion;

        public DirectoryUtility(IConfiguration configurtion)
        {
            _configurtion = configurtion;
        }

        public string GetUserDirectory(Guid userId)
        {
            string storageDirectory =
                _configurtion.GetSection("Directories")["StorageDirectory"];

            string userPath = Path.Combine(storageDirectory, userId.ToString());

            return userPath;
        }
    }
}
