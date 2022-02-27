using Model.Service;
using System;
using WpfApp.Model;

namespace Model
{
    public delegate void MyEventHandler(object sender, MyEventsArgs e);

    public class Town
    {
        #region Constante
        /// <summary>
        /// Сообщение при проигрыше из-за снижения безопасности
        /// </summary>
        private const string fail_security = "Террористы взорвали бомбу в центре города. Все жители погибли.";
        /// <summary>
        /// Сообщение при проигрыше из-за снижения уровня жизни
        /// </summary>
        private const string fail_life = "Уровень безысходности достиг предела. Все жители погибли.";
        /// <summary>
        /// Сообщение при проигрыше из-за снижения уровня образования
        /// </summary>
        private const string fail_education = "Жители разучились добывать еду. Город вымер.";

        /// <summary>
        /// Сообщение при проигрыше из-за снижения уровня экономики
        /// </summary>
        private const string fail_economy = "Из-за инфляции деньги обесценились. Город наводнили грабители. Всё население было убито";
        #endregion

        #region Property

        /// <summary>
        /// История событий за день в городе
        /// </summary>
        public string History { get; set; }

        /// <summary>
        /// Количество дней
        /// </summary>
        public int Day { get; set; } = 1;

        /// <summary>
        /// Наименование города
        /// </summary>
        public string NameTown { get; set; } = "Город N";

        /// <summary>
        /// Количество жителей в городе
        /// </summary>
        public int UnitCount
        {
            get => _UnitCount;
            set => SetUnit(value);
        }

        /// <summary>
        /// Динамика изменения количества жителей
        /// (на сколько измениться количество жителей в следующий ход)
        /// </summary>
        public int DynamicUnit { get; set; } = 1;

        /// <summary>
        /// Уровень безопасности
        /// </summary>
        public string Security => SecurityObj.ToString();

        /// <summary>
        /// Уровень жизни
        /// </summary>
        public string Life => LifeObj.ToString();

        /// <summary>
        /// Уровень образования
        /// </summary>
        public string Education => EducationObj.ToString();

        /// <summary>
        /// Уровень экономики
        /// </summary>
        public string Economy => EconomyObj.ToString();

        #endregion

        #region Variables
        /// <summary>
        /// Количество жителей
        /// </summary>
        private int _UnitCount = 10;

        /// <summary>
        /// Установка значения количества жителей
        /// При уменьшении жителей до 0
        /// Вызывает событие окончания игры.
        /// </summary>
        /// <param name="value">Новое количество жителей</param>
        private void SetUnit(int value)
        {
            if (value <= 0)
            {
                History = "Все жители умерли.\n";
                OnGameEnd?.Invoke(null, null);
            }
            _UnitCount = value;
        }

        /// <summary>
        /// Счётчик уровеня безопасности
        /// </summary>
        public Sphere SecurityObj = new(fail_security);


        /// <summary>
        /// Счётчик уровеня жизни
        /// </summary>
        public Sphere LifeObj = new(fail_life);


        /// <summary>
        /// Счётчик уровня образования
        /// </summary>
        public Sphere EducationObj = new(fail_education);

        /// <summary>
        /// Счётчик уровня экономики
        /// </summary>
        public Sphere EconomyObj = new(fail_economy);

        /// <summary>
        /// Департамент экстренных служб
        /// </summary>
        private readonly DepartmentService emergencyService;

        /// <summary>
        /// Департамент демографии
        /// </summary>
        private readonly DepartmentDemographics depDemographics;

        /// <summary>
        /// Департамент экономики
        /// </summary>
        private readonly DepartmentEconomy depEconomy;

        /// <summary>
        /// Департамент образования
        /// </summary>
        private readonly DepartmentEducation depEducation;

        /// <summary>
        /// Медики
        /// </summary>
        private readonly Ambulance ambulance;

        /// <summary>
        /// Пожарные
        /// </summary>
        private readonly Fireman fireman;

        /// <summary>
        /// Полиция
        /// </summary>
        private readonly Police police;

        /// <summary>
        /// Учебные учреждения
        /// </summary>
        private readonly School school;

        /// <summary>
        /// Банк
        /// </summary>
        private readonly Bank bank;
        #endregion

        #region Constraction
        /// <summary>
        /// Создание нового города
        /// </summary>
        public Town()
        {
            //подписка депортаментов на события
            Subscription(ref emergencyService, ref ReformExtra);
            Subscription(ref depDemographics, ref ReformFertility);
            Subscription(ref depEconomy, ref ReformEconomy);
            Subscription(ref depEducation, ref ReformEducation);
            //подписка служб на события
            //Пожар
            Subscription(ref fireman, ref Fire);
            Subscription(ref ambulance, ref Fire);
            Subscription(ref police, ref Fire);
            //преступность
            Subscription(ref police, ref Mafia);
            Subscription(ref ambulance, ref Mafia);


            Subscription(ref school, ref DegradationEducation);

            Subscription(ref bank, ref Inflation);

            Subscription(ref ambulance, ref Epidemic);
            

            //подписка сфер на событие при достижении максимального уровня
            SecurityObj.LevelMax += MaxLevelField;
            LifeObj.LevelMax += MaxLevelField;
            EconomyObj.LevelMax += MaxLevelField;
            EducationObj.LevelMax += MaxLevelField;
        }

