using PlusOnPlus.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PlusOnPlus.UI
{
    public partial class TaskForm : Form
    {
        private List<TaskInfo> Tasks { get; set; }
        private int Time { get; set; }
        private int StartTime;
        private int SelectedIndex { get; set; }
        private Timer UpdateTimer { get; set; }

        public TaskForm(Difficulty difficulty)
        {
            InitializeComponent();
            Tasks = TaskGenerator.GenerateTaskList(difficulty, 11);
            StartTime = 120 / ((int)difficulty + 1);
            Time = StartTime; taskPageLabel.Text = $"{SelectedIndex}/10";
            UpdateTimer = new Timer(); UpdateTimer.Interval = 1000;
            UpdateTimer.Tick += UpdateTimer_Tick; UpdateTimer.Start();
            Tasks[SelectedIndex].PrintQuestion(richTextBox2);
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            Time--;
            timeInfoLabel.Text = $"Осталось времени: {Time}/{StartTime} секунд.";
            if (Time <= 0) { EndTest(); };
        }
        private void Next_Click(object sender, EventArgs e)
        {
            if (SelectedIndex + 1 >= Tasks.Count) return;
            SelectedIndex++; taskPageLabel.Text = $"{SelectedIndex}/10";
            richTextBox2.Text = string.Empty; richTextBox1.Text = string.Empty;
            Tasks[SelectedIndex].PrintQuestion(richTextBox2);
            Tasks[SelectedIndex].PrintAnswer(richTextBox1);
        }
        private void Prev_Click(object sender, EventArgs e)
        {
            if (SelectedIndex - 1 < 0) return;
            SelectedIndex--; taskPageLabel.Text = $"{SelectedIndex}/10";
            richTextBox2.Text = string.Empty; richTextBox1.Text = string.Empty;
            Tasks[SelectedIndex].PrintQuestion(richTextBox2);
            Tasks[SelectedIndex].PrintAnswer(richTextBox1);
        }
        private void EndTest()
        {
            UpdateTimer.Dispose();
            int count = 0;
            foreach (var i in Tasks)
            {
                if (i.IsRightAnswer()) count++;
            }
            new Results(count).ShowDialog();
            this.Dispose();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            EndTest();
        }
        private void LabelToRichTextBox1(Label label)
        {
            Tasks[SelectedIndex].UserAnsFigures.Add(new Figure(label.ForeColor, Figure.StringToForm(label.Text)));
            richTextBox1.Text = string.Empty; Tasks[SelectedIndex].PrintAnswer(richTextBox1);
        }
        private void label1_Click(object sender, EventArgs e)
        {
            LabelToRichTextBox1(label1);
        }
        private void label7_Click(object sender, EventArgs e)
        {
            LabelToRichTextBox1(label7);
        }
        private void label10_Click(object sender, EventArgs e)
        {
            LabelToRichTextBox1(label10);
        }
        private void label2_Click(object sender, EventArgs e)
        {
            LabelToRichTextBox1(label2);
        }
        private void label6_Click(object sender, EventArgs e)
        {
            LabelToRichTextBox1(label6);
        }
        private void label9_Click(object sender, EventArgs e)
        {
            LabelToRichTextBox1(label9);
        }
        private void label4_Click(object sender, EventArgs e)
        {
            LabelToRichTextBox1(label4);
        }
        private void label5_Click(object sender, EventArgs e)
        {
            LabelToRichTextBox1(label5);
        }
        private void label8_Click(object sender, EventArgs e)
        {
            LabelToRichTextBox1(label8);
        }
        private void label13_Click(object sender, EventArgs e)
        {
            LabelToRichTextBox1(label13);
        }
        private void label12_Click(object sender, EventArgs e)
        {
            LabelToRichTextBox1(label12);
        }
        private void label11_Click(object sender, EventArgs e)
        {
            LabelToRichTextBox1(label11);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Tasks[SelectedIndex].UserAnsFigures.Clear();
            richTextBox1.Text = string.Empty;
        }

        private void richTextBox2_SelectionChanged(object sender, EventArgs e)
        {
            richTextBox2.SelectionLength = 0;
        }
    }
    public static class RichTextBoxExtensions
    {
        public static void TextAppendColorized(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}