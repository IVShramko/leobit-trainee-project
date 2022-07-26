using System;

namespace Dating.Logic.Infrastructure
{
    public interface IDirectoryUtility
    {
        string GetUserDirectory(Guid userId);
    }
}
