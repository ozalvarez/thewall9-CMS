using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.bll.Exceptions;

namespace thewall9.bll.Utils
{
    public class ImageUtil
    {
        const string ExpectedImagePrefix = "data:image/jpeg;base64,";
        public static Image StringToImage(string InputString)
        {
            byte[] imageBytes = Encoding.Unicode.GetBytes(InputString);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true, true);
            return image;
        }
        public static bool IsBase64(string InputString){
            return InputString.Split(',').Length == 2;
        }
        public static Stream StringToStream(string InputString)
        {
            string base64 = InputString.Split(',')[1];
            byte[] imageBytes = Convert.FromBase64String(base64);

            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            ms.Write(imageBytes, 0, imageBytes.Length);

            return ms;

        }
        public static Stream StringToStreamNo64(string InputString)
        {

            byte[] imageBytes = Encoding.Unicode.GetBytes(InputString);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            ms.Write(imageBytes, 0, imageBytes.Length);

            return ms;

        }
        public static bool IsImagen(System.IO.Stream stream, String fileName)
        {
            try
            {
                using (Image img = Image.FromStream(stream, false, false))
                {
                    if (fileName.ToLower().IndexOf(".jpg") > 0)
                        return true;
                    if (fileName.ToLower().IndexOf(".gif") > 0)
                        return true;
                    if (fileName.ToLower().IndexOf(".png") > 0)
                        return true;
                }
            }
            catch (ArgumentException) { }
            return false;
        }
    }
}
