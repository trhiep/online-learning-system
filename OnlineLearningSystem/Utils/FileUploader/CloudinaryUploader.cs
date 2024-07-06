using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace OnlineLearningSystem.Utils.FileUploader
{
    public class CloudinaryUploader
    {
        private const string CloudName = "duzrv35z5";
        private const string ApiKey = "167631264475554";
        private const string ApiSecret = "_jXx1bhbSmznVkr50_BbnkmxZXw";

        public static string Upload(IFormFile file)
        {
            try
            {
                // Khởi tạo Cloudinary
                Account account = new Account(CloudName, ApiKey, ApiSecret);
                Cloudinary cloudinary = new Cloudinary(account);

                // Tạo một unique filename cho file
                string uniqueFilename = Guid.NewGuid().ToString();

                // Đường dẫn lưu trữ ảnh trên Cloudinary
                string cloudinaryPath = $"Uploader/{uniqueFilename}";

                // Tạo upload parameters
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    PublicId = cloudinaryPath,
                    Overwrite = true,
                };

                // Upload file lên Cloudinary
                var uploadResult = cloudinary.Upload(uploadParams);

                // Xử lý kết quả upload (uploadResult)
                // Ví dụ: Lấy URL của file sau khi upload
                string fileUrl = uploadResult.Uri.ToString();

                // Đoạn mã xử lý sau khi upload thành công
                Console.WriteLine("Upload success!");

                return fileUrl;
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
    }
}
