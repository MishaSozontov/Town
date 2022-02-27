namespace Model.Service
{
    /// <summary>
    /// Класс описывающий учебные учреждения
    /// </summary>
    class School : Receiver
    {
        public override void ItIsEvent(object sender, MyEventsArgs e)
        {
            const string OK = "Благодаря усилиям учителей уровень образования повысился.\n",
                        Bad = "Несмотря на все усилия учителей, уровень образования упал\n";

            Town t = (Town)sender;
            if (ClassHandler.DoEvent(ref t.EducationObj, e.Counter))
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
