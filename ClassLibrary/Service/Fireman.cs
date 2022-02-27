namespace Model
{
    /// <summary>
    /// Пожарные
    /// </summary>
    class Fireman : Receiver
    {
        public override void ItIsEvent(object sender, MyEventsArgs e)
        {
            const string OK = "В городе пожар. Благодаря усилиям пожарных он был своевременно потушен.\n",
            Bad = "В городе пожар. Несмотря на все усилия пожарных ТЦ сгорел, уровень жизни понизился.\n";

            Town t = (Town)sender;

            if (ClassHandler.DoEvent(ref t.LifeObj, e.Counter ))
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
