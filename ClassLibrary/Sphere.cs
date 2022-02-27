using System;

namespace WpfApp.Model
{
    /// <summary>
    /// Класс описывающий сферу жизни
    /// </summary>
    public class Sphere
    {
        /// <summary>
        /// Сообщение при завершении игры
        /// </summary>
        private readonly string message;
        /// <summary>
        /// Счётчик условных единиц
        /// </summary>
        private int _Count = 10;
        public int Count
        {
            get => _Count;
            set
            {
                _Count = value;
                UpdateLevel();
            }
        }
        /// <summary>
        /// Достигнут ли максимальный уровень
        /// </summary>
        public bool IsLevelMax => Level == Level.Unnattainable;
        /// <summary>
        /// Условный уровень
        /// </summary>
        public Level Level { get; set; }
        /// <summary>
        /// Повышение уровня характеристики
        /// </summary>
        private void UpdateLevel()
        {
            switch (Count)
            {
                case <= 5:
                    {
                        LevelMax?.Invoke(message);
                        break;
                    }
                case <= 30:
                    {
                        Level = Level.None;
                        break;
                    }
                case <= 50:
                    {
                        Level = Level.Low;
                        break;
                    }
                case <= 100:
                    {
                        Level = Level.Medium;
                        break;
                    }
                case <= 200:
                    {
                        Level = Level.High;
                        break;
                    }
                case <= 255:
                    {
                        Level = Level.Unnattainable;
                        break;
                    }
                case > 255:
                    {
                        LevelMax?.Invoke("Поздравляем! Вы привели свой город в прекрасное будущее.");
                        break;
                    }
                default:
                    throw new ArgumentException("Выход за пределы значений");
            }
        }
        /// <summary>
        /// Перевод в строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Level switch
            {
                Level.None => "Потрясающе низкий",
                Level.Low => "Низкий",
                Level.Medium => "Средний",
                Level.High => "Высокий",
                Level.Unnattainable => "Превосходный",
                _ => null,
            };
        }
        #region Constructor
        /// <summary>
        /// Создание счётчика с выводом сообщения при проигрыше
        /// </summary>
        /// <param name="message">Сообщение при проигрыше по причине этого события</param>
        public Sphere(string message)
        {
            this.message = message;
        }
        #endregion

        #region events
        public delegate void Game(string txt);
        public event Game LevelMax;

        #endregion
    }
}

