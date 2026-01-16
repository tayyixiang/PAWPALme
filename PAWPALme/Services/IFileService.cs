using Microsoft.AspNetCore.Components.Forms;

namespace PAWPALme.Services
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IBrowserFile file, string folderName);
    }
}
