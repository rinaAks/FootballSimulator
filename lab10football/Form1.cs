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
        List<Team> teams = new List<Team>();
        List<Team> teamsWinners = new List<Team>();
        List<int> scores = new List<int>();
        List<Label> labels = new List<Label>(8);
        List<Label> labelsWinners = new List<Label>(4);

        int labelCounter = 0, labelCounter2 = 0, matchCounter = 0;
        int[] scoreCurrentTeams = new int[2];
        public Form1()
        {
            InitializeComponent();
            InitializeData();
            foreach (Control control in panel1.Controls)
            {
                if (control is Label)
                {
                    Label label = (Label)control;
                    label.Text = "Команда";
                }
            }
            teams = teams.OrderBy(x => random.Next()).ToList();

            int ind = 0; 
            Label[] labelsTemp = { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14 };

            for (int i = 0; i < 8; i++)
            {
                labels.Add(labelsTemp[i]);
            }
            for (int i = 8; i < 14; i++)
            {
                labelsWinners.Add(labelsTemp[i]);
            }

            foreach (Label label in labels)
            {
                label.Text = teams[ind].Name;
                ind += 1;
            }
        }

        private void InitializeData()
        {
            teams.Add(new Team("звёздочки", 2));
            teams.Add(new Team("фиалки", 4));
            teams.Add(new Team("ромашки", 1));
            teams.Add(new Team("бабочки", 3));
            teams.Add(new Team("ягодки", 2));
            teams.Add(new Team("подсолнухи", 3));
            teams.Add(new Team("карамельки", 2));
            teams.Add(new Team("барселона", 4));
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            if (matchCounter < 4) playFirstRound();
            else if (matchCounter < 6) playSecondRound();
            else if (matchCounter == 6) playThirdRound();
            matchCounter++;
        }


        public void playFirstRound()
        {
            labels[labelCounter].ForeColor = Color.Blue;
            labels[labelCounter + 1].ForeColor = Color.Blue;

            PoissonDistr();

            lbTeam1.Text = teams[labelCounter].Name;
            lbTeam2.Text = teams[labelCounter + 1].Name;

            if (scoreCurrentTeams[0] > scoreCurrentTeams[1])
            {
                labelsWinners[matchCounter].Text = teams[labelCounter].Name;
                teamsWinners.Add(new Team(teams[labelCounter].Name, labelCounter));
            }
            else
            {
                labelsWinners[matchCounter].Text = teams[labelCounter + 1].Name;
                teamsWinners.Add(new Team(teams[labelCounter + 1].Name, labelCounter + 1));
            }

            labelsWinners[matchCounter].Visible = true;
            labelsWinners[matchCounter].ForeColor = Color.Green;
            labelCounter += 2;
        }
        public void playSecondRound()
        {
            labelsWinners[labelCounter2].ForeColor = Color.Pink;
            labelsWinners[labelCounter2 + 1].ForeColor = Color.Pink;

            PoissonDistr();

            lbTeam1.Text = teamsWinners[labelCounter2].Name;
            lbTeam2.Text = teamsWinners[labelCounter2 + 1].Name;

            if (scoreCurrentTeams[0] > scoreCurrentTeams[1])
            {
                labelsWinners[matchCounter].Text = teamsWinners[labelCounter2].Name;
                teamsWinners.Add(new Team(teamsWinners[labelCounter2].Name, labelCounter2));
            }
            else
            {
                labelsWinners[matchCounter].Text = teamsWinners[labelCounter2 + 1].Name;
                teamsWinners.Add(new Team(teamsWinners[labelCounter2 + 1].Name, labelCounter2 + 1));
            }

            labelsWinners[matchCounter].Visible = true;
            labelsWinners[matchCounter].ForeColor = Color.Green;
            labelCounter2 += 2;
        }
        public void playThirdRound()
        {
            label13.ForeColor = Color.Purple;
            label14.ForeColor = Color.Purple;
            foreach (Label label in labels)
            {
                label.ForeColor = Color.Gray;
            }
            foreach (Label label in labelsWinners)
            {
                label.ForeColor = Color.Gray;
            }

            PoissonDistr();

            lbTeam1.Text = teamsWinners[4].Name;
            lbTeam2.Text = teamsWinners[5].Name;

            if (scoreCurrentTeams[0] > scoreCurrentTeams[1])
            {
                label16.Text = teamsWinners[4].Name;
            }
            else
            {
                label16.Text = teamsWinners[5].Name;
            }
            label15.Visible = true;
            label16.Visible = true;
        }

        public void PoissonDistr()
        {
            scoreCurrentTeams[0] = 0; scoreCurrentTeams[1] = 0;
            for (int i = 0; i < 2; i++)
            {
                double sum = 0;
                while (true)
                {
                    // int value = random.Next(0, 5);
                    double value = (double)random.NextDouble();
                    sum += Math.Log10(value);
                    if ((matchCounter < 4 && sum < (-1 * teams[labelCounter + i].Lyambda)) || (matchCounter >= 4 && sum < (-1 * teamsWinners[labelCounter2 + i].Lyambda)))
                    {
                        break;
                    }
                    else if (matchCounter == 6 && sum < (-1 * teamsWinners[4 + i].Lyambda)) break;
                    scoreCurrentTeams[i] += 1;
                }
            }

            lbScore1.Text = scoreCurrentTeams[0].ToString();
            lbScore2.Text = scoreCurrentTeams[1].ToString();
        }

    }
}
