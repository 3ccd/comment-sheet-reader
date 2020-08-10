using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using TwainLib;
using TwainGui;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Aruco;

namespace CommentSheetOMR
{
    public partial class CommentSheetReader : Form, IMessageFilter
    {

        private Twain tw;
        private bool msgfilter;
        Mat ScanedImage;

        int[] answers;
        List<Mat> freeArea;

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);



        public CommentSheetReader()
        {
            InitializeComponent();
            tw = new Twain();
            tw.Init(Handle);
            FileSaveInit();
            TrackBarInit();
        }

        private void StartScanButton_Click(object sender, EventArgs e)
        {
            if (!msgfilter){
                this.Enabled = false;
                msgfilter = true;
                Application.AddMessageFilter(this);
            }
            tw.Acquire();
            /*Mat oldImage = ScanedImage;
            ScanedImage = Cv2.ImRead("C:/test3.png");
            if (oldImage != null) oldImage.Dispose();
            DrawPictureBox(ScanedImage);
            CalcStartButton.Enabled = true;*/
        }

        bool IMessageFilter.PreFilterMessage(ref Message m){
            TwainCommand cmd = tw.PassMessage(ref m);
            if (cmd == TwainCommand.Not)
                return false;

            switch (cmd)
            {
                case TwainCommand.CloseRequest:
                    {
                        EndingScan();
                        tw.CloseSrc();
                        break;
                    }
                case TwainCommand.CloseOk:
                    {
                        EndingScan();
                        tw.CloseSrc();
                        break;
                    }
                case TwainCommand.DeviceEvent:
                    {
                        break;
                    }
                case TwainCommand.TransferReady:
                    {
                        ArrayList pics = tw.TransferPictures();
                        EndingScan();
                        tw.CloseSrc();
                        
                        for(int i = 0; i < pics.Count; i++)
                        {
                            IntPtr dibPtr = GlobalLock((IntPtr)pics[i]);
                            Bitmap bitmap = DibToImage.WithStream(dibPtr);
                            
                            Mat oldImage = ScanedImage;
                            ScanedImage = BitmapConverter.ToMat(bitmap);
                            if (oldImage != null) oldImage.Dispose();
                            Cv2.CvtColor(ScanedImage, ScanedImage, ColorConversionCodes.BGRA2BGR);
                            resultBox.Items.Add(ScanedImage.Type().ToString());
                            DrawPictureBox(ScanedImage);
                            statusLabel.Text = "スキャンが完了しました。右の画像を確認してください。\r\n" +
                                "失敗した場合は再度やり直すことができます。\r\n" +
                                "マークシートの読み取りを開始するには、「読み取り開始」ボタンをクリックしてください。";

                            CalcStartButton.Enabled = true;
                            dataSaveButton.Enabled = false;
                        }
                        break;
                    }
            }

            return true;
        }

        private void EndingScan()
        {
            if (msgfilter)
            {
                Application.RemoveMessageFilter(this);
                msgfilter = false;
                this.Enabled = true;
                this.Activate();
            }
        }

        private Mat ResizeImage(Mat mat)
        {
            OpenCvSharp.Size size = new OpenCvSharp.Size(pictureBox1.Size.Width, pictureBox1.Size.Height);
            Mat resized = new Mat();
            Cv2.Resize(mat, resized, size);
            return resized;
        }

        private void DrawPictureBox(Mat mat)
        {
            Mat resized = ResizeImage(mat);
            Bitmap bitmap = BitmapConverter.ToBitmap(resized);
            var oldImg = pictureBox1.Image;
            pictureBox1.Image = bitmap;
            if(oldImg != null)
            {
                oldImg.Dispose();
            }
        }


