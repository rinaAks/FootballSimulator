using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace lab10football
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        List<string> names = new List<string> { "звёздочки", "фиалки", "подсолнухи", "ромашки", "бабочки", "ягодки", "карамельки", "барселона" };

        public Form1()
        {
            InitializeComponent();
            foreach (Control control in panel1.Controls)
            {
                if (control is Label)
                {
                    Label label = (Label)control;
                    label.Text = "Команда";
                }
            }
            names = names.OrderBy(x => random.Next()).ToList();

            int ind = 0;
            Label[] labels = { label1, label2, label3, label4, label5, label6, label7, label8 };
            foreach (Label label in labels)
            {
                label.Text = names[ind];
                ind += 1;
            }
            /*
            foreach (Label label in panel2.Controls.OfType<Label>().Take(4))
            {
                //int randomNumber = random.Next(0, names.Count);  // генерируем случайное число в пределах коллекции значений
                label.Text = names[ind];  // присваиваем значение из списка значения лейблу
                ind += 1;
            }*/

        }

        List<double> lyambdas = new List<double> { 2, 4, 1, 3, 2, 3, 2, 4 };
        List<int> scores = new List<int>();
        List<int> winRound1lyambdaI = new List<int> { };
        List<string> winRound1name = new List<string>();
        int matchCounter = 0;
        private void btNext_Click(object sender, EventArgs e)
        {
            if (matchCounter == 0) playMatch(label1, label2, label12, 0);
            else if (matchCounter == 1) playMatch(label3, label4, label11, 2);
            else if (matchCounter == 2) playMatch(label5, label6, label9, 4);
            else if (matchCounter == 3) playMatch(label7, label8, label10, 6);
            else if (matchCounter == 4) playMatch(label12, label11, label13, winRound1lyambdaI[0]);
            else if (matchCounter == 5) playMatch(label9, label10, label14, winRound1lyambdaI[2]);
            matchCounter++;
        }   
        // мысль: сделать класс с полями label и лямбда, работать с объектами класса
        public void playMatch(Label lbT1, Label lbT2, Label lbWinner, int lyambdaI)
        {
            scores.Clear();
            scores = new List<int>(new int[2]);
            scores[0] = 0; scores[1] = 0;
            lbT1.ForeColor = Color.Blue;
            lbT2.ForeColor = Color.Blue;
            for (int i = 0; i < 2; i++)
            {
                double sum = 0;
                while (true)
                {
                    // int value = random.Next(0, 5);
                    double value = (double)random.NextDouble();
                    sum += Math.Log10(value);
                    if (sum < (-1 * lyambdas[lyambdaI + i]))
                    {
                        break;
                    }
                    scores[i] += 1;
                }
            }
            lbScore1.Text = scores[0].ToString();
            lbScore2.Text = scores[1].ToString();
            if (matchCounter < 4)
            {
                lbTeam1.Text = names[lyambdaI];
                lbTeam2.Text = names[lyambdaI + 1];
            }
            else if(matchCounter == 4) 
            {
                lbTeam1.Text = winRound1name[0];
                lbTeam2.Text = winRound1name[1];
            }
            else if (matchCounter == 5)
            {
                lbTeam1.Text = winRound1name[2];
                lbTeam2.Text = winRound1name[3];
            }
            lbWinner.Visible = true;
            lbWinner.ForeColor = Color.Green;
            if (scores[0] > scores[1])
            {
                lbWinner.Text = names[lyambdaI];
                if(matchCounter < 4)
                {
                    winRound1lyambdaI.Add(lyambdaI);
                    winRound1name.Add(names[lyambdaI]);
                }
            }
            else
            {
                lbWinner.Text = names[lyambdaI + 1];
                if (matchCounter < 4)
                {
                    winRound1lyambdaI.Add(lyambdaI + 1);
                    winRound1name.Add(names[lyambdaI + 1]);
                }
            }
        }
    }
}
