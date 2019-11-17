using System;
using System.Collections.Generic;
using System.Linq;

namespace Example__05.Bridge
{
    public class LogEntry
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }

    public abstract class LoggerBase
    {
        public void Log(string text)
        {
            var entries = Parse(text);
            foreach (var logEntry in entries)
            {
                Write(logEntry);
            }
        }

        protected abstract IEnumerable<LogEntry> Parse(string text);
        protected abstract void Write(LogEntry entry);
    }

    public abstract class FileLogger : LoggerBase
    {
        private string _fileName;

        protected FileLogger(string fileName)
        {
            _fileName = fileName;
        }

        protected override void Write(LogEntry entry)
        {
            //TODO записть лог в файл
        }
    }

    public class SimpleFileLogger : FileLogger
    {
        public SimpleFileLogger(string fileName) : base(fileName) { }

        protected override IEnumerable<LogEntry> Parse(string text)
        {
            return text.Split('\n').Select(x => new LogEntry
            {
                Date = DateTime.Now,
                Message = x
            });
        }
    }

    public class MultiLineFileLogger : FileLogger
    {
        public MultiLineFileLogger(string fileName, string delimTemplate) : base(fileName) { }

        protected override IEnumerable<LogEntry> Parse(string text)
        {
            //TODO разбить текст по шаблону
            return Enumerable.Empty<LogEntry>();
        }
    }

    public abstract class ConsoleLogger : LoggerBase
    {
        protected override void Write(LogEntry entry)
        {
            Console.WriteLine($"{entry.Date.ToShortDateString()} : {entry.Message}");
        }
    }

    public class SimpleConsoleLogger : ConsoleLogger
    {
        protected override IEnumerable<LogEntry> Parse(string text)
        {
            return text.Split('\n').Select(x => new LogEntry
            {
                Date = DateTime.Now,
                Message = x
            });
        }
    }

    public class MultiLineConsoleLogger : ConsoleLogger
    {
        protected override IEnumerable<LogEntry> Parse(string text)
        {
            //TODO разбить текст по шаблону
            return Enumerable.Empty<LogEntry>();
        }
    }
}
 