        /// <summary>
        /// Подписка объекта на событие
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">объект</param>
        /// <param name="handler">событие</param>
        /// <returns>Объект подписанный на событие</returns>
        private static void Subscription<T>(ref T obj, ref MyEventHandler handler) where T : Receiver, new()
        {
            obj = new T();
            obj.On(ref handler);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Реформа образования
        /// </summary>
        public void DoEducationReform()
        {
            OnEvent(ReformEducation, -1);
        }

        /// <summary>
        /// Реформа рождаемости
        /// </summary>
        public void DoFertilityReform()
        {
            OnEvent(ReformFertility, -1);
        }

        /// <summary>
        /// Реформа экономики
        /// </summary>
        public void DoEconomyReform()
        {
            OnEvent(ReformEconomy, -1);
        }

        /// <summary>
        /// Реформа экстренных служб
        /// </summary>
        public void DoExtraReform()
        {
            OnEvent(ReformExtra, -1);
        }

        /// <summary>
        /// Выполняет вызов основного и случайного события
        /// </summary>
        /// <param name="handler">Основное событие</param>
        private void OnEvent(MyEventHandler handler, int counter)
        {
            History = $"День {Day++}:\n";
            OnAction(handler, counter);
            OnRandomEvent();
        }

        /// <summary>
        /// Выполняет вызов события
        /// </summary>
        /// <param name="handler">Событие которое вызываем</param>
        private void OnAction(MyEventHandler handler, int Counter)
        {
            
            if (handler != null)
            {
                Delegate[] delegates = handler.GetInvocationList();
                string[] res = new string[delegates.Length];
                foreach (MyEventHandler h in delegates)
                {
                    MyEventsArgs e = new(Counter);
                    h(this, e);
                    History += e.Result;
                }
            }
        }

        /// <summary>
        /// Метод вызова случайного события
        /// </summary>
        private void OnRandomEvent()
        {
            Random random = new();
            int numberEvent = random.Next(0, 10);
            if (numberEvent == 0)
                OnAction(Epidemic, SecurityObj.Count); //Эпидемия
            else if (numberEvent == 1)
                OnAction(Mafia, SecurityObj.Count);  //Мафия
            else if (numberEvent == 2)
                OnAction(Fire, SecurityObj.Count);   //Пожар
            else if (numberEvent == 3)
                OnAction(Inflation, EconomyObj.Count); //Инфляция
            else if (numberEvent == 4)
                OnAction(DegradationEducation, EducationObj.Count); //Проблемы с образованием
            else
                History += "В городе всё спокойно.\n";
        }

        /// <summary>
        /// Обработчик события при достижении максимального уровня в одной из характеристик
        /// </summary>
        /// <param name="txt"></param>
        private void MaxLevelField(string txt)
        {
            if (SecurityObj.IsLevelMax && EconomyObj.IsLevelMax && LifeObj.IsLevelMax && EducationObj.IsLevelMax)
            {
                History = "Город достиг прекрасного будущего.\n";
                OnGameEnd?.Invoke(null, null);
            }
        }
        #endregion 

        #region Event
        /// <summary>
        /// Событие реформы образования
        /// </summary>
        public event MyEventHandler ReformEducation;

        /// <summary>
        /// Событие реформы рождаемости
        /// </summary>
        public event MyEventHandler ReformFertility;

        /// <summary>
        /// Событие реформы экономики
        /// </summary>
        public event MyEventHandler ReformEconomy;

        /// <summary>
        /// Событие проведения реформы экстренных служб
        /// </summary>
        public event MyEventHandler ReformExtra;

        /// <summary>
        /// Событие возникновения эпидемии
        /// </summary>
        public event MyEventHandler Epidemic; //экстр 

        /// <summary>
        /// Событие возникновения Мафии
        /// </summary>
        public event MyEventHandler Mafia; //экстр

        /// <summary>
        /// Событие возникновения Пожара
        /// </summary>
        public event MyEventHandler Fire; //экстр

        /// <summary>
        /// Событие возникновения инфляции
        /// </summary>
        public event MyEventHandler Inflation; //эконом

        /// <summary>
        /// Событие возникновения уменьшения уровня образования
        /// </summary>
        public event MyEventHandler DegradationEducation; //обр

        /// <summary>
        /// Событие конца игры
        /// </summary>
        public event MyEventHandler OnGameEnd;
        #endregion 
    }
}
