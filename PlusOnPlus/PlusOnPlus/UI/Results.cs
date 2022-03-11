using System;
using System.Windows.Forms;

namespace PlusOnPlus.UI
{
    public partial class Results : Form
    {
        public Results(int res)
        {
            InitializeComponent();
            label1.Text = $"Результат: {res}/10";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}