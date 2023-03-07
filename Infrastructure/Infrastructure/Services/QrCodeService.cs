using Application.Interfaces.Services;
using CharityApplication.Shared.Dtos.ServiceDtos.Responses;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace Infrastructure.Services
{
    public class QrCodeService : IQrCodeService
    {
        public QrCodeDTO GetBase64QrCodeString(string URL)
        {
            string qrCodeString = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(URL, QRCodeGenerator.ECCLevel.Q);
                QRCode qRCode = new QRCode(qRCodeData);
                using (Bitmap bitmap = qRCode.GetGraphic(5))
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    qrCodeString = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return new QrCodeDTO { QrCodeBase64 = qrCodeString };
        }
    }
}