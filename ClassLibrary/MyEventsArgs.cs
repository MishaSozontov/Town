using System;

namespace Model
{
    /// <summary>
    /// Класс указывающих входные и выходные аргументы события
    /// </summary>
    public class MyEventsArgs : EventArgs
    {
        public MyEventsArgs(int Count)
        {
            this.Counter = Count;
        }
        /// <summary>
        /// Параметр по которому проверяется вероятность события
        /// </summary>
        public readonly int Counter;
        /// <summary>
        /// Результаты события
        /// </summary>
        public string Result { get; set; }
    }
}
