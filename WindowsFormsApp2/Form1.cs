using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random random = new Random();
        const double k = 0.1, // k = t(i+1) - t(i)
            drift = 0, volatility = 0.025;
        double rate, rate2, rand, rand2, norm_var, norm_var2, w = 0, w2 = 0;
        int i = 1;

        private void button1_Click(object sender, EventArgs e)
        {
            rate = (double)numericUpDown2.Value;
            rate2 = (double)numericUpDown3.Value;

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            i = 1;

            chart1.Series[0].Points.AddXY(0, rate);
            chart1.Series[1].Points.AddXY(0, rate2);

            timer1.Start();

            button1.Text = "Начать заново";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            rand = random.NextDouble();
            rand2 = random.NextDouble();

            norm_var = Math.Cos(2 * Math.PI * rand) * Math.Sqrt((-2) * Math.Log(rand2));
            norm_var2 = Math.Sin(2 * Math.PI * rand) * Math.Sqrt((-2) * Math.Log(rand2));

            w = Math.Sqrt(k) * norm_var;
            rate = rate * Math.Exp((drift - ((volatility * volatility) / 2)) * k + (volatility * w));

            w2 = Math.Sqrt(k) * norm_var2;
            rate2 = rate2 * Math.Exp((drift - ((volatility * volatility) / 2)) * k + (volatility * w2));

            chart1.Series[0].Points.AddXY(i, rate);
            chart1.Series[1].Points.AddXY(i, rate2);

            i++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}
