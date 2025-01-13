namespace SchoolSystem.IServices
{
    public interface IFileService
    {
        /// <summary>
        /// Saves an uploaded file to the server.
        /// </summary>
        /// <param name="file">The file to save.</param>
        /// <param name="fileName">The name to save the file as.</param>
        /// <returns>The relative path to the saved file.</returns>
        Task<string> SaveFileAsync(IFormFile file, string fileName);

        /// <summary>
        /// Deletes a file from the server.
        /// </summary>
        /// <param name="filePath">The relative path of the file to delete.</param>
        /// <returns>True if the file was deleted, false otherwise.</returns>
        Task<bool> DeleteFileAsync(string filePath);
    }
}
