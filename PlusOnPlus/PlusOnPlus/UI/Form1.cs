using PlusOnPlus.UI;
using System;
using System.Windows.Forms;

namespace PlusOnPlus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            new TaskForm(src.Difficulty.Easy).ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            new TaskForm(src.Difficulty.Normal).ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            new TaskForm(src.Difficulty.Hard).ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}