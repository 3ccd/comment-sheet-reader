using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Aruco;

namespace CommentSheetOMR
{
    class MarkReader
    {

        Mat gray;
        Mat color;

        public MarkReader(Mat colorMat, Mat grayMat)
        {
            gray = grayMat;
            color = colorMat;
        }

        private int[] MarkRead(Mat mat, List<CodeDetail> vertical, List<CodeDetail> horizontal, int threthold, int area)
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

                if (vertical[i].type == 0) Form1.resultBox.Items.Add("Qnum : " + i.ToString() + " Value : " + mark[i].ToString());
                if (vertical[i].type == 1) Form1.resultBox.Items.Add("Qnum : " + i.ToString() + " Value : FreeArea");
            }

            return mark;
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

        private Mat DrawCodeLine(Mat mat, List<CodeDetail> v, List<CodeDetail> h, int[] answers)
        {
            for (int i = 0; i < v.Count; i++)
            {
                Cv2.Line(mat, new OpenCvSharp.Point(0, v[i].Center()), new OpenCvSharp.Point(mat.Width, v[i].Center()), new Scalar(255, 0, 0), 4);
            }
            for (int i = 0; i < h.Count; i++)
            {
                Cv2.Line(mat, new OpenCvSharp.Point(h[i].Center(), 0), new OpenCvSharp.Point(h[i].Center(), mat.Height), new Scalar(255, 0, 0), 4);
            }
            for (int i = 0; i < answers.Length; i++)
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

        public List<CodeDetail> CodeEncode(Mat mat)
        {
            int THRETHOLD = 50;

            List<CodeDetail> codeList = new List<CodeDetail>();
            Point2f[] markerPos = DetectMarkers(mat);

            int prevPix = 255;
            int startPix = 0;
            for (int i = (int)markerPos[0].Y; i < mat.Height; i++)
            {
                if (mat.At<byte>(i, (int)markerPos[0].X - 5) < THRETHOLD && prevPix > THRETHOLD)
                {
                    startPix = i;
                }
                if (mat.At<byte>(i, (int)markerPos[0].X - 5) > THRETHOLD && prevPix < THRETHOLD)
                {
                    codeList.Add(new CodeDetail(startPix, i, trackBar3.Value));
                }
                prevPix = mat.At<byte>(i, (int)markerPos[0].X - 5);
            }
            resultBox.Items.Add("Detected code : " + codeList.Count.ToString());
            for (int i = 0; i < codeList.Count; i++)
            {
                resultBox.Items.Add(i.ToString() + "START " + codeList[i].startPoint.ToString() + " LENGTH " + codeList[i].AreaSize().ToString() + " TYPE " + codeList[i].type.ToString());
            }
            return codeList;
        }

        public List<CodeDetail> CodeEncodeH(Mat mat)
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
    }
}
