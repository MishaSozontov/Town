namespace Model
{
    /// <summary>
    /// Класс описывающий врачей
    /// </summary>
    class Ambulance : Receiver
    {
        public override void ItIsEvent(object sender, MyEventsArgs e)
        {
            const string OK = "Медики оказали своевременную помощь пострадавшим.\n",
            Bad = "В городе произошло ЧП. Скорая не смогла приехать на место происшествия. Уровень безопасности снизился.\n";
            Town t = (Town)sender;

            if (ClassHandler.DoEvent(ref t.SecurityObj, e.Counter))
            {
                e.Result += Bad;
            }
            else
            {
                e.Result += OK;
            }
        }
    }
}
