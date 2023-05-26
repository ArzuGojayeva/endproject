namespace Task.Utilities
{
    public static class FileExtension
    {
        public static bool CheckFileType(this IFormFile file,string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool CheckFileSize(this IFormFile file,int size) {
            return file.Length / 1024 < size;
        }
        public static async Task<string>SaveFileasync(this IFormFile file,string folder,string root)
        {
            string uniquefile=Guid.NewGuid().ToString()+"_"+file.FileName;
            string path=Path.Combine(root,folder,uniquefile);
            FileStream stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return uniquefile;
        }
    }
}
