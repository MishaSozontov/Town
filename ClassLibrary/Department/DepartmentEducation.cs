namespace Model
{
    /// <summary>
    /// Класс описывающий министерство образования
    /// </summary>
    class DepartmentEducation : Receiver
    {
        public override void ItIsEvent(object sender, MyEventsArgs e)
        {
            var t = (Town)sender;
            t.UnitCount += t.DynamicUnit;
            t.DynamicUnit -= 3;
            t.SecurityObj.Count++;
            t.LifeObj.Count++;
            t.EducationObj.Count += 4;
            t.EconomyObj.Count--;
            e.Result = "Проведена реформа образования.\n";
        }
    }
}
