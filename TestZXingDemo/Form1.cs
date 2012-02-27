using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using com.google.zxing;
using com.google.zxing.client.result;
using com.google.zxing.common;
using com.google.zxing.qrcode;
using ECL = com.google.zxing.qrcode.decoder.ErrorCorrectionLevel;
using ErrorCorrectionLevel = com.google.zxing.qrcode.decoder.ErrorCorrectionLevel;
using com.google.zxing.microqrcode;

namespace TestZXingDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="format"></param>
        /// <param name="file"></param>
        public static void writeToFile(ByteMatrix matrix, ImageFormat format, string file)
        {
            Bitmap bmap = toBitmap(matrix);
            bmap.Save(file, format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Bitmap toBitmap(ByteMatrix matrix)
        {
            return matrix.ToBitmap();
            //int width = matrix.Width;
            //int height = matrix.Height;
            //Bitmap bmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            //for (int x = 0; x < width; x++)
            //{
            //    for (int y = 0; y < height; y++)
            //    {
            //        bmap.SetPixel(x, y, matrix.get_Renamed(x, y) != -1 ? Color.Black : Color.White);
            //    }
            //}
            //return bmap;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGen_Click(object sender, EventArgs e)
        {
            CreateQRCode("12345", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.L } });
            //CreateQRCode("http://www.ahau.edu.cn/net/user", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.H } });
            //CreateQRCode("http://zhq.qidian.com", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.H } });
            //CreateQRCode("978-7-115-22390-6", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.H } });
            //CreateQRCode("ISBN 978-7-115-22390-6", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.H } });
            //CreateQRCode("tupunco@163.com", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.H } });
            //CreateQRCode("tel:13795555555", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.H } });
            //CreateQRCode("ABCDEFGHIRTKGGN%^^&*&(*)(_)><KHHJHJJJKKK", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.L} });
            //CreateQRCode("ｌａｋｓｆｌｋｓａｄｏ３９０２３０９２３０９２３ｕｉｃｃｎｚｋｊＦＧＪＫＫＬＭＫＦＧＹＫＪＬＫ）（＿）＊（％＾％＄＄＃＠＃＠！～", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.H } });
            //CreateQRCode("ABCDEFGHIRTKGGN%^^&*&(*)(_)><KHHJHJJJKKK", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.H } });
            //CreateQRCode("赵海强", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.H } });
            //CreateQRCode("赵海强赵海强赵海强", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.H } });
            //CreateQRCode("HelloWorld赵海强赵海强赵海强赵海强", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.H } });
            //CreateQRCode("赵海强赵海强赵海强赵海强赵海强HelloWorld", new Hashtable() { { EncodeHintType.ERROR_CORRECTION, ECL.H } });
            //Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGenM3_Click(object sender, EventArgs e)
        {

            CreateMicroQRCode("111560066", new Hashtable() { { EncodeHintType.MICROQRCODE_VERSION, 3 }, 
                                                        { EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L } });
            CreateMicroQRCode("111560066", new Hashtable() { { EncodeHintType.MICROQRCODE_VERSION, 3 }, 
                                                        { EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.M } });
            CreateMicroQRCode("111560066", new Hashtable() { { EncodeHintType.MICROQRCODE_VERSION, 2 }, 
                                                        { EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L } });

            //var str = "012345679012345679012345679012345679";
            //for (int i = 1; i < str.Length; i++)
            //    CreateMicroQRCodeTest(str.Substring(0, i));

            //var str2 = "123456790123456790123456790123456790";
            //for (int i = 1; i < str2.Length; i++)
            //    CreateMicroQRCodeTest(str2.Substring(0, i));

            //Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        private static void CreateMicroQRCodeTest(string content)
        {
            if (string.IsNullOrEmpty(content))
                return;

            var lenArray = new int[] { 5, 10, 8, 23, 18, 35, 30, 21 };//数字位数
            var len = content.Length;
            //1
            if (len <= lenArray[0])
            {
                CreateMicroQRCode(content, new Hashtable() { { EncodeHintType.MICROQRCODE_VERSION, 1 }, 
                                                        { EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L } });
            }

            //2
            if (len <= lenArray[1])
            {
                CreateMicroQRCode(content, new Hashtable() { { EncodeHintType.MICROQRCODE_VERSION, 2 }, 
                                                        { EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L } });
            }
            if (len <= lenArray[2])
            {
                CreateMicroQRCode(content, new Hashtable() { { EncodeHintType.MICROQRCODE_VERSION, 2 }, 
                                                        { EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.M } });
            }

            //3
            if (len <= lenArray[3])
            {
                CreateMicroQRCode(content, new Hashtable() { { EncodeHintType.MICROQRCODE_VERSION, 3 }, 
                                                        { EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L } });
            }
            if (len <= lenArray[4])
            {
                CreateMicroQRCode(content, new Hashtable() { { EncodeHintType.MICROQRCODE_VERSION, 3 }, 
                                                        { EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.M } });
            }

            //4
            if (len <= lenArray[5])
            {
                CreateMicroQRCode(content, new Hashtable() { { EncodeHintType.MICROQRCODE_VERSION, 4 }, 
                                                        { EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L } });
            }
            if (len <= lenArray[6])
            {
                CreateMicroQRCode(content, new Hashtable() { { EncodeHintType.MICROQRCODE_VERSION, 4 }, 
                                                        { EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.M } });
            }
            if (len <= lenArray[7])
            {
                CreateMicroQRCode(content, new Hashtable() { { EncodeHintType.MICROQRCODE_VERSION, 4 }, 
                                                        { EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.Q } });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="hint"></param>
        private static void CreateMicroQRCode(string content, Hashtable hint)
        {
            ByteMatrix byteMatrix = new MultiFormatWriter().encode(content, BarcodeFormat.MICRO_QR_CODE, 17, 17, hint);
            writeToFile(byteMatrix, ImageFormat.Png, Guid.NewGuid().ToString() + ".png");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        private static void CreateQRCode(string content, Hashtable hint)
        {
            ByteMatrix byteMatrix = new MultiFormatWriter().encode(content, BarcodeFormat.QR_CODE, 210, 210, hint);
            writeToFile(byteMatrix, ImageFormat.Png, Guid.NewGuid().ToString() + ".png");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDecodeSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialogDecode.ShowDialog() == DialogResult.OK)
                this.textBoxDecodeFilePath.Text = openFileDialogDecode.FileName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDecode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBoxDecodeFilePath.Text))
                return;

            var image = new Bitmap(this.textBoxDecodeFilePath.Text);
            if (image == null)
                return;

            var source = new RGBLuminanceSource(image, image.Width, image.Height);
            BinaryBitmap bitmap = new BinaryBitmap(new HybridBinarizer(source));

            //Result result = new MultiFormatReader().decode(bitmap, new Hashtable() { { BarcodeFormat.QR_CODE, "" }, { DecodeHintType.TRY_HARDER, true } });
            Result result = new MicroQRCodeReader().decode(bitmap, new Hashtable() { 
            //Result result = new QRCodeReader().decode(bitmap, new Hashtable() { 
                { DecodeHintType.PURE_BARCODE, "" },
                { DecodeHintType.NEED_RESULT_POINT_CALLBACK, new CResultPointCallback() },
                //{ BarcodeFormat.MICRO_QR_CODE, "" }, 
                { DecodeHintType.TRY_HARDER, true } 
            });
            ParsedResult parsedResult = ResultParser.parseResult(result);
            this.textBoxResult.Text = string.Format("format:{0}, type:{1} \r\nRaw result:{2}\r\nParsed result:{3}",
                                                                        result.BarcodeFormat,
                                                                        parsedResult.Type,
                                                                        result.Text,
                                                                        parsedResult.DisplayResult);
            if (result.ResultMetadata != null)
            {
                if (result.ResultMetadata.ContainsKey(ResultMetadataType.ERROR_CORRECTION_LEVEL))
                {
                    var ecl = result.ResultMetadata[ResultMetadataType.ERROR_CORRECTION_LEVEL];
                    this.textBoxResult.Text += "\r\nECL:" + ecl;
                }
            }
            //foreach (DictionaryEntry item in result.ResultMetadata)
            //{
            //    this.textBoxResult.Text += string.Format("\r\n[Key:{0}, Value:{1}]", item.Key, item.Value);
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        private class CResultPointCallback
            : ResultPointCallback
        {
            #region ResultPointCallback 成员

            public void foundPossibleResultPoint(ResultPoint point)
            {
                Console.WriteLine("{0}", point);
            }

            #endregion
        }
    }
}
