using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using QRCoder;
using QRCoderTests.XUnitExtenstions;
using Shouldly;
using Xunit;

namespace QRCoderTests
{
    public class QRCodeRendererTests
    {
        [Fact]
        [Category("QRRenderer/QRCode")]
        public void can_create_standard_qrcode_graphic()
        {
            var gen  = new QRCodeGenerator();
            var data = gen.CreateQrCode("This is a quick test! 123#?", QRCodeGenerator.ECCLevel.H);
            var bmp  = new QRCode(data).GetGraphic(10);

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Bmp);
            var imgBytes = ms.ToArray();
            var md5      = new MD5CryptoServiceProvider();
            var hash     = md5.ComputeHash(imgBytes);
            var result   = BitConverter.ToString(hash).Replace("-", "").ToLower();
            ms.Dispose();

            result.ShouldBe("41d3313c10d84034d67d476eec04163f");
        }


        [Fact]
        [Category("QRRenderer/QRCode")]
        public void can_create_qrcode_with_transparent_logo_graphic()
        {
            //Create QR code
            var gen  = new QRCodeGenerator();
            var data = gen.CreateQrCode("This is a quick test! 123#?", QRCodeGenerator.ECCLevel.H);
            var bmp  = new QRCode(data).GetGraphic(10, Color.Black, Color.Transparent, (Bitmap) Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "") + "\\assets\\noun_software engineer_2909346.png"));
            //Used logo is licensed under public domain. Ref.: https://thenounproject.com/Iconathon1/collection/redefining-women/?i=2909346

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            var imgBytes = ms.ToArray();
            var md5      = new MD5CryptoServiceProvider();
            var hash     = md5.ComputeHash(imgBytes);
            var result   = BitConverter.ToString(hash).Replace("-", "").ToLower();
            ms.Dispose();

            result.ShouldBe("ee65d96c3013f6032b561cc768251eef");
        }

        [Fact]
        [Category("QRRenderer/QRCode")]
        public void can_create_qrcode_with_non_transparent_logo_graphic()
        {
            //Create QR code
            var gen  = new QRCodeGenerator();
            var data = gen.CreateQrCode("This is a quick test! 123#?", QRCodeGenerator.ECCLevel.H);
            var bmp  = new QRCode(data).GetGraphic(10, Color.Black, Color.White, (Bitmap) Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "") + "\\assets\\noun_software engineer_2909346.png"));
            //Used logo is licensed under public domain. Ref.: https://thenounproject.com/Iconathon1/collection/redefining-women/?i=2909346

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            var imgBytes = ms.ToArray();
            var md5      = new MD5CryptoServiceProvider();
            var hash     = md5.ComputeHash(imgBytes);
            var result   = BitConverter.ToString(hash).Replace("-", "").ToLower();
            ms.Dispose();

            result.ShouldBe("1d718f06f904af4a46748f02af2d4eec");
        }
    }
}