using PlusOnPlus.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PlusOnPlus.src
{
    enum FormInfo
    {
        Square,
        Circle,
        Triangle
    }
    enum Operation
    {
        Plus,
        Minus
    }

    class Figure
    {
        public Color FColor { get; set; }
        public FormInfo FigureForms { get; set; }

        public Figure(Color fColor, FormInfo figureForms)
        {
            FColor = fColor;
            FigureForms = figureForms;
        }
        public override string ToString()
        {
            switch (FigureForms)
            {
                default: return "⬛";
                case FormInfo.Circle: return "⚫";
                case FormInfo.Triangle: return "🔺";
            }
        }
        public static FormInfo StringToForm(string input)
        {
            switch (input)
            {
                default: return FormInfo.Square;
                case "⚫": return FormInfo.Circle;
                case "🔺": return FormInfo.Triangle;
            }
        }
    }
    class TaskInfo
    {
        public List<Figure> Figures { get; private set; }
        public List<Figure> UserAnsFigures { get; set; } = new List<Figure>();

        public List<Figure> MinusPart { get; set; } = new List<Figure>();
        public List<Figure> MinusAnswer { get; set; } = new List<Figure>();
        public Operation OperationInfo { get; set; }

        public TaskInfo(List<Figure> figures, Operation op)
        {
            Figures = figures;
            OperationInfo = op;
            if (OperationInfo == Operation.Minus)
            {
                for (int i = 0; i < Figures.Count; i++)
                {
                    if (i >= (Figures.Count - 1) / 2) MinusPart.Add(Figures[i]);
                    else MinusAnswer.Add(Figures[i]);
                }
            }
            else
            {
                Thread.Sleep(1);
                if (new Random().Next(0, 6) == 1) FillHalfAnswer();
            }
            Shuffle(Figures);
        }
        public TaskInfo(Difficulty dif)
        {
            Figures = TaskGenerator.GenerateTask(dif);
        }
        public void PrintQuestion(RichTextBox box)
        {
            for (int i = 0; i < Figures.Count; i++)
            {
                box.TextAppendColorized(Figures[i].ToString(), Figures[i].FColor);
                if (OperationInfo != Operation.Minus && i != Figures.Count - 1) box.TextAppendColorized(" + ", Color.Black);
            }
            if (MinusPart.Count != 0)
            {
                box.TextAppendColorized(" - ", Color.Black);
                MinusPart.ForEach(x => box.TextAppendColorized(x.ToString(), x.FColor));
            }
        }
        public void PrintAnswer(RichTextBox box)
        {
            for (int i = 0; i < UserAnsFigures.Count; i++)
            {
                box.TextAppendColorized(UserAnsFigures[i].ToString(), UserAnsFigures[i].FColor);
            }
        }
        public bool IsRightAnswer()
        {
            if (UserAnsFigures.Count == 0) return false;
            if (OperationInfo == Operation.Plus)
            {
                if (UserAnsFigures.Count != Figures.Count) return false;
                foreach (var i in UserAnsFigures)
                {
                    if (!IsHave(Figures, i)) return false;
                }
                return true;
            }
            else
            {
                if (UserAnsFigures.Count != (Figures.Count - 1) / 2) return false;
                foreach (var i in UserAnsFigures)
                {
                    if (!IsHave(MinusAnswer, i)) return false;
                }
                return true;
            }
        }
        private void FillHalfAnswer()
        {
            for (int i = 0; i < Figures.Count; i++)
            {
                if (i >= (Figures.Count - 1) / 2) UserAnsFigures.Add(Figures[i]);
            }
        }
        private static bool IsHave(List<Figure> Figures, Figure fig2)
        {
            foreach (var fig1 in Figures)
            {
                if (fig1.FColor == fig2.FColor && fig1.FigureForms == fig2.FigureForms) return true;
            }
            return false;
        }
        private static bool Compare(Figure fig1, Figure fig2)
        {
            if (fig1.FColor == fig2.FColor && fig1.FigureForms == fig2.FigureForms) return true;
            return false;
        }
        public static void Shuffle(List<Figure> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Figure value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}