﻿namespace Model
{
    /// <summary>
    /// Класс описывающий полицию
    /// </summary>
    class Police : Receiver
    {
        public override void ItIsEvent(object sender, MyEventsArgs e)
        {
            const string OK = "Полиция нейтрализовала преступников. Город стал безопаснее.\n",
                        Bad = "Несмотря на старания полиция не смогла найти преступников. Уровень безопасности упал.\n";
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
