namespace Model.Service
{
    /// <summary>
    /// Класс описывающий банк
    /// </summary>
    class Bank : Receiver
    {
        public override void ItIsEvent(object sender, MyEventsArgs e)
        {
            const string OK = "Банк сумел предотвратить инфляцию.\n",
            Bad = "Банк выпустил много валюты. Уровень экономики устремился вниз.\n";

            Town t = (Town)sender;
            if (ClassHandler.DoEvent(ref t.EconomyObj, e.Counter))
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
