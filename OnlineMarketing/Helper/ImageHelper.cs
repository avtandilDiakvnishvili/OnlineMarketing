namespace OnlineMarketing.Helper
{

    public static class ImageHelper
    {

        public static string SaveDistributorImage(byte[] imageBytes, IWebHostEnvironment env)
        {

            var path = Path.Combine(env.ContentRootPath, "Images");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string filename = $"{DateTime.Now.Millisecond}.jpg";

            if (imageBytes != null && imageBytes.Length > 0)
            {
                try
                {
                    File.WriteAllBytes(Path.Combine(path, filename), imageBytes);
                    return Path.Combine(filename);

                }
                catch (Exception e)
                {
                    var s = e.Message;
                    filename = string.Empty;
                    return string.Empty;
                }
            }
            return string.Empty;

        }

        public static void DeleteDistributorImage(IWebHostEnvironment env, string file_name)
        {
            String path = Path.Combine(env.ContentRootPath, "Images", file_name);
            try
            {
                var image = Directory.GetFiles(path, file_name);
                foreach (var filePath in image)
                {
                    File.Delete(filePath);
                }
            }
            catch { }
        }
    }
}
