namespace Model
{
    /// <summary>
    /// Класс экстренных служб
    /// </summary>
    class DepartmentService : Receiver
    {
        public DepartmentService(){}

        public override void ItIsEvent(object sender, MyEventsArgs e)
        {
            var t = (Town)sender;
            t.UnitCount += t.DynamicUnit;
            t.SecurityObj.Count += 3;
            t.LifeObj.Count--;
            t.EducationObj.Count--;
            t.EconomyObj.Count -= 2;
            
            e.Result = "Проведена реформа экстренных служб. Безопасность усилилась.\n";
        }
    }
}
