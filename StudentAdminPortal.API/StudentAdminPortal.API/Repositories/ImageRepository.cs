using StudentAdminPortal.API.Repositories.Interfaces;
using System.IO;

namespace StudentAdminPortal.API.Repositories
{
    public class ImageRepository : IImageRepository
    {
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            //Upload Img To stream
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);

            //file to stream 

            using Stream filestream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(filestream);

            return GetImgServerPath(fileName);





        }

        // get specific file path in the server or Api 
        private string GetImgServerPath(string fileName)
        {
            return Path.Combine(@"Resources\Images", fileName);
        }
    }
}
