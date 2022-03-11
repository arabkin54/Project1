using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace PlusOnPlus.src
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }
    class TaskGenerator
    {
        private static List<Color> RegColors = new List<Color>() { Color.Red, Color.LightGray, Color.Khaki, Color.Green };
        public static List<TaskInfo> GenerateTaskList(Difficulty difficulty, int count)
        {
            List<TaskInfo> Result = new List<TaskInfo>();
            for (int i = 0; i < count; i++)
            {
                Result.Add(new TaskInfo(GenerateTask(difficulty), (Operation)new Random().Next(0,2)));
                Thread.Sleep(1);
            }
            return Result;
        }
        public static List<Figure> GenerateTask(Difficulty difficulty)
        {
            return GenerateTaskNumerics((int)difficulty + 4);
        }
        private static List<Figure> GenerateTaskNumerics(int countInt)
        {
            List<Figure> Result = new List<Figure>();
            var Rnd = new Random();
            for (int i = 0; i < countInt; i++)
            {
                Result.Add(new Figure(RegColors[Rnd.Next(0, RegColors.Count)], (FormInfo)Rnd.Next(0, 3)));
                Thread.Sleep(1);
            }
            return Result;
        }
    }
}