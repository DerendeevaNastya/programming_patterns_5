using System;
using System.Collections.Generic;
using System.Linq;

namespace Example__05.Flyweight
{
    public static class WindowsFactory
    {
        private static readonly List<WindowConfig> Cache = new List<WindowConfig>();

        public static Windows GetWindows(string title, string text)
        {
            if (Cache.All(x => x.Title != title))
            {
                Cache.Add(new WindowConfig(title));
            }

            var result = Cache.First(x => x.Title == title);
            return result.Title.Equals("WARNING")
                ? (Windows) new WarningWindows(text) {Config = result}
                : (Windows) new SavedPositionWindows(text) {Config = result};
        }
    }

    public class WindowConfig
    {
        public WindowConfig(string title)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("DEBUG:Вызвался тяжеловесный конструктор");
            Console.ResetColor();
            Title = title;
        }

        public string Title { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }

    public abstract class Windows
    {
        protected Windows(string text)
        {
            Text = text;
        }

        //Внешние данные
        public string Text { get; set; }

        //Внутренние данные
        public WindowConfig Config { get; set; }


        public abstract void Show();
    }

    public class SavedPositionWindows : Windows
    {
        public SavedPositionWindows(string text) : base(text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("DEBUG:Создалось окно с запомнеными координатами");
            Console.ResetColor();
        }

        public override void Show()
        {
            Console.WriteLine($"Тут показалось окно с заголовком {Config.Title}, c текущими координатами");
            Console.WriteLine(new string('=', 10));
            Console.WriteLine(Text);
            Console.WriteLine();
        }
    }

    public class WarningWindows : Windows
    {
        public WarningWindows(string text) : base(text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Создалось окно с предупреждением");
            Console.ResetColor();
        }

        private const int DefaultX = 400;
        private const int DefaultY = 400;

        public override void Show()
        {
            Config.PositionX = DefaultX;
            Config.PositionY = DefaultY;
            Console.WriteLine($"Тут показалось предупреждение");
            Console.WriteLine(new string('=', 10));
            Console.WriteLine(Text);
        }
    }
}