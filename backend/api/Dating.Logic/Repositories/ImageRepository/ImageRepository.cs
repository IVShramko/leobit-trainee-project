using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.ImageRepository
{
    public class ImageRepository : IImageRepository
    {
        public Guid AddPhoto(string DataUrlString, Guid UserId)
        {
            var base64Data = Regex.Match(DataUrlString, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;

            var photobytes = Convert.FromBase64String(base64Data);

            string root = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "/img";

            string path = Directory.CreateDirectory(root + "/" + UserId).FullName;

            Guid id = Guid.NewGuid();

            File.WriteAllBytes(path + "/" + id, photobytes);

            return id;
        }

        public void DeletePhotoById(Guid id)
        {
            throw new NotImplementedException();
        }

        public string[] GetAllUserPhotos(Guid userId)
        {
            throw new NotImplementedException();
        }

        public string GetPhotoById(Guid id, Guid userId)
        {
            string folderPath = Path.Combine(
                Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
                "img",
                userId.ToString());

            string filePath = Path.Combine(folderPath, id.ToString());

            try
            {
                var photobytes = File.ReadAllBytes(filePath);
                
                return Convert.ToBase64String(photobytes);
            }
            catch (FileNotFoundException)
            {
                return null;
            }

        }
    }
}
