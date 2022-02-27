using System;
using WpfApp.Model;

namespace Model
{
    /// <summary>
    /// Класс обрабатывающий случайное событие
    /// </summary>
    static class ClassHandler
    {
        /// <summary>
        /// Проверяет вероятность возникновения события
        /// Если событие возможно выполняет его
        /// </summary>
        /// <param name="field">Сфера жизни события</param>
        /// <returns>true - событие произошло, false - событие не произошло </returns>
        public static bool DoEvent(ref Sphere field, int Counter)
        {
            if (CheckEvent(Counter))
            {
                field.Count -= field.Count / 20;
                return true;
            }
            field.Count++;
            return false;
        }
        /// <summary>
        /// Проверяет вероятность возникновения события
        /// </summary>
        /// <param name="parametr">Параметр проверки</param>
        /// <returns>true-событие произойдёт, false-если нет</returns>
        private static bool CheckEvent(int parametr)
        {
            Random random = new();
            int prop = parametr - random.Next(0, 101);
            return prop < 0;
        }
    }
}
