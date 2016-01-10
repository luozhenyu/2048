using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //初始化变量region
        #region
        int numLeft;
        bool isGameOver = false;
        int isGameWin = 0;
        int[,] table = new int[4, 4];
        int[,] lastStep = new int[4, 4];
        string saveGamePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\2048";
        Random randobj = new Random();
        Bitmap bmpBack = new Bitmap(500, 600);
        Bitmap num2 = new Bitmap(100, 100);
        Bitmap num4 = new Bitmap(100, 100);
        Bitmap num4B = new Bitmap(100, 100);
        Bitmap num8 = new Bitmap(100, 100);
        Bitmap num8B = new Bitmap(100, 100);
        Bitmap num16 = new Bitmap(100, 100);
        Bitmap num16B = new Bitmap(100, 100);
        Bitmap num32 = new Bitmap(100, 100);
        Bitmap num32B = new Bitmap(100, 100);
        Bitmap num64 = new Bitmap(100, 100);
        Bitmap num64B = new Bitmap(100, 100);
        Bitmap num128 = new Bitmap(100, 100);
        Bitmap num128B = new Bitmap(100, 100);
        Bitmap num256 = new Bitmap(100, 100);
        Bitmap num256B = new Bitmap(100, 100);
        Bitmap num512 = new Bitmap(100, 100);
        Bitmap num512B = new Bitmap(100, 100);
        Bitmap num1024 = new Bitmap(100, 100);
        Bitmap num1024B = new Bitmap(100, 100);
        Bitmap num2048 = new Bitmap(100, 100);
        Bitmap num2048B = new Bitmap(100, 100);
        Bitmap num4096 = new Bitmap(100, 100);
        Bitmap num4096B = new Bitmap(100, 100);
        Bitmap num8192 = new Bitmap(100, 100);
        Bitmap num8192B = new Bitmap(100, 100);
        Bitmap num16384 = new Bitmap(100, 100);
        Bitmap num16384B = new Bitmap(100, 100);
        Bitmap num32768 = new Bitmap(100, 100);
        Bitmap num32768B = new Bitmap(100, 100);
        Bitmap num65536 = new Bitmap(100, 100);
        Bitmap num65536B = new Bitmap(100, 100);
        Bitmap num131072 = new Bitmap(100, 100);
        Bitmap num131072B = new Bitmap(100, 100);
        Bitmap gameOver = new Bitmap(420, 420);
        Bitmap gameWin = new Bitmap(420, 420);
        Bitmap aboutMe = new Bitmap(420, 420);
        Bitmap composedPicture = new Bitmap(500, 600);
        Bitmap composedPicture0 = new Bitmap(500, 600);
        Bitmap composedPicture1 = new Bitmap(500, 600);
        Bitmap composedPicture2 = new Bitmap(500, 600);
        Bitmap composedPicture3 = new Bitmap(500, 600);
        Bitmap composedPicture4 = new Bitmap(500, 600);
        Bitmap composedPicture5 = new Bitmap(500, 600);
        Bitmap composedPicture6 = new Bitmap(500, 600);
        #endregion
        //启动动画
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Thread th = new Thread(resourcePrepare);
            th.Start();
            AnimateWindow(Handle, 300, 0x20004); // 自上向下。
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            if (File.Exists(saveGamePath + @"\gameData.dat"))
            {
                readGameData();
                resetAnimation();
                print2048();
            }
            else
            {
                resetGame();
            }
        }
        void resourcePrepare()
        {
            Graphics bufferGraphics = Graphics.FromImage(bmpBack);
            bufferGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            bufferGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //绘制窗体
            Point[] mainBorder = new Point[5];
            mainBorder[0] = new Point(0, 0);
            mainBorder[1] = new Point(499, 0);
            mainBorder[2] = new Point(499, 599);
            mainBorder[3] = new Point(0, 599);
            mainBorder[4] = new Point(0, 0);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(250, 249, 239)), new Rectangle(1, 1, 499, 599));
            bufferGraphics.DrawLines(new Pen(Color.LightGray, 1), mainBorder);
            //BEST与SCORE
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(201, 187, 176)), new Rectangle(250, 30, 100, 55));
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(201, 187, 176)), new Rectangle(360, 30, 100, 55));
            //文字
            bufferGraphics.DrawString("2048", new Font("Microsoft Yahei", 30, FontStyle.Bold), new SolidBrush(Color.FromArgb(139, 129, 120)), new PointF(40, 30));
            bufferGraphics.DrawString("Join the numbers and", new Font("Microsoft Yahei", 15), new SolidBrush(Color.FromArgb(120, 110, 101)), new PointF(40, 85));
            bufferGraphics.DrawString("get to the", new Font("Arial", 15), new SolidBrush(Color.FromArgb(120, 110, 101)), new PointF(40, 110));
            bufferGraphics.DrawString("2048 tile!", new Font("Arial", 15, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 101)), new PointF(130, 110));
            bufferGraphics.DrawString("SCORE", new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.FromArgb(242, 233, 223)), new PointF(268, 40));
            bufferGraphics.DrawString("BEST", new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.FromArgb(242, 233, 223)), new PointF(383, 40));
            //数字区域 40-460
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(188, 173, 159)), new Rectangle(40, 140, 420, 420));
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(205, 192, 179)), new Rectangle(100 * j + 55, 100 * i + 155, 90, 90));
                }
            //画2的图
            bufferGraphics = Graphics.FromImage(num2);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(239, 228, 218)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("2", new Font("Microsoft Yahei", 40, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 101)), new PointF(25, 15));
            //画4的图
            bufferGraphics = Graphics.FromImage(num4);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(238, 225, 199)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("4", new Font("Microsoft Yahei", 40, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 101)), new PointF(25, 15));
            //画4B的图
            bufferGraphics = Graphics.FromImage(num4B);
            bufferGraphics.DrawImage(num4, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画8的图
            bufferGraphics = Graphics.FromImage(num8);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(248, 176, 116)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("8", new Font("Microsoft Yahei", 40, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(25, 15));
            //画8B的图
            bufferGraphics = Graphics.FromImage(num8B);
            bufferGraphics.DrawImage(num8, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画16的图
            bufferGraphics = Graphics.FromImage(num16);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(254, 144, 90)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("16", new Font("Microsoft Yahei", 40, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(7, 15));
            //画16B的图
            bufferGraphics = Graphics.FromImage(num16B);
            bufferGraphics.DrawImage(num16, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画32的图
            bufferGraphics = Graphics.FromImage(num32);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 113, 85)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("32", new Font("Microsoft Yahei", 40, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(7, 15));
            //画32B的图
            bufferGraphics = Graphics.FromImage(num32B);
            bufferGraphics.DrawImage(num32, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画64的图
            bufferGraphics = Graphics.FromImage(num64);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 75, 34)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("64", new Font("Microsoft Yahei", 40, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(7, 15));
            //画64B的图
            bufferGraphics = Graphics.FromImage(num64B);
            bufferGraphics.DrawImage(num64, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画128的图
            bufferGraphics = Graphics.FromImage(num128);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(238, 210, 110)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("128", new Font("Microsoft Yahei", 29, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(5, 25));
            //画128B的图
            bufferGraphics = Graphics.FromImage(num128B);
            bufferGraphics.DrawImage(num128, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画256的图
            bufferGraphics = Graphics.FromImage(num256);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(239, 207, 91)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("256", new Font("Microsoft Yahei", 29, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(5, 25));
            //画256B的图
            bufferGraphics = Graphics.FromImage(num256B);
            bufferGraphics.DrawImage(num256, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画512的图
            bufferGraphics = Graphics.FromImage(num512);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(239, 207, 72)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("512", new Font("Microsoft Yahei", 29, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(5, 25));
            //画512B的图
            bufferGraphics = Graphics.FromImage(num512B);
            bufferGraphics.DrawImage(num512, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画1024的图
            bufferGraphics = Graphics.FromImage(num1024);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(239, 200, 52)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("1024", new Font("Microsoft Yahei", 23, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(5, 30));
            //画1024B的图
            bufferGraphics = Graphics.FromImage(num1024B);
            bufferGraphics.DrawImage(num1024, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画2048的图
            bufferGraphics = Graphics.FromImage(num2048);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(238, 197, 26)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("2048", new Font("Microsoft Yahei", 23, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(5, 30));
            //画2048B的图
            bufferGraphics = Graphics.FromImage(num2048B);
            bufferGraphics.DrawImage(num2048, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画4096的图
            bufferGraphics = Graphics.FromImage(num4096);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 33)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("4096", new Font("Microsoft Yahei", 23, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(5, 30));
            //画4096B的图
            bufferGraphics = Graphics.FromImage(num4096B);
            bufferGraphics.DrawImage(num4096, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画8192的图
            bufferGraphics = Graphics.FromImage(num8192);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 0)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("8192", new Font("Microsoft Yahei", 23, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(5, 30));
            //画8192B的图
            bufferGraphics = Graphics.FromImage(num8192B);
            bufferGraphics.DrawImage(num8192, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画16384的图
            bufferGraphics = Graphics.FromImage(num16384);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 0)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("16384", new Font("Microsoft Yahei", 19, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(6, 35));
            //画16384B的图
            bufferGraphics = Graphics.FromImage(num16384B);
            bufferGraphics.DrawImage(num16384, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画32768的图
            bufferGraphics = Graphics.FromImage(num32768);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 0)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("32768", new Font("Microsoft Yahei", 19, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(6, 35));
            //画32768B的图
            bufferGraphics = Graphics.FromImage(num32768B);
            bufferGraphics.DrawImage(num32768, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画65536的图
            bufferGraphics = Graphics.FromImage(num65536);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 0)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("65536", new Font("Microsoft Yahei", 19, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(6, 35));
            //画65536B的图
            bufferGraphics = Graphics.FromImage(num65536B);
            bufferGraphics.DrawImage(num65536, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画131072的图
            bufferGraphics = Graphics.FromImage(num131072);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 0)), new Rectangle(5, 5, 90, 90));
            bufferGraphics.DrawString("131072", new Font("Microsoft Yahei", 16, FontStyle.Bold), new SolidBrush(Color.FromArgb(249, 246, 242)), new PointF(6, 38));
            //画131072B的图
            bufferGraphics = Graphics.FromImage(num131072B);
            bufferGraphics.DrawImage(num131072, new Rectangle(0, 0, 100, 100), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //画gameOver
            bufferGraphics = Graphics.FromImage(gameOver);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(220, 213, 200, 187)), new Rectangle(0, 0, 420, 420));
            bufferGraphics.DrawString("Game over!", new Font("Microsoft Yahei", 30, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 112)), new PointF(85, 130));
            bufferGraphics.DrawString("Try again", new Font("Microsoft Yahei", 20, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 112)), new PointF(135, 210));
            bufferGraphics.DrawString("Press Enter", new Font("Microsoft Yahei", 20, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 112)), new PointF(125, 250));
            //画gameWin
            bufferGraphics = Graphics.FromImage(gameWin);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 213, 200, 187)), new Rectangle(0, 0, 420, 420));
            bufferGraphics.DrawString("You Win!", new Font("Microsoft Yahei", 30, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 112)), new PointF(110, 130));
            bufferGraphics.DrawString("To continue", new Font("Microsoft Yahei", 20, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 112)), new PointF(125, 210));
            bufferGraphics.DrawString("Press Enter", new Font("Microsoft Yahei", 20, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 112)), new PointF(135, 250));
            //画aboutMe
            bufferGraphics = Graphics.FromImage(aboutMe);
            bufferGraphics.FillRectangle(new SolidBrush(Color.FromArgb(220, 213, 200, 187)), new Rectangle(0, 0, 420, 420));
            bufferGraphics.DrawString("2048", new Font("Microsoft Yahei", 50, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 112)), new PointF(115, 60));
            bufferGraphics.DrawString("If you like the game, please\n\n               with your friends ", new Font("Microsoft Yahei", 15, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 112)), new PointF(65, 180));
            bufferGraphics.DrawString("share", new Font("Microsoft Yahei", 23, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 112)), new PointF(62, 222));
            bufferGraphics.DrawString("   If you are lucky enough to \nfind a bug, please contact me\n        at QQ807447907 ", new Font("Microsoft Yahei", 10, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 112)), new PointF(105, 360));
            bufferGraphics.DrawString("Power By Luo Zhenyu,\n       罗震宇 China", new Font("Microsoft Yahei", 15, FontStyle.Bold), new SolidBrush(Color.FromArgb(120, 110, 112)), new PointF(90, 290));
            bufferGraphics.Dispose();
        }
        void print2048()
        {
            Graphics bufferGraphics = Graphics.FromImage(composedPicture);
            bufferGraphics.DrawImage(bmpBack, 0, 0);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (table[i, j] != 0)
                    {
                        if (table[i, j] == 2) bufferGraphics.DrawImage(num2, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 4) bufferGraphics.DrawImage(num4, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 8) bufferGraphics.DrawImage(num8, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 16) bufferGraphics.DrawImage(num16, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 32) bufferGraphics.DrawImage(num32, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 64) bufferGraphics.DrawImage(num64, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 128) bufferGraphics.DrawImage(num128, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 256) bufferGraphics.DrawImage(num256, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 512) bufferGraphics.DrawImage(num512, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 1024) bufferGraphics.DrawImage(num1024, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 2048) bufferGraphics.DrawImage(num2048, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 4096) bufferGraphics.DrawImage(num4096, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 8192) bufferGraphics.DrawImage(num8192, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 16384) bufferGraphics.DrawImage(num16384, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 32768) bufferGraphics.DrawImage(num32768, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 65536) bufferGraphics.DrawImage(num65536, 100 * j + 50, 100 * i + 150);
                        else if (table[i, j] == 131072) bufferGraphics.DrawImage(num131072, 100 * j + 50, 100 * i + 150);
                    }
                }
            }
            Graphics g = (new PaintEventArgs(CreateGraphics(), ClientRectangle)).Graphics;
            g.DrawImage(composedPicture, 0, 0);
            bufferGraphics.Dispose();
            g.Dispose();
        }
        void saveGameData(uint score, uint best)
        {
            byte[] gameData = new byte[24];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0, k, l; j < 4; j++)
                {
                    l = 0;
                    k = table[i, j];
                    while (k > 1)
                    {
                        k >>= 1;
                        l++;
                    }
                    gameData[i * 4 + j] = Convert.ToByte(l);
                }
            }
            gameData[16] = Convert.ToByte((score & 0xff000000) >> 24);
            gameData[17] = Convert.ToByte((score & 0x00ff0000) >> 16);
            gameData[18] = Convert.ToByte((score & 0x0000ff00) >> 8);
            gameData[19] = Convert.ToByte((score & 0x000000ff));
            gameData[20] = Convert.ToByte((best & 0xff000000) >> 24);
            gameData[21] = Convert.ToByte((best & 0x00ff0000) >> 16);
            gameData[22] = Convert.ToByte((best & 0x0000ff00) >> 8);
            gameData[23] = Convert.ToByte((best & 0x000000ff));
            if (!Directory.Exists(saveGamePath))
            {
                DirectoryInfo dir = new DirectoryInfo(saveGamePath);
                dir.Create();
            }
            File.WriteAllBytes(saveGamePath + @"\gameData.dat", gameData);
        }
        void readGameData()
        {
            byte[] gameData = File.ReadAllBytes(saveGamePath + @"\gameData.dat");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (gameData[i * 4 + j] == 0)
                    {
                        table[i, j] = 0;
                    }
                    else
                    {
                        table[i, j] = 1 << gameData[i * 4 + j];
                    }
                }
            }
            label_score.Text = ((gameData[16] << 24) + (gameData[17] << 16) + (gameData[18] << 8) + gameData[19]).ToString();
            label_best.Text = ((gameData[20] << 24) + (gameData[21] << 16) + (gameData[22] << 8) + gameData[23]).ToString();
        }
        void resetGame()
        {
            //全部重设为0
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    table[i, j] = 0;
            isGameOver = false;
            isGameWin = 0;
            label_score.Text = "0";
            resetAnimation();
            randomProduct();
            randomProduct();
            showAnimation();
            print2048();
        }
        private void testIsGameOver()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (table[i, j] == 0)
                    {
                        isGameOver = false;
                        return;
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (table[i, j] == table[i, j + 1])
                    {
                        isGameOver = false;
                        return;
                    }
                }
            }
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (table[i, j] == table[i + 1, j])
                    {
                        isGameOver = false;
                        return;
                    }
                }
            }
            if (isGameOver == false)
            {
                isGameOver = true;
                Graphics g = (new PaintEventArgs(CreateGraphics(), ClientRectangle)).Graphics;
                g.DrawImage(gameOver, 40, 140);
                g.Dispose();
            }
        }
        private void testIsWin()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (table[i, j] == 2048 && isGameWin == 0)
                    {
                        isGameWin = 1;
                        return;
                    }
                }
            }
        }
        void exitGame()
        {
            saveGameData(Convert.ToUInt32(label_score.Text), Convert.ToUInt32(label_best.Text));
            AnimateWindow(Handle, 500, 0x10008); // 自下而上。
            Environment.Exit(0);
        }
        //剩余格子随机产生2或4
        void randomProduct()
        {
            numLeft = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (table[i, j] == 0) numLeft++;
                }
            }
            if (numLeft == 0) return;
            numLeft = randobj.Next(numLeft) + 1;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (table[i, j] == 0)
                    {
                        numLeft--;
                        if (numLeft == 0)
                        {
                            if (randobj.Next(10) == 0)
                            {
                                table[i, j] = 4;
                                makeComposedPictures(100 * j + 50, 100 * i + 150, 4);
                            }
                            else
                            {
                                table[i, j] = 2;
                                makeComposedPictures(100 * j + 50, 100 * i + 150, 2);
                            }
                        }
                    }
                }
            }
        }
        //补间动画
        private void showAnimation()
        {
            Graphics g = (new PaintEventArgs(CreateGraphics(), ClientRectangle)).Graphics;
            g.DrawImage(composedPicture0, 0, 0);
            Thread.Sleep(15);
            g.DrawImage(composedPicture1, 0, 0);
            Thread.Sleep(15);
            g.DrawImage(composedPicture2, 0, 0);
            Thread.Sleep(15);
            g.DrawImage(composedPicture3, 0, 0);
            Thread.Sleep(15);
            g.DrawImage(composedPicture4, 0, 0);
            Thread.Sleep(30);
            g.DrawImage(composedPicture5, 0, 0);
            Thread.Sleep(100);
            g.DrawImage(composedPicture6, 0, 0);
            g.Dispose();
        }
        private void resetAnimation()
        {
            Graphics bufferGraphics = Graphics.FromImage(composedPicture0);
            bufferGraphics.DrawImage(bmpBack, 0, 0);
            bufferGraphics = Graphics.FromImage(composedPicture1);
            bufferGraphics.DrawImage(bmpBack, 0, 0);
            bufferGraphics = Graphics.FromImage(composedPicture2);
            bufferGraphics.DrawImage(bmpBack, 0, 0);
            bufferGraphics = Graphics.FromImage(composedPicture3);
            bufferGraphics.DrawImage(bmpBack, 0, 0);
            bufferGraphics = Graphics.FromImage(composedPicture4);
            bufferGraphics.DrawImage(bmpBack, 0, 0);
            bufferGraphics = Graphics.FromImage(composedPicture5);
            bufferGraphics.DrawImage(bmpBack, 0, 0);
            bufferGraphics = Graphics.FromImage(composedPicture6);
            bufferGraphics.DrawImage(bmpBack, 0, 0);
            bufferGraphics = Graphics.FromImage(composedPicture);
        }
        //重载 移动并合并方块
        void makeComposedPictures(int fromX, int fromY, int toX, int toY, int number1, int number2)
        {
            Graphics bufferGraphics;
            Bitmap temp = new Bitmap(100, 100);
            Bitmap temp2 = new Bitmap(100, 100);
            Bitmap temp2B = new Bitmap(100, 100);
            if (number1 == 2)
            {
                temp = num2;
                temp2 = num4;
                temp2B = num4B;
            }
            else if (number1 == 4)
            {
                temp = num4;
                temp2 = num8;
                temp2B = num8B;
            }
            else if (number1 == 8)
            {
                temp = num8;
                temp2 = num16;
                temp2B = num16B;
            }
            else if (number1 == 16)
            {
                temp = num16;
                temp2 = num32;
                temp2B = num32B;
            }
            else if (number1 == 32)
            {
                temp = num32;
                temp2 = num64;
                temp2B = num64B;
            }
            else if (number1 == 64)
            {
                temp = num64;
                temp2 = num128;
                temp2B = num128B;
            }
            else if (number1 == 128)
            {
                temp = num128;
                temp2 = num256;
                temp2B = num256B;
            }
            else if (number1 == 256)
            {
                temp = num256;
                temp2 = num512;
                temp2B = num512B;
            }
            else if (number1 == 512)
            {
                temp = num512;
                temp2 = num1024;
                temp2B = num1024B;
            }
            else if (number1 == 1024)
            {
                temp = num1024;
                temp2 = num2048;
                temp2B = num2048B;
            }
            else if (number1 == 2048)
            {
                temp = num2048;
                temp2 = num4096;
                temp2B = num4096B;
            }
            else if (number1 == 4096)
            {
                temp = num4096;
                temp2 = num8192;
                temp2B = num8192B;
            }
            else if (number1 == 8192)
            {
                temp = num8192;
                temp2 = num16384;
                temp2B = num16384B;
            }
            else if (number1 == 16384)
            {
                temp = num16384;
                temp2 = num32768;
                temp2B = num32768B;
            }
            else if (number1 == 32768)
            {
                temp = num32768;
                temp2 = num65536;
                temp2B = num65536B;
            }
            else if (number1 == 65536)
            {
                temp = num65536;
                temp2 = num131072;
                temp2B = num131072B;
            }
            int perStepX = (toX - fromX) / 8;
            int perStepY = (toY - fromY) / 8;
            //移动
            //0
            bufferGraphics = Graphics.FromImage(composedPicture0);
            bufferGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            bufferGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            bufferGraphics.DrawImage(temp, fromX + perStepX * 1, fromY + perStepY * 1);
            //1
            bufferGraphics = Graphics.FromImage(composedPicture1);
            bufferGraphics.DrawImage(temp, fromX + perStepX * 2, fromY + perStepY * 2);
            //2
            bufferGraphics = Graphics.FromImage(composedPicture2);
            bufferGraphics.DrawImage(temp, fromX + perStepX * 4, fromY + perStepY * 4);
            //3
            bufferGraphics = Graphics.FromImage(composedPicture3);
            bufferGraphics.DrawImage(temp, fromX + perStepX * 6, fromY + perStepY * 6);
            //4
            bufferGraphics = Graphics.FromImage(composedPicture4);
            bufferGraphics.DrawImage(temp, fromX + perStepX * 7, fromY + perStepY * 7);
            //放大
            //0
            bufferGraphics = Graphics.FromImage(composedPicture5);
            bufferGraphics.DrawImage(temp2B, toX, toY);
            //1
            bufferGraphics = Graphics.FromImage(composedPicture6);
            bufferGraphics.DrawImage(temp2, toX, toY);
        }
        //重载 移动到指定位置
        void makeComposedPictures(int fromX, int fromY, int toX, int toY, int number)
        {
            Graphics bufferGraphics;
            Bitmap temp = new Bitmap(100, 100);
            if (number == 2)
            {
                temp = num2;
            }
            else if (number == 4)
            {
                temp = num4;
            }
            else if (number == 8)
            {
                temp = num8;
            }
            else if (number == 16)
            {
                temp = num16;
            }
            else if (number == 32)
            {
                temp = num32;
            }
            else if (number == 64)
            {
                temp = num64;
            }
            else if (number == 128)
            {
                temp = num128;
            }
            else if (number == 256)
            {
                temp = num256;
            }
            else if (number == 512)
            {
                temp = num512;
            }
            else if (number == 1024)
            {
                temp = num1024;
            }
            else if (number == 2048)
            {
                temp = num2048;
            }
            else if (number == 4096)
            {
                temp = num4096;
            }
            else if (number == 8192)
            {
                temp = num8192;
            }
            else if (number == 16384)
            {
                temp = num16384;
            }
            else if (number == 32768)
            {
                temp = num32768;
            }
            else if (number == 65536)
            {
                temp = num65536;
            }
            else if (number == 131072)
            {
                temp = num131072;
            }
            int perStepX = (toX - fromX) / 8;
            int perStepY = (toY - fromY) / 8;
            //移动
            //0
            bufferGraphics = Graphics.FromImage(composedPicture0);
            bufferGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            bufferGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            bufferGraphics.DrawImage(temp, fromX + perStepX * 1, fromY + perStepY * 1);
            //1
            bufferGraphics = Graphics.FromImage(composedPicture1);
            bufferGraphics.DrawImage(temp, fromX + perStepX * 2, fromY + perStepY * 2);
            //2
            bufferGraphics = Graphics.FromImage(composedPicture2);
            bufferGraphics.DrawImage(temp, fromX + perStepX * 4, fromY + perStepY * 4);
            //3
            bufferGraphics = Graphics.FromImage(composedPicture3);
            bufferGraphics.DrawImage(temp, fromX + perStepX * 6, fromY + perStepY * 6);
            //4
            bufferGraphics = Graphics.FromImage(composedPicture4);
            bufferGraphics.DrawImage(temp, fromX + perStepX * 7, fromY + perStepY * 7);
            //放大时补间
            //0
            bufferGraphics = Graphics.FromImage(composedPicture5);
            bufferGraphics.DrawImage(temp, toX, toY);
            //1
            bufferGraphics = Graphics.FromImage(composedPicture6);
            bufferGraphics.DrawImage(temp, toX, toY);
            bufferGraphics.Dispose();
        }
        //重载 生成方块
        void makeComposedPictures(int fromX, int fromY, int number)
        {
            Graphics bufferGraphics;
            Bitmap temp = new Bitmap(100, 100);
            if (number == 2)
            {
                temp = num2;
            }
            else if (number == 4)
            {
                temp = num4;
            }
            //移动
            bufferGraphics = Graphics.FromImage(composedPicture0);
            bufferGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            bufferGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //2
            bufferGraphics = Graphics.FromImage(composedPicture3);
            bufferGraphics.DrawImage(temp, new Rectangle(fromX + 45, fromY + 45, 10, 10), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //3
            bufferGraphics = Graphics.FromImage(composedPicture3);
            bufferGraphics.DrawImage(temp, new Rectangle(fromX + 30, fromY + 30, 40, 40), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //4
            bufferGraphics = Graphics.FromImage(composedPicture4);
            bufferGraphics.DrawImage(temp, new Rectangle(fromX + 15, fromY + 15, 70, 70), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //放大时补间
            //0
            bufferGraphics = Graphics.FromImage(composedPicture5);
            bufferGraphics.DrawImage(temp, new Rectangle(fromX + 8, fromY + 8, 84, 84), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            //1
            bufferGraphics = Graphics.FromImage(composedPicture6);
            bufferGraphics.DrawImage(temp, new Rectangle(fromX + 5, fromY + 5, 90, 90), new Rectangle(5, 5, 90, 90), GraphicsUnit.Pixel);
            bufferGraphics.Dispose();
        }
        //方向键操作 取消方向键对控件的焦点的控件，用自己自定义的函数处理各个方向键的处理函数
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up :
                    upKeyDown();
                    return true;
                case Keys.W:
                    upKeyDown();
                    return true;
                case Keys.Down:
                    downKeyDown();
                    return true;
                case Keys.S:
                    downKeyDown();
                    return true;
                case Keys.Left:
                    leftKeyDown();
                    return true;
                case Keys.A:
                    leftKeyDown();
                    return true;
                case Keys.Right:
                    rightKeyDown();
                    return true;
                case Keys.D:
                    rightKeyDown();
                    return true;
                case Keys.Escape:
                    exitGame();
                    return true;
                case Keys.Enter:
                    if (isGameOver) resetGame();
                    else if (isGameWin == 1)
                    {
                        isGameWin = 2;
                        print2048();
                    }
                    return true;
                case Keys.Tab:
                    Thread a = new Thread(fixTab);
                    a.Start();
                    return true;
                default:
                    return false;
            }
        }
        private void upKeyDown()
        {
            testIsGameOver();
            if (isGameOver || isGameWin == 1)
            {
                return;
            }
            lastStep = (int[,])table.Clone();
            resetAnimation();
            for (int j = 0; j < 4; j++)
            {
                if (table[0, j] == 0)
                {
                    if (table[1, j] == 0)
                    {
                        if (table[2, j] == 0)
                        {
                            if (table[3, j] == 0)
                            {; }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 150, table[3, j]);
                                table[0, j] = table[3, j];
                                table[3, j] = 0;
                            }
                        }
                        else
                        {
                            if (table[3, j] == 0)
                            {
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 150, table[2, j]);
                                table[0, j] = table[2, j];
                                table[2, j] = 0;
                            }
                            else if (table[3, j] == table[2, j])
                            {
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 150, table[2, j]);
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 150, table[3, j], table[3, j]);
                                table[0, j] = table[2, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[0, j]).ToString();
                                table[2, j] = 0;
                                table[3, j] = 0;
                            }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 150, table[2, j]);
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 250, table[3, j]);
                                table[0, j] = table[2, j];
                                table[1, j] = table[3, j];
                                table[2, j] = 0;
                                table[3, j] = 0;
                            }
                        }
                    }
                    else
                    {
                        if (table[2, j] == 0)
                        {
                            if (table[j, 3] == 0)
                            {
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j]);
                                table[0, j] = table[1, j];
                                table[1, j] = 0;
                            }
                            else if (table[1, j] == table[3, j])
                            {
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j]);
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 150, table[3, j], table[3, j]);
                                table[0, j] = table[1, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[0, j]).ToString();
                                table[1, j] = 0;
                                table[3, j] = 0;
                            }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j]);
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 250, table[3, j]);
                                table[0, j] = table[1, j];
                                table[1, j] = table[3, j];
                                table[3, j] = 0;
                            }
                        }
                        else
                        {
                            if (table[3, j] == 0)
                            {
                                if (table[1, j] == table[2, j])
                                {
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j]);
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 150, table[2, j], table[2, j]);
                                    table[0, j] = table[2, j] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[0, j]).ToString();
                                    table[1, j] = 0;
                                    table[2, j] = 0;
                                }
                                else
                                {
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j]);
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 250, table[2, j]);
                                    table[0, j] = table[1, j];
                                    table[1, j] = table[2, j];
                                    table[2, j] = 0;
                                }
                            }
                            else
                            {
                                if (table[1, j] == table[2, j])
                                {
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j]);
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 150, table[2, j], table[2, j]);
                                    makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 250, table[3, j]);
                                    table[0, j] = table[2, j] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[0, j]).ToString();
                                    table[2, j] = 0;
                                    table[1, j] = table[3, j];
                                    table[3, j] = 0;
                                }
                                else if (table[2, j] == table[3, j])
                                {
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j]);
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 250, table[2, j]);
                                    makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 250, table[3, j], table[3, j]);
                                    table[0, j] = table[1, j];
                                    table[1, j] = table[3, j] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[1, j]).ToString();
                                    table[2, j] = 0;
                                    table[3, j] = 0;
                                }
                                else
                                {
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j]);
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 250, table[2, j]);
                                    makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 350, table[3, j]);
                                    table[0, j] = table[1, j];
                                    table[1, j] = table[2, j];
                                    table[2, j] = table[3, j];
                                    table[3, j] = 0;
                                }
                            }
                        }
                    }
                }
                else
                {
                    makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 150, table[0, j]);
                    if (table[1, j] == 0)
                    {
                        if (table[2, j] == 0)
                        {
                            if (table[3, j] == 0)
                            {
                                ;
                            }
                            else if (table[0, j] == table[3, j])
                            {
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 150, table[3, j], table[3, j]);
                                table[0, j] = table[3, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[0, j]).ToString();
                                table[3, j] = 0;
                            }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 250, table[3, j]);
                                table[1, j] = table[3, j];
                                table[3, j] = 0;
                            }
                        }
                        else if (table[2, j] == table[0, j])
                        {
                            makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 150, table[2, j], table[2, j]);
                            table[0, j] = table[2, j] << 1;
                            label_score.Text = (Convert.ToInt32(label_score.Text) + table[0, j]).ToString();
                            table[2, j] = 0;
                            if (table[3, j] != 0)
                            {
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 250, table[3, j]);
                                table[1, j] = table[3, j];
                                table[3, j] = 0;
                            }
                        }
                        else
                        {
                            if (table[3, j] == 0)
                            {
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 250, table[2, j]);
                                table[1, j] = table[2, j];
                                table[2, j] = 0;
                            }
                            else if (table[3, j] == table[2, j])
                            {
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 250, table[2, j]);
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 250, table[3, j], table[3, j]);
                                table[1, j] = table[3, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[1, j]).ToString();
                                table[2, j] = 0;
                                table[3, j] = 0;
                            }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 250, table[2, j]);
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 350, table[3, j]);
                                table[1, j] = table[2, j];
                                table[2, j] = table[3, j];
                                table[3, j] = 0;
                            }
                        }
                    }
                    else if (table[1, j] == table[0, j])
                    {
                        if (table[2, j] == 0)
                        {
                            if (table[3, j] == 0)
                            {
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j], table[1, j]);
                                table[0, j] = table[1, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[0, j]).ToString();
                                table[1, j] = 0;
                            }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j], table[1, j]);
                                table[0, j] = table[1, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[0, j]).ToString();
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 250, table[3, j]);
                                table[1, j] = table[3, j];
                                table[3, j] = 0;
                            }
                        }
                        else
                        {
                            if (table[3, j] == table[2, j])
                            {
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j], table[1, j]);
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 250, table[2, j]);
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 250, table[3, j], table[3, j]);
                                table[0, j] = table[1, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[0, j]).ToString();
                                table[1, j] = table[2, j];
                                table[1, j] = table[3, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[1, j]).ToString();
                                table[2, j] = 0;
                                table[3, j] = 0;
                            }
                            else
                            {
                                if (table[3, j] == 0)
                                {
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j], table[1, j]);
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 250, table[2, j]);
                                    table[0, j] = table[1, j] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[0, j]).ToString();
                                    table[1, j] = table[2, j];
                                    table[2, j] = 0;
                                }
                                else
                                {
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 150, table[1, j], table[1, j]);
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 250, table[2, j]);
                                    makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 350, table[3, j]);
                                    table[0, j] = table[1, j] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[0, j]).ToString();
                                    table[1, j] = table[2, j];
                                    table[2, j] = table[3, j];
                                    table[3, j] = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 250, table[1, j]);
                        if (table[2, j] == 0)
                        {
                            if (table[3, j] == 0)
                            {
                                ;
                            }
                            else if (table[3, j] == table[1, j])
                            {
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 250, table[3, j], table[3, j]);
                                table[1, j] = table[3, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[1, j]).ToString();
                                table[3, j] = 0;
                            }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 350, table[3, j]);
                                table[2, j] = table[3, j];
                                table[3, j] = 0;
                            }
                        }
                        else if (table[1, j] == table[2, j])
                        {
                            makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 250, table[2, j], table[2, j]);
                            table[1, j] = table[2, j] << 1;
                            label_score.Text = (Convert.ToInt32(label_score.Text) + table[1, j]).ToString();
                            table[2, j] = 0;
                            if (table[3, j] != 0)
                            {
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 350, table[3, j]);
                                table[2, j] = table[3, j];
                                table[3, j] = 0;
                            }
                        }
                        else if (table[2, j] == table[3, j])
                        {
                            makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 350, table[2, j]);
                            makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 350, table[3, j], table[3, j]);
                            table[2, j] = table[3, j] << 1;
                            label_score.Text = (Convert.ToInt32(label_score.Text) + table[2, j]).ToString();
                            table[3, j] = 0;
                        }
                        else
                        {
                            makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 350, table[2, j]);
                            if (table[3, j] != 0)
                            {
                                makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 450, table[3, j]);
                            }
                        }
                    }
                }
            }
            int score = Convert.ToInt32(label_score.Text);
            int best = Convert.ToInt32(label_best.Text);
            if (score > best) label_best.Text = score.ToString();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (lastStep[i, j] != table[i, j])
                    {
                        randomProduct();
                        showAnimation();
                        testIsWin();
                        if (isGameWin == 1)
                        {
                            Graphics g = (new PaintEventArgs(CreateGraphics(), ClientRectangle)).Graphics;
                            g.DrawImage(gameWin, 40, 140);
                            g.Dispose();
                        }
                        return;
                    }
                }
            }
            table = (int[,])lastStep.Clone();
        }
        private void downKeyDown()
        {
            testIsGameOver();
            if (isGameOver || isGameWin == 1)
            {
                return;
            }
            lastStep = (int[,])table.Clone();
            resetAnimation();
            for (int j = 0; j < 4; j++)
            {
                if (table[3, j] == 0)
                {
                    if (table[2, j] == 0)
                    {
                        if (table[1, j] == 0)
                        {
                            if (table[0, j] == 0)
                            {; }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 450, table[0, j]);
                                table[3, j] = table[0, j];
                                table[0, j] = 0;
                            }
                        }
                        else
                        {
                            if (table[0, j] == 0)
                            {
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 450, table[1, j]);
                                table[3, j] = table[1, j];
                                table[1, j] = 0;
                            }
                            else if (table[0, j] == table[1, j])
                            {
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 450, table[1, j]);
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 450, table[0, j], table[0, j]);
                                table[3, j] = table[1, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[3, j]).ToString();
                                table[1, j] = 0;
                                table[0, j] = 0;
                            }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 450, table[1, j]);
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 350, table[0, j]);
                                table[3, j] = table[1, j];
                                table[2, j] = table[0, j];
                                table[1, j] = 0;
                                table[0, j] = 0;
                            }
                        }
                    }
                    else
                    {
                        if (table[1, j] == 0)
                        {
                            if (table[0, j] == 0)
                            {
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j]);
                                table[3, j] = table[2, j];
                                table[2, j] = 0;
                            }
                            else if (table[2, j] == table[0, j])
                            {
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j]);
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 450, table[0, j], table[0, j]);
                                table[3, j] = table[2, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[3, j]).ToString();
                                table[2, j] = 0;
                                table[0, j] = 0;
                            }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j]);
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 350, table[0, j]);
                                table[3, j] = table[2, j];
                                table[2, j] = table[0, j];
                                table[0, j] = 0;
                            }
                        }
                        else
                        {
                            if (table[0, j] == 0)
                            {
                                if (table[2, j] == table[1, j])
                                {
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j]);
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 450, table[1, j], table[1, j]);
                                    table[3, j] = table[1, j] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[3, j]).ToString();
                                    table[2, j] = 0;
                                    table[1, j] = 0;
                                }
                                else
                                {
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j]);
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 350, table[1, j]);
                                    table[3, j] = table[2, j];
                                    table[2, j] = table[1, j];
                                    table[1, j] = 0;
                                }
                            }
                            else
                            {
                                if (table[2, j] == table[1, j])
                                {
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j]);
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 450, table[1, j], table[1, j]);
                                    makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 350, table[0, j]);
                                    table[3, j] = table[1, j] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[3, j]).ToString();
                                    table[1, j] = 0;
                                    table[2, j] = table[0, j];
                                    table[0, j] = 0;
                                }
                                else if (table[1, j] == table[0, j])
                                {
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j]);
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 350, table[1, j]);
                                    makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 350, table[0, j], table[0, j]);
                                    table[3, j] = table[2, j];
                                    table[2, j] = table[0, j] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[2, j]).ToString();
                                    table[1, j] = 0;
                                    table[0, j] = 0;
                                }
                                else
                                {
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j]);
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 350, table[1, j]);
                                    makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 250, table[0, j]);
                                    table[3, j] = table[2, j];
                                    table[2, j] = table[1, j];
                                    table[1, j] = table[0, j];
                                    table[0, j] = 0;
                                }
                            }
                        }
                    }
                }
                else
                {
                    makeComposedPictures(100 * j + 50, 450, 100 * j + 50, 450, table[3, j]);
                    if (table[2, j] == 0)
                    {
                        if (table[1, j] == 0)
                        {
                            if (table[0, j] == 0)
                            {
                                ;
                            }
                            else if (table[3, j] == table[0, j])
                            {
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 450, table[0, j], table[0, j]);
                                table[3, j] = table[0, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[3, j]).ToString();
                                table[0, j] = 0;
                            }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 350, table[0, j]);
                                table[2, j] = table[0, j];
                                table[0, j] = 0;
                            }
                        }
                        else if (table[1, j] == table[3, j])
                        {
                            makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 450, table[1, j], table[1, j]);
                            table[3, j] = table[1, j] << 1;
                            label_score.Text = (Convert.ToInt32(label_score.Text) + table[3, j]).ToString();
                            table[1, j] = 0;
                            if (table[0, j] != 0)
                            {
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 350, table[0, j]);
                                table[2, j] = table[0, j];
                                table[0, j] = 0;
                            }
                        }
                        else
                        {
                            if (table[0, j] == 0)
                            {
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 350, table[1, j]);
                                table[2, j] = table[1, j];
                                table[1, j] = 0;
                            }
                            else if (table[0, j] == table[1, j])
                            {
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 350, table[1, j]);
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 350, table[0, j], table[0, j]);
                                table[2, j] = table[0, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[2, j]).ToString();
                                table[1, j] = 0;
                                table[0, j] = 0;
                            }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 350, table[1, j]);
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 250, table[0, j]);
                                table[2, j] = table[1, j];
                                table[1, j] = table[0, j];
                                table[0, j] = 0;
                            }
                        }
                    }
                    else if (table[2, j] == table[3, j])
                    {
                        if (table[1, j] == 0)
                        {
                            if (table[0, j] == 0)
                            {
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j], table[2, j]);
                                table[3, j] = table[2, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[3, j]).ToString();
                                table[2, j] = 0;
                            }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j], table[2, j]);
                                table[3, j] = table[2, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[3, j]).ToString();
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 350, table[0, j]);
                                table[2, j] = table[0, j];
                                table[0, j] = 0;
                            }
                        }
                        else
                        {
                            if (table[0, j] == table[1, j])
                            {
                                makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j], table[2, j]);
                                makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 350, table[1, j]);
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 350, table[0, j], table[0, j]);
                                table[3, j] = table[2, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[3, j]).ToString();
                                table[2, j] = table[1, j];
                                table[2, j] = table[0, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[2, j]).ToString();
                                table[1, j] = 0;
                                table[0, j] = 0;
                            }
                            else
                            {
                                if (table[0, j] == 0)
                                {
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j], table[2, j]);
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 350, table[1, j]);
                                    table[3, j] = table[2, j] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[3, j]).ToString();
                                    table[2, j] = table[1, j];
                                    table[1, j] = 0;
                                }
                                else
                                {
                                    makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 450, table[2, j], table[2, j]);
                                    makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 350, table[1, j]);
                                    makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 250, table[0, j]);
                                    table[3, j] = table[2, j] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[3, j]).ToString();
                                    table[2, j] = table[1, j];
                                    table[1, j] = table[0, j];
                                    table[0, j] = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        makeComposedPictures(100 * j + 50, 350, 100 * j + 50, 350, table[2, j]);
                        if (table[1, j] == 0)
                        {
                            if (table[0, j] == 0)
                            {
                                ;
                            }
                            else if (table[0, j] == table[2, j])
                            {
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 350, table[0, j], table[0, j]);
                                table[2, j] = table[0, j] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[2, j]).ToString();
                                table[0, j] = 0;
                            }
                            else
                            {
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 250, table[0, j]);
                                table[1, j] = table[0, j];
                                table[0, j] = 0;
                            }
                        }
                        else if (table[2, j] == table[1, j])
                        {
                            makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 350, table[1, j], table[1, j]);
                            table[2, j] = table[1, j] << 1;
                            label_score.Text = (Convert.ToInt32(label_score.Text) + table[2, j]).ToString();
                            table[1, j] = 0;
                            if (table[0, j] != 0)
                            {
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 250, table[0, j]);
                                table[1, j] = table[0, j];
                                table[0, j] = 0;
                            }
                        }
                        else if (table[1, j] == table[0, j])
                        {
                            makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 250, table[1, j]);
                            makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 250, table[0, j], table[0, j]);
                            table[1, j] = table[0, j] << 1;
                            label_score.Text = (Convert.ToInt32(label_score.Text) + table[1, j]).ToString();
                            table[0, j] = 0;
                        }
                        else
                        {
                            makeComposedPictures(100 * j + 50, 250, 100 * j + 50, 250, table[1, j]);
                            if (table[0, j] != 0)
                            {
                                makeComposedPictures(100 * j + 50, 150, 100 * j + 50, 150, table[0, j]);
                            }
                        }
                    }
                }
            }
            int score = Convert.ToInt32(label_score.Text);
            int best = Convert.ToInt32(label_best.Text);
            if (score > best) label_best.Text = score.ToString();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (lastStep[i, j] != table[i, j])
                    {
                        randomProduct();
                        showAnimation();
                        testIsWin();
                        if (isGameWin == 1)
                        {
                            Graphics g = (new PaintEventArgs(CreateGraphics(), ClientRectangle)).Graphics;
                            g.DrawImage(gameWin, 40, 140);
                            g.Dispose();
                        }
                        return;
                    }
                }
            }
            table = (int[,])lastStep.Clone();
        }
        private void leftKeyDown()
        {
            testIsGameOver();
            if (isGameOver || isGameWin == 1)
            {
                return;
            }
            lastStep = (int[,])table.Clone();
            resetAnimation();
            for (int i = 0; i < 4; i++)
            {
                if (table[i, 0] == 0)
                {
                    if (table[i, 1] == 0)
                    {
                        if (table[i, 2] == 0)
                        {
                            if (table[i, 3] == 0)
                            {; }
                            else
                            {
                                makeComposedPictures(350, 100 * i + 150, 50, 100 * i + 150, table[i, 3]);
                                table[i, 0] = table[i, 3];
                                table[i, 3] = 0;
                            }
                        }
                        else
                        {
                            if (table[i, 3] == 0)
                            {
                                makeComposedPictures(250, 100 * i + 150, 50, 100 * i + 150, table[i, 2]);
                                table[i, 0] = table[i, 2];
                                table[i, 2] = 0;
                            }
                            else if (table[i, 3] == table[i, 2])
                            {
                                makeComposedPictures(250, 100 * i + 150, 50, 100 * i + 150, table[i, 2]);
                                makeComposedPictures(350, 100 * i + 150, 50, 100 * i + 150, table[i, 3], table[i, 3]);
                                table[i, 0] = table[i, 2] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 0]).ToString();
                                table[i, 2] = 0;
                                table[i, 3] = 0;
                            }
                            else
                            {
                                makeComposedPictures(250, 100 * i + 150, 50, 100 * i + 150, table[i, 2]);
                                makeComposedPictures(350, 100 * i + 150, 150, 100 * i + 150, table[i, 3]);
                                table[i, 0] = table[i, 2];
                                table[i, 1] = table[i, 3];
                                table[i, 2] = 0;
                                table[i, 3] = 0;
                            }
                        }
                    }
                    else
                    {
                        if (table[i, 2] == 0)
                        {
                            if (table[i, 3] == 0)
                            {
                                makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1]);
                                table[i, 0] = table[i, 1];
                                table[i, 1] = 0;
                            }
                            else if (table[i, 1] == table[i, 3])
                            {
                                makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1]);
                                makeComposedPictures(350, 100 * i + 150, 50, 100 * i + 150, table[i, 3], table[i, 3]);
                                table[i, 0] = table[i, 1] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 0]).ToString();
                                table[i, 1] = 0;
                                table[i, 3] = 0;
                            }
                            else
                            {
                                makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1]);
                                makeComposedPictures(350, 100 * i + 150, 150, 100 * i + 150, table[i, 3]);
                                table[i, 0] = table[i, 1];
                                table[i, 1] = table[i, 3];
                                table[i, 3] = 0;
                            }
                        }
                        else
                        {
                            if (table[i, 3] == 0)
                            {
                                if (table[i, 1] == table[i, 2])
                                {
                                    makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1]);
                                    makeComposedPictures(250, 100 * i + 150, 50, 100 * i + 150, table[i, 2], table[i, 2]);
                                    table[i, 0] = table[i, 2] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 0]).ToString();
                                    table[i, 1] = 0;
                                    table[i, 2] = 0;
                                }
                                else
                                {
                                    makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1]);
                                    makeComposedPictures(250, 100 * i + 150, 150, 100 * i + 150, table[i, 2]);
                                    table[i, 0] = table[i, 1];
                                    table[i, 1] = table[i, 2];
                                    table[i, 2] = 0;
                                }
                            }
                            else
                            {
                                if (table[i, 1] == table[i, 2])
                                {
                                    makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1]);
                                    makeComposedPictures(250, 100 * i + 150, 50, 100 * i + 150, table[i, 2], table[i, 2]);
                                    makeComposedPictures(350, 100 * i + 150, 150, 100 * i + 150, table[i, 3]);
                                    table[i, 0] = table[i, 2] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 0]).ToString();
                                    table[i, 2] = 0;
                                    table[i, 1] = table[i, 3];
                                    table[i, 3] = 0;
                                }
                                else if (table[i, 2] == table[i, 3])
                                {
                                    makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1]);
                                    makeComposedPictures(250, 100 * i + 150, 150, 100 * i + 150, table[i, 2]);
                                    makeComposedPictures(350, 100 * i + 150, 150, 100 * i + 150, table[i, 3], table[i, 3]);
                                    table[i, 0] = table[i, 1];
                                    table[i, 1] = table[i, 3] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 1]).ToString();
                                    table[i, 2] = 0;
                                    table[i, 3] = 0;
                                }
                                else
                                {
                                    makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1]);
                                    makeComposedPictures(250, 100 * i + 150, 150, 100 * i + 150, table[i, 2]);
                                    makeComposedPictures(350, 100 * i + 150, 250, 100 * i + 150, table[i, 3]);
                                    table[i, 0] = table[i, 1];
                                    table[i, 1] = table[i, 2];
                                    table[i, 2] = table[i, 3];
                                    table[i, 3] = 0;
                                }
                            }
                        }
                    }
                }
                else
                {
                    makeComposedPictures(50, 100 * i + 150, 50, 100 * i + 150, table[i, 0]);
                    if (table[i, 1] == 0)
                    {
                        if (table[i, 2] == 0)
                        {
                            if (table[i, 3] == 0)
                            {
                                ;
                            }
                            else if (table[i, 0] == table[i, 3])
                            {
                                makeComposedPictures(350, 100 * i + 150, 50, 100 * i + 150, table[i, 3], table[i, 3]);
                                table[i, 0] = table[i, 3] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 0]).ToString();
                                table[i, 3] = 0;
                            }
                            else
                            {
                                makeComposedPictures(350, 100 * i + 150, 150, 100 * i + 150, table[i, 3]);
                                table[i, 1] = table[i, 3];
                                table[i, 3] = 0;
                            }
                        }
                        else if (table[i, 2] == table[i, 0])
                        {
                            makeComposedPictures(250, 100 * i + 150, 50, 100 * i + 150, table[i, 2], table[i, 2]);
                            table[i, 0] = table[i, 2] << 1;
                            label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 0]).ToString();
                            table[i, 2] = 0;
                            if (table[i, 3] != 0)
                            {
                                makeComposedPictures(350, 100 * i + 150, 150, 100 * i + 150, table[i, 3]);
                                table[i, 1] = table[i, 3];
                                table[i, 3] = 0;
                            }
                        }
                        else
                        {
                            if (table[i, 3] == 0)
                            {
                                makeComposedPictures(250, 100 * i + 150, 150, 100 * i + 150, table[i, 2]);
                                table[i, 1] = table[i, 2];
                                table[i, 2] = 0;
                            }
                            else if (table[i, 3] == table[i, 2])
                            {
                                makeComposedPictures(250, 100 * i + 150, 150, 100 * i + 150, table[i, 2]);
                                makeComposedPictures(350, 100 * i + 150, 150, 100 * i + 150, table[i, 3], table[i, 3]);
                                table[i, 1] = table[i, 3] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 1]).ToString();
                                table[i, 2] = 0;
                                table[i, 3] = 0;
                            }
                            else
                            {
                                makeComposedPictures(250, 100 * i + 150, 150, 100 * i + 150, table[i, 2]);
                                makeComposedPictures(350, 100 * i + 150, 250, 100 * i + 150, table[i, 3]);
                                table[i, 1] = table[i, 2];
                                table[i, 2] = table[i, 3];
                                table[i, 3] = 0;
                            }
                        }
                    }
                    else if (table[i, 1] == table[i, 0])
                    {
                        if (table[i, 2] == 0)
                        {
                            if (table[i, 3] == 0)
                            {
                                makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1], table[i, 1]);
                                table[i, 0] = table[i, 1] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 0]).ToString();
                                table[i, 1] = 0;
                            }
                            else
                            {
                                makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1], table[i, 1]);
                                table[i, 0] = table[i, 1] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 0]).ToString();
                                makeComposedPictures(350, 100 * i + 150, 150, 100 * i + 150, table[i, 3]);
                                table[i, 1] = table[i, 3];
                                table[i, 3] = 0;
                            }
                        }
                        else
                        {
                            if (table[i, 3] == table[i, 2])
                            {
                                makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1], table[i, 1]);
                                makeComposedPictures(250, 100 * i + 150, 150, 100 * i + 150, table[i, 2]);
                                makeComposedPictures(350, 100 * i + 150, 150, 100 * i + 150, table[i, 3], table[i, 3]);
                                table[i, 0] = table[i, 1] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 0]).ToString();
                                table[i, 1] = table[i, 2];
                                table[i, 1] = table[i, 3] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 1]).ToString();
                                table[i, 2] = 0;
                                table[i, 3] = 0;
                            }
                            else
                            {
                                if (table[i, 3] == 0)
                                {
                                    makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1], table[i, 1]);
                                    makeComposedPictures(250, 100 * i + 150, 150, 100 * i + 150, table[i, 2]);
                                    table[i, 0] = table[i, 1] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 0]).ToString();
                                    table[i, 1] = table[i, 2];
                                    table[i, 2] = 0;
                                }
                                else
                                {
                                    makeComposedPictures(150, 100 * i + 150, 50, 100 * i + 150, table[i, 1], table[i, 1]);
                                    makeComposedPictures(250, 100 * i + 150, 150, 100 * i + 150, table[i, 2]);
                                    makeComposedPictures(350, 100 * i + 150, 250, 100 * i + 150, table[i, 3]);
                                    table[i, 0] = table[i, 1] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 0]).ToString();
                                    table[i, 1] = table[i, 2];
                                    table[i, 2] = table[i, 3];
                                    table[i, 3] = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        makeComposedPictures(150, 100 * i + 150, 150, 100 * i + 150, table[i, 1]);
                        if (table[i, 2] == 0)
                        {
                            if (table[i, 3] == 0)
                            {
                                ;
                            }
                            else if (table[i, 3] == table[i, 1])
                            {
                                makeComposedPictures(350, 100 * i + 150, 150, 100 * i + 150, table[i, 3], table[i, 3]);
                                table[i, 1] = table[i, 3] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 1]).ToString();
                                table[i, 3] = 0;
                            }
                            else
                            {
                                makeComposedPictures(350, 100 * i + 150, 250, 100 * i + 150, table[i, 3]);
                                table[i, 2] = table[i, 3];
                                table[i, 3] = 0;
                            }
                        }
                        else if (table[i, 1] == table[i, 2])
                        {
                            makeComposedPictures(250, 100 * i + 150, 150, 100 * i + 150, table[i, 2], table[i, 2]);
                            table[i, 1] = table[i, 2] << 1;
                            label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 1]).ToString();
                            table[i, 2] = 0;
                            if (table[i, 3] != 0)
                            {
                                makeComposedPictures(350, 100 * i + 150, 250, 100 * i + 150, table[i, 3]);
                                table[i, 2] = table[i, 3];
                                table[i, 3] = 0;
                            }
                        }
                        else if (table[i, 2] == table[i, 3])
                        {
                            makeComposedPictures(250, 100 * i + 150, 250, 100 * i + 150, table[i, 2]);
                            makeComposedPictures(350, 100 * i + 150, 250, 100 * i + 150, table[i, 3], table[i, 3]);
                            table[i, 2] = table[i, 3] << 1;
                            label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 2]).ToString();
                            table[i, 3] = 0;
                        }
                        else
                        {
                            makeComposedPictures(250, 100 * i + 150, 250, 100 * i + 150, table[i, 2]);
                            if (table[i, 3] != 0)
                            {
                                makeComposedPictures(350, 100 * i + 150, 350, 100 * i + 150, table[i, 3]);
                            }
                        }
                    }
                }
            }
            int score = Convert.ToInt32(label_score.Text);
            int best = Convert.ToInt32(label_best.Text);
            if (score > best) label_best.Text = score.ToString();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (lastStep[i, j] != table[i, j])
                    {
                        randomProduct();
                        showAnimation();
                        testIsWin();
                        if (isGameWin == 1)
                        {
                            Graphics g = (new PaintEventArgs(CreateGraphics(), ClientRectangle)).Graphics;
                            g.DrawImage(gameWin, 40, 140);
                            g.Dispose();
                        }
                        return;
                    }
                }
            }
            table = (int[,])lastStep.Clone();
        }
        private void rightKeyDown()
        {
            testIsGameOver();
            if (isGameOver || isGameWin == 1)
            {
                return;
            }
            lastStep = (int[,])table.Clone();
            resetAnimation();
            for (int i = 0; i < 4; i++)
            {
                if (table[i, 3] == 0)
                {
                    if (table[i, 2] == 0)
                    {
                        if (table[i, 1] == 0)
                        {
                            if (table[i, 0] == 0)
                            {; }
                            else
                            {
                                makeComposedPictures(50, 100 * i + 150, 350, 100 * i + 150, table[i, 0]);
                                table[i, 3] = table[i, 0];
                                table[i, 0] = 0;
                            }
                        }
                        else
                        {
                            if (table[i, 0] == 0)
                            {
                                makeComposedPictures(150, 100 * i + 150, 350, 100 * i + 150, table[i, 1]);
                                table[i, 3] = table[i, 1];
                                table[i, 1] = 0;
                            }
                            else if (table[i, 0] == table[i, 1])
                            {
                                makeComposedPictures(150, 100 * i + 150, 350, 100 * i + 150, table[i, 1]);
                                makeComposedPictures(50, 100 * i + 150, 350, 100 * i + 150, table[i, 0], table[i, 0]);
                                table[i, 3] = table[i, 1] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 3]).ToString();
                                table[i, 1] = 0;
                                table[i, 0] = 0;
                            }
                            else
                            {
                                makeComposedPictures(150, 100 * i + 150, 350, 100 * i + 150, table[i, 1]);
                                makeComposedPictures(50, 100 * i + 150, 250, 100 * i + 150, table[i, 0]);
                                table[i, 3] = table[i, 1];
                                table[i, 2] = table[i, 0];
                                table[i, 1] = 0;
                                table[i, 0] = 0;
                            }
                        }
                    }
                    else
                    {
                        if (table[i, 1] == 0)
                        {
                            if (table[i, 0] == 0)
                            {
                                makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2]);
                                table[i, 3] = table[i, 2];
                                table[i, 2] = 0;
                            }
                            else if (table[i, 2] == table[i, 0])
                            {
                                makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2]);
                                makeComposedPictures(50, 100 * i + 150, 350, 100 * i + 150, table[i, 0], table[i, 0]);
                                table[i, 3] = table[i, 2] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 3]).ToString();
                                table[i, 2] = 0;
                                table[i, 0] = 0;
                            }
                            else
                            {
                                makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2]);
                                makeComposedPictures(50, 100 * i + 150, 250, 100 * i + 150, table[i, 0]);
                                table[i, 3] = table[i, 2];
                                table[i, 2] = table[i, 0];
                                table[i, 0] = 0;
                            }
                        }
                        else
                        {
                            if (table[i, 0] == 0)
                            {
                                if (table[i, 2] == table[i, 1])
                                {
                                    makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2]);
                                    makeComposedPictures(150, 100 * i + 150, 350, 100 * i + 150, table[i, 1], table[i, 1]);
                                    table[i, 3] = table[i, 1] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 3]).ToString();
                                    table[i, 2] = 0;
                                    table[i, 1] = 0;
                                }
                                else
                                {
                                    makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2]);
                                    makeComposedPictures(150, 100 * i + 150, 250, 100 * i + 150, table[i, 1]);
                                    table[i, 3] = table[i, 2];
                                    table[i, 2] = table[i, 1];
                                    table[i, 1] = 0;
                                }
                            }
                            else
                            {
                                if (table[i, 2] == table[i, 1])
                                {
                                    makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2]);
                                    makeComposedPictures(150, 100 * i + 150, 350, 100 * i + 150, table[i, 1], table[i, 1]);
                                    makeComposedPictures(50, 100 * i + 150, 250, 100 * i + 150, table[i, 0]);
                                    table[i, 3] = table[i, 1] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 3]).ToString();
                                    table[i, 1] = 0;
                                    table[i, 2] = table[i, 0];
                                    table[i, 0] = 0;
                                }
                                else if (table[i, 1] == table[i, 0])
                                {
                                    makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2]);
                                    makeComposedPictures(150, 100 * i + 150, 250, 100 * i + 150, table[i, 1]);
                                    makeComposedPictures(50, 100 * i + 150, 250, 100 * i + 150, table[i, 0], table[i, 0]);
                                    table[i, 3] = table[i, 2];
                                    table[i, 2] = table[i, 0] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 2]).ToString();
                                    table[i, 1] = 0;
                                    table[i, 0] = 0;
                                }
                                else
                                {
                                    makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2]);
                                    makeComposedPictures(150, 100 * i + 150, 250, 100 * i + 150, table[i, 1]);
                                    makeComposedPictures(50, 100 * i + 150, 150, 100 * i + 150, table[i, 0]);
                                    table[i, 3] = table[i, 2];
                                    table[i, 2] = table[i, 1];
                                    table[i, 1] = table[i, 0];
                                    table[i, 0] = 0;
                                }
                            }
                        }
                    }
                }
                else
                {
                    makeComposedPictures(350, 100 * i + 150, 350, 100 * i + 150, table[i, 3]);
                    if (table[i, 2] == 0)
                    {
                        if (table[i, 1] == 0)
                        {
                            if (table[i, 0] == 0)
                            {
                                ;
                            }
                            else if (table[i, 3] == table[i, 0])
                            {
                                makeComposedPictures(50, 100 * i + 150, 350, 100 * i + 150, table[i, 0], table[i, 0]);
                                table[i, 3] = table[i, 0] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 3]).ToString();
                                table[i, 0] = 0;
                            }
                            else
                            {
                                makeComposedPictures(50, 100 * i + 150, 250, 100 * i + 150, table[i, 0]);
                                table[i, 2] = table[i, 0];
                                table[i, 0] = 0;
                            }
                        }
                        else if (table[i, 1] == table[i, 3])
                        {
                            makeComposedPictures(150, 100 * i + 150, 350, 100 * i + 150, table[i, 1], table[i, 1]);
                            table[i, 3] = table[i, 1] << 1;
                            label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 3]).ToString();
                            table[i, 1] = 0;
                            if (table[i, 0] != 0)
                            {
                                makeComposedPictures(50, 100 * i + 150, 250, 100 * i + 150, table[i, 0]);
                                table[i, 2] = table[i, 0];
                                table[i, 0] = 0;
                            }
                        }
                        else
                        {
                            if (table[i, 0] == 0)
                            {
                                makeComposedPictures(150, 100 * i + 150, 250, 100 * i + 150, table[i, 1]);
                                table[i, 2] = table[i, 1];
                                table[i, 1] = 0;
                            }
                            else if (table[i, 0] == table[i, 1])
                            {
                                makeComposedPictures(150, 100 * i + 150, 250, 100 * i + 150, table[i, 1]);
                                makeComposedPictures(50, 100 * i + 150, 250, 100 * i + 150, table[i, 0], table[i, 0]);
                                table[i, 2] = table[i, 0] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 2]).ToString();
                                table[i, 1] = 0;
                                table[i, 0] = 0;
                            }
                            else
                            {
                                makeComposedPictures(150, 100 * i + 150, 250, 100 * i + 150, table[i, 1]);
                                makeComposedPictures(50, 100 * i + 150, 150, 100 * i + 150, table[i, 0]);
                                table[i, 2] = table[i, 1];
                                table[i, 1] = table[i, 0];
                                table[i, 0] = 0;
                            }
                        }
                    }
                    else if (table[i, 2] == table[i, 3])
                    {
                        if (table[i, 1] == 0)
                        {
                            if (table[i, 0] == 0)
                            {
                                makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2], table[i, 2]);
                                table[i, 3] = table[i, 2] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 3]).ToString();
                                table[i, 2] = 0;
                            }
                            else
                            {
                                makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2], table[i, 2]);
                                table[i, 3] = table[i, 2] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 3]).ToString();
                                makeComposedPictures(50, 100 * i + 150, 250, 100 * i + 150, table[i, 0]);
                                table[i, 2] = table[i, 0];
                                table[i, 0] = 0;
                            }
                        }
                        else
                        {
                            if (table[i, 0] == table[i, 1])
                            {
                                makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2], table[i, 2]);
                                makeComposedPictures(150, 100 * i + 150, 250, 100 * i + 150, table[i, 1]);
                                makeComposedPictures(50, 100 * i + 150, 250, 100 * i + 150, table[i, 0], table[i, 0]);
                                table[i, 3] = table[i, 2] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 3]).ToString();
                                table[i, 2] = table[i, 1];
                                table[i, 2] = table[i, 0] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 2]).ToString();
                                table[i, 1] = 0;
                                table[i, 0] = 0;
                            }
                            else
                            {
                                if (table[i, 0] == 0)
                                {
                                    makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2], table[i, 2]);
                                    makeComposedPictures(150, 100 * i + 150, 250, 100 * i + 150, table[i, 1]);
                                    table[i, 3] = table[i, 2] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 3]).ToString();
                                    table[i, 2] = table[i, 1];
                                    table[i, 1] = 0;
                                }
                                else
                                {
                                    makeComposedPictures(250, 100 * i + 150, 350, 100 * i + 150, table[i, 2], table[i, 2]);
                                    makeComposedPictures(150, 100 * i + 150, 250, 100 * i + 150, table[i, 1]);
                                    makeComposedPictures(50, 100 * i + 150, 150, 100 * i + 150, table[i, 0]);
                                    table[i, 3] = table[i, 2] << 1;
                                    label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 3]).ToString();
                                    table[i, 2] = table[i, 1];
                                    table[i, 1] = table[i, 0];
                                    table[i, 0] = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        makeComposedPictures(250, 100 * i + 150, 250, 100 * i + 150, table[i, 2]);
                        if (table[i, 1] == 0)
                        {
                            if (table[i, 0] == 0)
                            {
                                ;
                            }
                            else if (table[i, 0] == table[i, 2])
                            {
                                makeComposedPictures(50, 100 * i + 150, 250, 100 * i + 150, table[i, 0], table[i, 0]);
                                table[i, 2] = table[i, 0] << 1;
                                label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 2]).ToString();
                                table[i, 0] = 0;
                            }
                            else
                            {
                                makeComposedPictures(50, 100 * i + 150, 150, 100 * i + 150, table[i, 0]);
                                table[i, 1] = table[i, 0];
                                table[i, 0] = 0;
                            }
                        }
                        else if (table[i, 2] == table[i, 1])
                        {
                            makeComposedPictures(150, 100 * i + 150, 250, 100 * i + 150, table[i, 1], table[i, 1]);
                            table[i, 2] = table[i, 1] << 1;
                            label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 2]).ToString();
                            table[i, 1] = 0;
                            if (table[i, 0] != 0)
                            {
                                makeComposedPictures(50, 100 * i + 150, 150, 100 * i + 150, table[i, 0]);
                                table[i, 1] = table[i, 0];
                                table[i, 0] = 0;
                            }
                        }
                        else if (table[i, 1] == table[i, 0])
                        {
                            makeComposedPictures(150, 100 * i + 150, 150, 100 * i + 150, table[i, 1]);
                            makeComposedPictures(50, 100 * i + 150, 150, 100 * i + 150, table[i, 0], table[i, 0]);
                            table[i, 1] = table[i, 0] << 1;
                            label_score.Text = (Convert.ToInt32(label_score.Text) + table[i, 1]).ToString();
                            table[i, 0] = 0;
                        }
                        else
                        {
                            makeComposedPictures(150, 100 * i + 150, 150, 100 * i + 150, table[i, 1]);
                            if (table[i, 0] != 0)
                            {
                                makeComposedPictures(50, 100 * i + 150, 50, 100 * i + 150, table[i, 0]);
                            }
                        }
                    }
                }
            }
            int score = Convert.ToInt32(label_score.Text);
            int best = Convert.ToInt32(label_best.Text);
            if (score > best) label_best.Text = score.ToString();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (lastStep[i, j] != table[i, j])
                    {
                        randomProduct();
                        showAnimation();
                        testIsWin();
                        if (isGameWin == 1)
                        {
                            Graphics g = (new PaintEventArgs(CreateGraphics(), ClientRectangle)).Graphics;
                            g.DrawImage(gameWin, 40, 140);
                            g.Dispose();
                        }
                        return;
                    }
                }
            }
            table = (int[,])lastStep.Clone();
        }
        //修复Tab键引起显示错误的bug
        void fixTab()
        {
            print2048();
        }
        // 禁掉清除背景消息
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) return;
            base.WndProc(ref m);
        }
        //控制鼠标拖拽窗口
        private bool isMouseDown = false;
        private Point FormLocation;
        private Point mouseOffset;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            print2048();
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                FormLocation = Location;
                mouseOffset = MousePosition;
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int _x = 0;
            int _y = 0;
            if (isMouseDown)
            {
                Point pt = MousePosition;
                _x = mouseOffset.X - pt.X;
                _y = mouseOffset.Y - pt.Y;
                Location = new Point(FormLocation.X - _x, FormLocation.Y - _y);
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }
        //关闭、最小化、About按钮hover
        private void button_Close_MouseHover(object sender, EventArgs e)
        {
            button_close.Text = "×";
            button_minimum.Text = "-";
            button_About.Text = "?";
        }
        private void button_Exit_MouseLeave(object sender, EventArgs e)
        {
            button_close.Text = "";
            button_minimum.Text = "";
            button_About.Text = "";
        }
        private void button_Minimum_MouseHover(object sender, EventArgs e)
        {
            button_close.Text = "×";
            button_minimum.Text = "-";
            button_About.Text = "?";
        }
        private void button_Minimum_MouseLeave(object sender, EventArgs e)
        {
            button_close.Text = "";
            button_minimum.Text = "";
            button_About.Text = "";
        }
        private void button_About_MouseHover(object sender, EventArgs e)
        {
            button_close.Text = "×";
            button_minimum.Text = "-";
            button_About.Text = "?";
        }
        private void button_About_MouseLeave(object sender, EventArgs e)
        {
            button_close.Text = "";
            button_minimum.Text = "";
            button_About.Text = "";
        }
        [DllImport("user32.dll", EntryPoint = "AnimateWindow")]
        private static extern bool AnimateWindow(IntPtr handle, int ms, int flags);
        //退出、最小化、New Game按钮
        private void button_Exit_Click(object sender, EventArgs e)
        {
            exitGame();
        }
        private void button_Minimum_Click(object sender, EventArgs e)
        {
            AnimateWindow(Handle, 300, 0x90000); // 淡入淡出
            WindowState = FormWindowState.Minimized;
        }
        private void button_NewGame_Click(object sender, EventArgs e)
        {
            resetGame();
        }
        private void button_About_Click(object sender, EventArgs e)
        {
            Graphics g = (new PaintEventArgs(CreateGraphics(), ClientRectangle)).Graphics;
            g.DrawImage(aboutMe, 40, 140);
            g.Dispose();
        }
    }
}
