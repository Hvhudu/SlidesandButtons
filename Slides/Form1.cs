using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Slides
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        List<Bitmap> images = new List<Bitmap>();
        int count = 0;
        Bitmap one;
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 500;
            timer.Tick += next_Click;
        }
        private void open_Click(object sender, EventArgs e)
        {
            stop_Click(null, null);
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                timer.Stop();
                if (images.Count != 0) 
                {
                    foreach (var item in images)
                    {
                        item.Dispose();
                    }
                    images.Clear();
                }

                DirectoryInfo info = new DirectoryInfo(folder.SelectedPath);
                IEnumerable<FileInfo> files = info.EnumerateFiles();
                foreach(var item in files)
                {
                    string ex = Path.GetExtension(item.FullName);
                    if (ex == ".bmp" || ex == ".jpg" || ex == ".jpeg" || ex == ".png")
                    {
                        Bitmap pt = new Bitmap(item.FullName);
                        Size pt_size = pictureBox1.Size;
                        images.Add(new Bitmap(pt, pictureBox1.Size));
                    }
                   
                }
            }
            count = 0;
            label1.Text = " ";
            pictureBox1.Image = images[count];
            label1.Text = Convert.ToString((count + 1) + "/" + images.Count);
        }


        private void next_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                return;
            }
            count++;
            if (count >= images.Count)
            {
                count = 0;
            }
            pictureBox1.Image = images[count];
            label1.Text = Convert.ToString((count + 1) + "/" + images.Count);
        }

        private void stop_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void prev_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                return;
            }
            count--;
            if (count >= images.Count)
            {
                count = 0;
            }
            pictureBox1.Image = images[count];
            label1.Text = Convert.ToString((count + 1) + "/" + images.Count);
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (images.Count != 0)
            {
                timer.Start();
            }
            else
            {
                MessageBox.Show("Папка не выбрана");
            }
        }
    }
}