        private void OMRSheet(Mat mat)
        {
            resultBox.Items.Clear();
            Mat gray = new Mat();
            Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.Threshold(gray, gray, 0, 255, ThresholdTypes.Otsu);

            double angle = ImageAdjust(gray);
            if (angle == -1.0)
            {
                resultBox.Items.Add("スキャンに失敗した可能性があります。");
                return;
            }

            Mat adjustedImg = ImageRotate(gray, angle);
            Mat adjustedImgColor = ImageRotate(mat, angle);

            List<CodeDetail> codeV = CodeEncode(adjustedImg);
            List<CodeDetail> codeH = CodeEncodeH(adjustedImg);

            answers = MarkReader(adjustedImg, codeV, codeH, trackBar2.Value * 10, trackBar1.Value);

            freeArea = GetFreeArea(adjustedImgColor, codeV, codeH);

            DrawPictureBox(DrawCodeLine(adjustedImgColor, codeV, codeH, answers));

            statusLabel.Text = "読み取りが終了しました。" + Environment.NewLine + "結果を確認し、問題なければ「データ保存」ボタンをクリックして回答を記録してください。";
            dataSaveButton.Enabled = true;
            adjustedImg.Dispose();
            adjustedImgColor.Dispose();
            gray.Dispose();
        }

        private double ImageAdjust(Mat gray)
        {
            Point2f[] corners = DetectMarkers(gray);

            resultBox.Items.Add("DetectedMarkers : " + corners.Length);
            if (corners.Length <= 2)
            {
                return -1.0;
            }

            double disRadian = Math.Atan2(corners[1].Y - corners[0].Y, corners[1].X - corners[0].X);
            double disDigree = disRadian * 180 / Math.PI;
            resultBox.Items.Add("AdjustDegree : "+disDigree.ToString());
            return disDigree;
        }

        private Mat ImageRotate(Mat mat, double angle)
        {
            Mat outImg = new Mat();
            Mat rotatedImg = Cv2.GetRotationMatrix2D(new Point2f(mat.Width / 2, mat.Height / 2), angle, 1.0);
            Cv2.WarpAffine(mat, outImg, rotatedImg, new OpenCvSharp.Size(mat.Width, mat.Height));

            return outImg;
        }

        private Mat DrawCodeLine(Mat mat, List<CodeDetail> v, List<CodeDetail> h, int[] answers)
        {
            for(int i = 0; i < v.Count; i++)
            {
                Cv2.Line(mat, new OpenCvSharp.Point(0, v[i].Center()), new OpenCvSharp.Point(mat.Width, v[i].Center()), new Scalar(255, 0, 0),4);
            }
            for (int i = 0; i < h.Count; i++)
            {
                Cv2.Line(mat, new OpenCvSharp.Point(h[i].Center(), 0), new OpenCvSharp.Point(h[i].Center(), mat.Height), new Scalar(255, 0, 0),4);
            }
            for(int i = 0; i < answers.Length; i++)
            {
                if (answers[i] != 0) Cv2.Circle(mat, new OpenCvSharp.Point(h[answers[i] - 1].Center(), v[i].Center()), 100, new Scalar(0, 0, 255), 4);
            }
            return mat;
        }

        PredefinedDictionaryName dictName = PredefinedDictionaryName.Dict6X6_250;
        Dictionary dictionary;
        DetectorParameters detectParam;
        private Point2f[] DetectMarkers(Mat gray)
        {
            dictionary = CvAruco.GetPredefinedDictionary(dictName);
            detectParam = DetectorParameters.Create();

            Point2f[][] corners;
            int[] ids;
            Point2f[][] reject_points;

            CvAruco.DetectMarkers(gray, dictionary, out corners, out ids, detectParam, out reject_points);

            Point2f[] cornerPoints = new Point2f[ids.Length];

            for (int i = 0; i < ids.Length; i++)
            {
                for (int j = 0; j < ids.Length; j++)
                {
                    if (ids[j] == i)
                    {
                        cornerPoints[i] = corners[j][0];
                    }
                }
            }

            return cornerPoints;
        }

