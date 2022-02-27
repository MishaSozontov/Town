namespace Model
{
    /// <summary>
    /// Министерство демографии
    /// </summary>
    class DepartmentDemographics : Receiver
    {
        public override void ItIsEvent(object sender, MyEventsArgs e)
        {
            var t = (Town)sender;
            t.UnitCount += t.DynamicUnit;
            t.DynamicUnit += 2;
            t.SecurityObj.Count -= 2;
            t.LifeObj.Count -= 2;
            t.EducationObj.Count -= 2;
            t.EconomyObj.Count -= 2;
            e.Result += "Проведена реформа рождаемости.\n";
        }
    }
}
