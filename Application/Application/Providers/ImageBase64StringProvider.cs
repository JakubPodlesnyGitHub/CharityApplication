namespace Application.Providers
{
    public static class ImageBase64StringProvider
    {
        public static string ProvideBase64(string imagePath)
        {
            byte[] imageArray = File.ReadAllBytes(imagePath);
            return $"data:image/png;base64,{Convert.ToBase64String(imageArray)}";
        }
    }
}