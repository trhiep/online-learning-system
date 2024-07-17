using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.DataProtection;

namespace OnlineLearningSystem.Utils.FileUploader
{
    public class CloudinaryUploader
    {
        private static CloudinaryServiceAccount serviceAccount;

        public CloudinaryUploader()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            // Bind the configuration to the MailServiceAccount class
            serviceAccount = new CloudinaryServiceAccount();
            configuration.GetSection("MailServiceAccount").Bind(serviceAccount);
        }

        public static string Upload(IFormFile file)
        {
            try
            {
                // Verify Cloudinary account
                var account = new Account(serviceAccount.CloudName, serviceAccount.ApiKey, serviceAccount.ApiSecret);
                Cloudinary cloudinary = new Cloudinary(account);

                // Tạo một unique filename cho file
                string uniqueFilename = Guid.NewGuid().ToString();

                // Đường dẫn lưu trữ ảnh trên Cloudinary
                string cloudinaryPath = $"OLS/{uniqueFilename}";

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
