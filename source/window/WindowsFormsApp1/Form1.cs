using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Encoder = System.Drawing.Imaging.Encoder;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Image screenshot;
        private string path;
        private string filename;

        public Form1()
        {
            InitializeComponent();
            if (Clipboard.ContainsImage())
            {
                screenshot = Clipboard.GetImage();
                path = "C:\\Users\\" + Environment.UserName + "\\Pictures";
                string date = DateTime.Now.ToString();
                filename = "Screenshot_" + date.Replace(':', '.').Replace(' ', '_') + ".jpg";

                pictureBox1.Image = screenshot;
                textBox1.Text = path + "\\" + filename;
            }
            else this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string str = textBox1.Text;

                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(Encoder.Quality, trackBar1.Value);
                myEncoderParameters.Param[0] = myEncoderParameter;
                screenshot.Save(str, GetEncoderInfo("image/jpeg"), myEncoderParameters);
            } catch (Exception) {}
            this.Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar1, trackBar1.Value.ToString() + "%");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath + "\\" + filename;
            }
        }

        private ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}
