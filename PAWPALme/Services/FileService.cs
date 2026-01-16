using Microsoft.AspNetCore.Components.Forms;

namespace PAWPALme.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly long _maxFileSize = 1024 * 1024 * 5; // 5 MB

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFileAsync(IBrowserFile file, string folderName)
        {
            if (file == null) return string.Empty;

            // 1. Generate unique filename
            var extension = Path.GetExtension(file.Name);
            var fileName = $"{Guid.NewGuid()}{extension}";

            // 2. Define path: wwwroot/uploads/{folderName}
            var folderPath = Path.Combine(_environment.WebRootPath, "uploads", folderName);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var fullPath = Path.Combine(folderPath, fileName);

            // 3. Save to disk
            await using FileStream fs = new(fullPath, FileMode.Create);
            await file.OpenReadStream(_maxFileSize).CopyToAsync(fs);

            // 4. Return web-friendly URL
            return $"/uploads/{folderName}/{fileName}";
        }
    }
}