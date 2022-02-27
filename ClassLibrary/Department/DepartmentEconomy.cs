namespace Model
{
    /// <summary>
    /// Класс, описывающий министерство экономики
    /// </summary>
    class DepartmentEconomy : Receiver
    {
        public override void ItIsEvent(object sender, MyEventsArgs e)
        {
            var t =  (Town)sender;
            t.UnitCount += t.DynamicUnit;
            t.DynamicUnit += 5;
            t.LifeObj.Count += 3;
            t.EducationObj.Count -= 3;
            t.EconomyObj.Count += 4;
            e.Result = "Проведена реформа экономики.\n";
        }
    }
}