        private List<CodeDetail> CodeEncode(Mat mat)
        {
            int THRETHOLD = 50;

            List<CodeDetail> codeList = new List<CodeDetail>();
            Point2f[] markerPos = DetectMarkers(mat);

            int prevPix = 255;
            int startPix = 0;
            for (int i = (int)markerPos[0].Y; i < mat.Height; i++)
            {
                if(mat.At<byte>(i, (int)markerPos[0].X -5) < THRETHOLD && prevPix > THRETHOLD)
                {
                    startPix = i;
                }
                if(mat.At<byte>(i, (int)markerPos[0].X -5) > THRETHOLD && prevPix < THRETHOLD)
                {
                    codeList.Add(new CodeDetail(startPix, i, trackBar3.Value));
                }
                prevPix = mat.At<byte>(i, (int)markerPos[0].X - 5);
            }
            resultBox.Items.Add("Detected code : " + codeList.Count.ToString());
            for(int i = 0; i < codeList.Count; i++)
            {
                resultBox.Items.Add(i.ToString() +"START " + codeList[i].startPoint.ToString() + " LENGTH " + codeList[i].AreaSize().ToString() +" TYPE "+codeList[i].type.ToString());
            }
            return codeList;
        }

        private List<CodeDetail> CodeEncodeH(Mat mat)
        {
            int THRETHOLD = 50;

            List<CodeDetail> codeList = new List<CodeDetail>();
            Point2f[] markerPos = DetectMarkers(mat);

            int prevPix = 255;
            int startPix = 0;
            for (int i = (int)markerPos[0].X; i < mat.Width; i++)
            {
                if (mat.At<byte>((int)markerPos[0].Y - 5, i) < THRETHOLD && prevPix > THRETHOLD)
                {
                    startPix = i;
                }
                if (mat.At<byte>((int)markerPos[0].Y - 5, i) > THRETHOLD && prevPix < THRETHOLD)
                {
                    codeList.Add(new CodeDetail(startPix, i, trackBar3.Value));
                }
                prevPix = mat.At<byte>((int)markerPos[0].Y - 5, i);
            }
            resultBox.Items.Add("Detected code : " + codeList.Count.ToString());
            for (int i = 0; i < codeList.Count; i++)
            {
                resultBox.Items.Add(i.ToString() + " START " + codeList[i].startPoint.ToString() + " LENGTH " + codeList[i].AreaSize().ToString());
            }
            return codeList;
        }

        private Mat MatClipper(Mat colorImg, CodeDetail cd, List<CodeDetail> horizon)
        {
            int width = horizon[horizon.Count - 1].Center() - horizon[0].Center();
            return colorImg.Clone(new Rect(horizon[0].Center(), cd.startPoint, width, cd.AreaSize()));
        }

        private List<Mat> GetFreeArea(Mat colorImg, List<CodeDetail> vertical, List<CodeDetail> horizontal)
        {
            List<Mat> matList = new List<Mat>();
            for (int i = 0; i < vertical.Count; i++)
            {
                if (vertical[i].type == 1)
                {
                    matList.Add(MatClipper(colorImg, vertical[i], horizontal));
                }
            }
            return matList;
        }

