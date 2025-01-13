namespace HirePlatform.Helpers
{
    public class DocumentSettings
    {
        public static async Task<string> UploadFile(IFormFile file, string FolderName)
        {
            if (file == null)
                return String.Empty;
            //D:\projects\C# projects\Core Projects\Company2Project\MVCFirstProject
            //string folderpath = Directory.GetCurrentDirectory() + "\\wwwroot\\" + FolderName;
            // string folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);

            string filename = $"{Guid.NewGuid()}{file.FileName}";
            string filepath = Path.Combine(folderpath, filename);
            using var fs = new FileStream(filepath, FileMode.Create);
            await file.CopyToAsync(fs);
            return filename;
        }
        public async static Task DeleteFile(string? FileName, string FolderName)
        {
            if (FileName is not null && FolderName is not null)
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName, FileName);
                if (File.Exists(filepath))
                    File.Delete(filepath);
            }
        }
    }
}
