﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SannikovaVika_Filters_1
{
    public partial class Form1 : Form
    {
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All Files(*.*) | *.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
               image = new Bitmap(dialog.FileName);
            }
            pictureBox1.Image = image;
            pictureBox1.Refresh();
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void гауссовФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void чернобелыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void увеличениеЯркостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new IncreaseВrightness();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void повышениеРезкостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new EmbossedFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void motionBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MotionBlurFilter(3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void эффектСтеклаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GlassEffectFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void вертикальныеВолныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new VerticalWavesFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void горизонтальныеВолныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new HorizontalWavesFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrayWorldFilter filter = new GrayWorldFilter();
            filter.FindAvg(image);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void линейноеРастяжениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearStretchingFilter filter = new LinearStretchingFilter();
            filter.FindMaxMin(image);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MedianFilter(3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        //private void gradToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    bool[,] mask = new bool[3, 3] { { false, true, false }, { true, true, true }, { false, true, false } };
        //    pictureBox1.Image = MathMorfology.GradFilter(image, mask);
        //    pictureBox1.Refresh();
        //}

        //расширение
        private void крестик3х3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[3, 3] { { false, true, false }, 
                                             { true, true, true }, 
                                            { false, true, false } };
            pictureBox1.Image = MathMorfology.Dilation(image, mask);
            pictureBox1.Refresh();
        }
        private void квадрат3х3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[3, 3] { { true, true, true }, 
                                            { true, true, true }, 
                                            { true, true, true } };
            pictureBox1.Image = MathMorfology.Dilation(image, mask);
            pictureBox1.Refresh();
        }
        private void квадрат5х5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[5, 5] { { true, true, true, true, true }, 
                                            { true, true, true, true, true }, 
                                            { true, true, true, true, true }, 
                                            { true, true, true, true, true }, 
                                            { true, true, true, true, true } };
            pictureBox1.Image = MathMorfology.Dilation(image, mask);
            pictureBox1.Refresh();
        }

        //сужение
        private void крестик3х3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[3, 3] { { false, true, false }, { true, true, true }, { false, true, false } };
            pictureBox1.Image = MathMorfology.Erosion(image, mask);
            pictureBox1.Refresh();
        }
        private void квадрат3х3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[3, 3] { { true, true, true },
                                            { true, true, true },
                                            { true, true, true } };
            pictureBox1.Image = MathMorfology.Erosion(image, mask);
            pictureBox1.Refresh();
        }
        private void квадрат5х5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[5, 5] { { true, true, true, true, true },
                                            { true, true, true, true, true },
                                            { true, true, true, true, true },
                                            { true, true, true, true, true },
                                            { true, true, true, true, true } };
            pictureBox1.Image = MathMorfology.Erosion(image, mask);
            pictureBox1.Refresh();
        }

        //открытие
        private void крестик3х3ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[3, 3] { { false, true, false }, { true, true, true }, { false, true, false } };
            pictureBox1.Image = MathMorfology.Opening(image, mask);
            pictureBox1.Refresh();
        }
        private void квадрат3х3ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[3, 3] { { true, true, true },
                                            { true, true, true },
                                            { true, true, true } };
            pictureBox1.Image = MathMorfology.Opening(image, mask);
            pictureBox1.Refresh();
        }
        private void квадрат5х5ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[5, 5] { { true, true, true, true, true },
                                            { true, true, true, true, true },
                                            { true, true, true, true, true },
                                            { true, true, true, true, true },
                                            { true, true, true, true, true } };
            pictureBox1.Image = MathMorfology.Opening(image, mask);
            pictureBox1.Refresh();
        }

        //закрытие
        private void крестик3х3ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[3, 3] { { false, true, false }, { true, true, true }, { false, true, false } };
            pictureBox1.Image = MathMorfology.Closing(image, mask);
            pictureBox1.Refresh();
        }
        private void квадрат3х3ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[3, 3] { { true, true, true },
                                            { true, true, true },
                                            { true, true, true } };
            pictureBox1.Image = MathMorfology.Closing(image, mask);
            pictureBox1.Refresh();
        }
        private void квадрат5х5ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[5, 5] { { true, true, true, true, true },
                                            { true, true, true, true, true },
                                            { true, true, true, true, true },
                                            { true, true, true, true, true },
                                            { true, true, true, true, true } };
            pictureBox1.Image = MathMorfology.Closing(image, mask);
            pictureBox1.Refresh();
        }

        //gradient
        private void крестик3х3ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[3, 3] { { false, true, false }, { true, true, true }, { false, true, false } };
            pictureBox1.Image = MathMorfology.GradFilter(image, mask);
            pictureBox1.Refresh();
        }
        private void квадрат3х3ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[3, 3] { { true, true, true },
                                            { true, true, true },
                                            { true, true, true } };
            pictureBox1.Image = MathMorfology.GradFilter(image, mask);
            pictureBox1.Refresh();
        }
        private void квадрат5х5ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[5, 5] { { true, true, true, true, true },
                                            { true, true, true, true, true },
                                            { true, true, true, true, true },
                                            { true, true, true, true, true },
                                            { true, true, true, true, true } };
            pictureBox1.Image = MathMorfology.GradFilter(image, mask);
            pictureBox1.Refresh();
        }

        private void newFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool[,] mask = new bool[3, 3] { { false, true, false }, { true, true, true }, { false, true, false } };
            Bitmap resultImage = new Bitmap(image.Width, image.Height);
            Bitmap newImage1 = new Bitmap(image.Width, image.Height);
            Bitmap newImage2 = new Bitmap(image.Width, image.Height);
            newImage2 = MathMorfology.GradFilter(image, mask);
            Filters filter = new SobelFilter();
            newImage1 = filter.processImage(image);
            int R, G, B;

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    R = newImage1.GetPixel(i, j).R - newImage2.GetPixel(i, j).R;
                    G = newImage1.GetPixel(i, j).G - newImage2.GetPixel(i, j).G;
                    B = newImage1.GetPixel(i, j).B - newImage2.GetPixel(i, j).B;
                    R = Clamp(R, 0, 255);
                    G = Clamp(G, 0, 255);
                    B = Clamp(B, 0, 255);
                    resultImage.SetPixel(i, j, Color.FromArgb(R, G, B));
                }
            }
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }
        public int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
