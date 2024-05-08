namespace Talabat.API.Helper
{
    public static class DocumentSettings
    {

        public static string UploadFile(IFormFile File , string FolderName)
        {
            //folder Path = Directory + FileFolderPath + FileName
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory()
                                             , "wwwroot\\Files", FolderName);

            // Make the File Name Uniqe
            string FileName = $"{Guid.NewGuid()}{File.FileName}";


            // The Full Path 
            string FilePath = Path.Combine(FolderPath, FileName);


            // Save this File As Stream (Date Per time)
            var FileStream = new FileStream(FilePath, FileMode.Create);


             File.CopyTo(FileStream);

            // We Return the file name That we want to Save it in the Database
            return FileName;


        }

        public static void DeleteFile(String FileName , string FolderName) {
        

            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName  ,FileName);
            if (File.Exists(FilePath))
                File.Delete(FilePath);


        }

    }
}
