namespace Talabat.API.Helper
{
    public static class DocumentSettings
    {
        //Note :
        // We Ust The ProductPictureUrlResolver Ro Map The Images Url to be the Full Url 
        // Base Url + Folder Name + Image Name

        public static string UploadFile(IFormFile File , string FolderName)
        {
            //folder Path = Directory + FileFolderPath + FileName
            // we use GetCurrentDirectory To Get The Current Directory of the folder
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory()
                                             , "wwwroot", FolderName);

            // Make the File Name Uniqe
            string FileName = $"{Guid.NewGuid()}{File.FileName}";

            // The Full Path 
            string FilePath = Path.Combine(FolderPath, FileName);

            // Save this File As Stream (Date Per time)
            var FileStream = new FileStream(FilePath, FileMode.Create);

             File.CopyTo(FileStream);

            // We Return the file name That we want to Save it in the Databas
            string PicUrl = $"{FolderName}/{FileName}";

            return PicUrl;

        }


        public static string UpdateFile(IFormFile UpdatedFile, string originalFileName , string FolderName)
        {
            DeleteFile(originalFileName);

            return UploadFile(UpdatedFile, FolderName);
        }


        public static void DeleteFile(String FileName ) {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FileName);
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }

    }
}
