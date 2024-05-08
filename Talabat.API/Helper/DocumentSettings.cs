namespace Talabat.API.Helper
{
    public static class DocumentSettings
    {


        private static String GeryBasPath()
        {
            return "wwwroot\\Files";
        }


        public static string UploadFile(IFormFile File , string FolderName)
        {
            //folder Path = Directory + FileFolderPath + FileName
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory()
                                             , GeryBasPath() , FolderName);

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



        public static string UpdateFile(IFormFile UpdatedFile, string originalFileName , string FolderName)
        {

            DeleteFile(originalFileName, FolderName);

            return UploadFile(UpdatedFile, FolderName);

        }



        public static void DeleteFile(String FileName , string FolderName) {
        

            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), GeryBasPath() , FolderName  ,FileName);
            if (File.Exists(FilePath))
                File.Delete(FilePath);


        }

    }
}
