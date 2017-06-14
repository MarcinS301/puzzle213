using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace puzzle
{
    public partial class Form1 : Form
    {
        Point Point;
        ArrayList images = new ArrayList();
        public Form1()
        {
            Point.X = 150;
            Point.Y = 150;

            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cropImageTomages(Image orginal, int w, int h)
        {
            Bitmap bmp = new Bitmap(w, h);

            Graphics graphic = Graphics.FromImage(bmp);

            graphic.DrawImage(orginal, 0, 0, w, h);

            graphic.Dispose();

            int movr = 0, movd = 0;

            for (int x = 0; x < 8; x++)
            {
                Bitmap piece = new Bitmap(90, 90);

                for (int i = 0; i < 90; i++)
                    for (int j = 0; j < 90; j++)
                        piece.SetPixel(i, j,bmp.GetPixel(i + movr, j + movd));

                images.Add(piece);

                movr += 90;

                if (movr == 300)
                {
                    movr = 0;
                    movd += 90;
                }
            }

        }


        private void button10_Click(object sender, EventArgs e)
        {
            foreach (Button b in panel1.Controls)
                b.Enabled = true;

            Image orginal = Image.FromFile(@"C:\Users\Marcin\Desktop\das\2.jpg");

            cropImageTomages(orginal, 300, 300);

            AddImagesToButtons(images);

        }
        private void AddImagesToButtons(ArrayList images)
        {
            int i = 0;
            int[] arr = {0, 1, 2, 3, 4, 5, 6, 7 };


            arr = suffle(arr);

            foreach (Button b in panel1.Controls)
            {
                if (i < arr.Length)
                {
                    b.Image = (Image)images[arr[i]];
                    i++;
                }
            }


        }
        private int[] suffle(int[] arr)
        {
            Random rand = new Random();
            arr = arr.OrderBy(x => rand.Next()).ToArray();

            return arr;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MoveButton((Button)sender);
        }

        private void MoveButton(Button btn)
        {
            if (((btn.Location.X == Point.X - 90 || btn.Location.X == Point.X + 90)
          && btn.Location.Y == Point.Y)
          || (btn.Location.Y == Point.Y - 90 || btn.Location.Y == Point.Y + 90)
          && btn.Location.X == Point.X)
            {
                Point swap = btn.Location;
                btn.Location = Point;
                Point = swap;
            }

            if (Point.X == 180 && Point.Y == 180)
                CheckValid();
        }

        private void CheckValid()
        {
            int c = 0, index;
            foreach (Button btn in panel1.Controls)
            {
                index = (btn.Location.Y / 90) * 3 + btn.Location.X / 90;
                if (images[index] == btn.Image)
                    c++;
            }
            if (c == 8)
                MessageBox.Show("Chcialo sie panu w to grac?");
        }
    }
  



}