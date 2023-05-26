namespace _25may.Utilities
{
    public static class FileExtensions
    {
        public static bool CheckFileType(this IFormFile file,string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool CheckFileSize(this IFormFile file,int size) {
            return file.Length / 1024 < size;
        }
        public async static Task<string>SavefileAsync(this IFormFile file,string root,string folder)
        {
            string uniquefile=Guid.NewGuid().ToString()+"_"+file.FileName;
            string path=Path.Combine(root,folder,uniquefile);
            FileStream stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return uniquefile;
        }
    }
}
