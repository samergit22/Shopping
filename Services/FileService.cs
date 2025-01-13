using SchoolSystem.IServices;

namespace SchoolSystem.Services
{
    public class FileService : IFileService
    {
        private readonly string _uploadPath;

        public FileService(IWebHostEnvironment environment)
        {
            // Define the folder where files will be saved (e.g., "wwwroot/uploads")
            _uploadPath = Path.Combine(environment.WebRootPath, "uploads");
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath); // Create the directory if it doesn't exist
            }
        }

        /// <summary>
        /// Saves an uploaded file to the server.
        /// </summary>
        /// <param name="file">The file to save.</param>
        /// <param name="fileName">The name to save the file as.</param>
        /// <returns>The relative path to the saved file.</returns>
        public async Task<string> SaveFileAsync(IFormFile file, string fileName)
        {
            if (file.Length <= 0)
                throw new ArgumentException("File is empty.");

            if (file.Length > 10 * 1024 * 1024) 
                throw new ArgumentException("File is too large.");

            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };

            var filePath = Path.Combine(_uploadPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"/uploads/{fileName}"; // Return the relative path for use in the application
        }

        /// <summary>
        /// Deletes a file from the server.
        /// </summary>
        /// <param name="filePath">The relative path of the file to delete.</param>
        /// <returns>True if the file was deleted, false otherwise.</returns>
        public async Task<bool> DeleteFileAsync(string filePath)
        {
            try
            {
                var absolutePath = Path.Combine(_uploadPath, Path.GetFileName(filePath));
                if (File.Exists(absolutePath))
                {
                    File.Delete(absolutePath);
                    return true;
                }
                return false;
            }
            catch
            {
                // Log the exception (optional)
                return false;
            }
        }
    }
}
