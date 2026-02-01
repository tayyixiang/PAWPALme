using Microsoft.AspNetCore.Components.Forms;

namespace PAWPALme.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFileAsync(IBrowserFile file, string folderName)
        {
            try
            {
                // 1. Safe Directory Creation
                // Ensures wwwroot/uploads/shelters exists
                var uploadPath = Path.Combine(_environment.WebRootPath, "uploads", folderName);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // 2. Unique Filename
                // Prevents overwriting if two users upload "profile.jpg"
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.Name);
                var filePath = Path.Combine(uploadPath, fileName);

                // 3. THE CRITICAL FIX: Max Size Limit
                // Default is 512KB. We set it to 5MB (5 * 1024 * 1024).
                long maxFileSize = 5 * 1024 * 1024;

                using (var stream = file.OpenReadStream(maxFileSize))
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                }

                // 4. Return Web-Accessible URL
                return $"/uploads/{folderName}/{fileName}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Upload Error] {ex.Message}");
                return null; // Return null so the UI knows it failed
            }
        }
    }
}