        private bool ColorCounter(Mat mat, Point2f point, int threthold, int area)
        {
            int counter = 0;
            int pixelSum = 0;
            int horizontal = area;
            int vertical = area;
            for (int i = horizontal * -1; i < horizontal; i++)
            {
                for (int j = vertical * -1; j < vertical; j++)
                {
                    pixelSum += mat.At<byte>((int)point.Y + j, (int)point.X + i);
                    mat.Set<byte>((int)point.Y + j, (int)point.X + i, 0);
                    counter++;
                }
            }
            pixelSum = pixelSum / counter;
            if (pixelSum < threthold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int[] MarkReader(Mat mat, List<CodeDetail> vertical, List<CodeDetail> horizontal, int threthold, int area)
        {
            int[] mark = new int[vertical.Count];
            for (int i = 0; i < vertical.Count; i++)
            {
                if (vertical[i].type == 0) for (int j = 0; j < horizontal.Count; j++)
                    {
                        if (ColorCounter(mat, new Point2f(horizontal[j].Center(), vertical[i].Center()), threthold, area))
                        {
                            mark[i] = j + 1;
                        }
                    }

                if (vertical[i].type == 0) resultBox.Items.Add("Qnum : " + i.ToString() + " Value : " + mark[i].ToString());
                if (vertical[i].type == 1) resultBox.Items.Add("Qnum : " + i.ToString() + " Value : FreeArea");
            }

            return mark;
        }

        private void CalcStartButton_Click(object sender, EventArgs e)
        {
            OMRSheet(ScanedImage);
        }

        private void FileSave()
        {
            bool isFirst = false;

            if(textBox1.Text == "")
            {
                statusLabel.Text = "保存先を指定してください。";
                return;
            }
            if(textBox2.Text == "")
            {
                statusLabel.Text = "ファイル名を指定してください。";
                return;
            }
            string filePath = textBox1.Text + @"\" + textBox2.Text;
            if (!Directory.Exists(filePath))
            {
                try
                {
                    Directory.CreateDirectory(filePath);
                }
                catch(Exception e)
                {
                    statusLabel.Text = "ファイル保存エラー　"+ Environment.NewLine + e.Message;
                    return;
                }
                
                isFirst = true;
            }

            SaveSCV(filePath);
            SaveImage(filePath);

            if (isFirst)
            {
                statusLabel.Text = "フォルダ：「" + textBox2.Text + "」を作成し、保存しました。";
            }
            else
            {
                statusLabel.Text = "データを追記しました。";
            }


        }

        private string GetTimeStamp(bool type)
        {
            DateTime dt = DateTime.Now;
            string ts;
            if (type)
            {
                ts = dt.ToString("yyyy/MM/dd HH:mm");
            }
            else
            {
                ts = dt.ToString("yyyyMMddHHmm");
            }
            return ts;
        }

        private void SaveSCV(string filePath)
        {
            string csvLine = GetTimeStamp(true);
            for(int i = 0; i < answers.Length; i++)
            {
                csvLine += "," + answers[i].ToString();
            }
            csvLine += Environment.NewLine;
            try
            {
                File.AppendAllText(filePath + @"\data.csv", csvLine);
            }catch(Exception e)
            {
                statusLabel.Text = "CSV保存エラー　" + Environment.NewLine + e.Message;
                return;
            }
        }

        private void SaveImage(string filePath)
        {
            for(int i = 0; i < freeArea.Count; i++)
            {
                try
                {
                    Cv2.ImWrite(filePath + @"\" + GetTimeStamp(false) + "_" + i.ToString() + ".jpg", freeArea[i]);
                }catch(Exception e)
                {
                    statusLabel.Text = "画像保存エラー　" + Environment.NewLine + e.Message;
                }
                
            }
        }

        private void FileSaveInit()
        {
            textBox1.Text = Environment.CurrentDirectory;
        }

        private void TrackBarInit()
        {
            trackBar1Value.Text = trackBar1.Value.ToString();
            trackBar2Value.Text = trackBar2.Value.ToString();
            trackBar3Value.Text = trackBar3.Value.ToString();
        }

        private void DataSaveButton_Click(object sender, EventArgs e)
        {
            FileSave();
        }

        private string ShowDirectoryDialog(string selectedPath)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "データを保存するフォルダを選択してください。";
            fbd.SelectedPath = selectedPath;
            fbd.ShowNewFolderButton = true;

            if(fbd.ShowDialog(this) == DialogResult.OK)
            {
                fbd.Dispose();
                return fbd.SelectedPath;
            }
            else
            {
                fbd.Dispose();
                return fbd.SelectedPath;
            }

        }

        private void FolderButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = ShowDirectoryDialog(textBox1.Text);
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1Value.Text = trackBar1.Value.ToString();
        }

        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            trackBar2Value.Text = trackBar2.Value.ToString();
        }

        private void TrackBar3_Scroll(object sender, EventArgs e)
        {
            trackBar3Value.Text = trackBar3.Value.ToString();
        }
    }

    class CodeDetail
    {
        public static readonly int TEXT_AREA = 1;
        public static readonly int MARK_AREA = 0;

        public int startPoint;
        public int endPoint;
        public int type;

        public CodeDetail(int stp, int edp, int areaSize)
        {
            startPoint = stp;
            endPoint = edp;
            if(edp - stp > areaSize)
            {
                type = TEXT_AREA;
            }
            else
            {
                type = MARK_AREA;
            }
        }

        public int Center()
        {
            return ((endPoint - startPoint) / 2) + startPoint;
        }
        public int AreaSize()
        {
            return endPoint - startPoint;
        }
    }
}
