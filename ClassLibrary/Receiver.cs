namespace Model
{
    /// <summary>
    /// Абстрактный класс "Клиент"
    /// Добавляет и удаляет подписку на события города
    /// </summary>
    abstract class Receiver
    {
        //NewTown town;

        protected Receiver() { }

        /// <summary>
        /// Добавляет подписку на событие
        /// </summary>
        /// <param name="e">Событие</param>
        public void On(ref MyEventHandler e)
        {
            e += new MyEventHandler(ItIsEvent);
        }
        /// <summary>
        /// Удаляет подписку на событие
        /// </summary>
        /// <param name="e">Событие</param>
        public void Off(MyEventHandler e)
        {
            e -= new MyEventHandler(ItIsEvent);
        }
        /// <summary>
        /// Класс обработчик события
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void ItIsEvent(object sender, MyEventsArgs e);
    }
}
