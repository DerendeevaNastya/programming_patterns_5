using System;
using System.Collections.Generic;
using System.Linq;

namespace Example__05.Bridge.After
{
    public class LogEntry
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }

    public interface ILogParser
    {
        IEnumerable<LogEntry> Parse(string text);
    }

    public abstract class LoggerBase
    {
        protected ILogParser Parser;

        protected LoggerBase(ILogParser parser)
        {
            Parser = parser;
        }

        public void Log(string text)
        {
            var entries = Parser.Parse(text);
            foreach (var logEntry in entries)
            {
                Write(logEntry);
            }
        }

        protected abstract void Write(LogEntry entry);
    }

    public class FileLogger : LoggerBase
    {
        private string _fileName;

        protected FileLogger(ILogParser parser, string fileName) : base(parser)
        {
            _fileName = fileName;
        }

        protected override void Write(LogEntry entry)
        {
            //TODO записть лог в файл
        }
    }

    public class ConsoleLogger : LoggerBase
    {
        public ConsoleLogger(ILogParser parser) : base(parser)
        { }

        protected override void Write(LogEntry entry)
        {
            Console.WriteLine($"{entry.Date.ToShortDateString()} : {entry.Message}");
        }
    }

    public class SimpleLogParser : ILogParser
    {
        public IEnumerable<LogEntry> Parse(string text)
        {
            return text.Split('\n').Select(x => new LogEntry
            {
                Date = DateTime.Now,
                Message = x
            });
        }
    }

    public class MultiLineLogParser : ILogParser
    {
        public IEnumerable<LogEntry> Parse(string text)
        {
            //TODO разбить текст по шаблону
            return Enumerable.Empty<LogEntry>();
        }
    }
}