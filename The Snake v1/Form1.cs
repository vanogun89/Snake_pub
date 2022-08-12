using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Snake_v1
{
    public partial class Form1 : Form
    {
        private PictureBox fruits;
        private int rI, rJ;
        private PictureBox[] snake = new PictureBox[1500];
        private Label ScoreL = new Label();
        private int refX, refY;
        private int _windth = 500;
        private int _height = 500;
        private int _sideS = 10;
        private int score = 0;

        public Form1()
        {
            InitializeComponent();
            this.Text = "The Snake";
            this.Width = _windth + 100;
            this.Height = _height + 10;
            this.KeyDown += new KeyEventHandler(OKP);
            snake[0] = new PictureBox
            {
                Location = new Point(200, 200),
                Size = new Size(_sideS, _sideS),
                BackColor = Color.Green
            };
            this.Controls.Add(snake[0]);
            mapS();
            refX = 1;
            refY = 0;
            ScoreL.Text = "Score: 0";
            ScoreL.Location = new Point(500, 10);
            this.Controls.Add(ScoreL);
            timer1.Tick += new EventHandler(_update);
            timer1.Interval = 150;
            timer1.Start();
            fruits = new PictureBox();
            fruits.Size = new Size(10, 10);
            fruits.BackColor = Color.Red;
            genTheFruits();
        }
        private void eatFruits()
        {
            if ((snake[0].Location.X == rI) && (snake[0].Location.Y == rJ))
            {
                ScoreL.Text = "Score: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + 10 * refX, snake[score - 1].Location.Y + 10 * refY);
                snake[score].Size = new Size(_sideS, _sideS);
                snake[score].BackColor = Color.Green;
                this.Controls.Add(snake[score]);
                genTheFruits();
            }
        }

        private void theWall()
        {
            if ((snake[0].Location.X < 0) || (snake[0].Location.Y < 0) || (snake[0].Location.X > 470) || (snake[0].Location.Y > 460))
            {
                timer1.Stop();
            }
        }

        private void mapS()
        {
            for (int i = 0; i < _windth / _sideS - 1; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(_sideS * i, 0);
                pic.Size = new Size(1, _windth - 30);
                this.Controls.Add(pic);
            }
            for (int i = 0; i < _windth / _sideS - 2; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, _sideS * i);
                pic.Size = new Size(_height - 20, 1);
                this.Controls.Add(pic);
            }
        }

        private void eatMYself()
        {
            for (int i = 1; i<score ; i++)
            {
                if(snake[0].Location == snake[i].Location)
                {
                    for (int j = i; j <= score; j++)
                        this.Controls.Remove(snake[j]);
                    score -= score - i + 1;
                }
            }
        }

        private void genTheFruits()
        {
            Random r = new Random();
            rI = r.Next(0, _windth - _sideS);
            int tempI = rI % _sideS;
            rI -= tempI;
            rJ = r.Next(0, _windth - _sideS);
            int tempJ = rJ % _sideS;
            rJ -= tempJ;
            if ((rI<0)||(rI>470))
            {
                rI -= 20;
            }
            if ((rJ < 0) || (rJ > 460))
            {
                rJ -= 20;
            }
            fruits.Location = new Point(rI, rJ);
            this.Controls.Add(fruits);
        }

        private void moveTheSN()
        {
            for (int i = score; i >= 1; i--)
            {

                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + refX * _sideS, snake[0].Location.Y + refY * _sideS);
            eatMYself();
        }


        private void _update (object myObject, EventArgs eventsArgs)
        {
            eatFruits();
            moveTheSN();
            theWall();

        }
        private void OKP(object semder, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    refX = 1;
                    refY = 0;
                    break;
                case "Left":
                    refX = -1;
                    refY = 0;
                    break;
                case "Up":
                    refX = 0;
                    refY = -1;
                    break;
                case "Down":
                    refX = 0;
                    refY = 1;
                    break;

            }

           

        }
    }
}
