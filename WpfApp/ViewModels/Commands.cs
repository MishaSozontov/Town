using Model;
using Prism.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfApp.ViewModels
{
    public class Commands : INotifyPropertyChanged
    {
        #region Variables
        /// <summary>
        /// Новости
        /// </summary>
        private string _News;
        public string News
        {
            get => _News;
            set => SetProperty(ref _News, value);
        }
        /// <summary>
        /// Наименование города
        /// </summary>
        private string _NameTown = "Обычный город";
        public string NameTown
        {
            get => _NameTown;
            set => SetProperty(ref _NameTown, value);
        }
        /// <summary>
        /// Количество жителей и в скобках динамика изменения
        /// </summary>
        private string _UnitCount = "10 (0)";
        public string UnitCount
        {
            get => _UnitCount;
            set => SetProperty(ref _UnitCount, value);
        }
        /// <summary>
        /// Уровень безопасности
        /// </summary>
        private string _Security;
        public string Security
        {
            get => _Security;
            set => SetProperty(ref _Security, value);
        }
        /// <summary>
        /// Уровень жизни
        /// </summary>
        private string _Life;
        public string Life
        {
            get => _Life;
            set => SetProperty(ref _Life, value);
        }
        /// <summary>
        /// Уровень образования
        /// </summary>
        private string _Education;
        public string Education
        {
            get => _Education;
            set => SetProperty(ref _Education, value);
        }
        /// <summary>
        /// Уровень 
        /// </summary>
        private string _Economy;
        public string Economy
        {
            get => _Economy;
            set => SetProperty(ref _Economy, value);
        }
        private bool IsNotNull => town != null;
        /// <summary>
        /// Город
        /// </summary>
        private Town town;
        #endregion

        #region Constructor
        public Commands()
        {
            EducationDelegateCommand = new DelegateCommand(EducationReform);
            StartGameDelegateCommand = new DelegateCommand(StartGame);
            EconomyDelegateCommand = new DelegateCommand(EconomyReform);
            FertilityDelegateCommand = new DelegateCommand(FertilityReform);
            ExtraDelegateCommand = new DelegateCommand(ExtraReform);
        }
        #endregion

        #region DelegateCommandMethods
        /// <summary>
        /// Новая игра
        /// </summary>
        private void StartGame()
        {
            town = new Town()/* { NameTown = "Город N" }*/;
            
            News = "Игра в городе N началась. Удачи!\n";
            town.OnGameEnd += GameEnd;
            //town.OnMessage += Message;
            UpdateProperty();
        }

        /// <summary>
        /// Реформа образования
        /// </summary>
        private void EducationReform()
        {
            if (IsNotNull) DoAction(new Action(town.DoEducationReform));
        }

        /// <summary>
        /// Реформа рождаемости
        /// </summary>
        private void FertilityReform()
        {
            if (IsNotNull) DoAction(new Action(town.DoFertilityReform));
        }
        /// <summary>
        /// Реформа экономики
        /// </summary>
        private void EconomyReform()
        {
            if (IsNotNull) DoAction(new Action(town.DoEconomyReform));
        }
        /// <summary>
        /// Реформа экстренных служб
        /// </summary>
        private void ExtraReform()
        {
            if (IsNotNull) DoAction(new Action(town.DoExtraReform));
        }
        /// <summary>
        /// Делегат метода действия
        /// </summary>
        private delegate void Action();
        /// <summary>
        /// Если есть город выполняет действие
        /// </summary>
        /// <param name="test"></param>
        private void DoAction(Action a)
        {
            a.Invoke();
            if (IsNotNull) UpdateProperty();
        }

        /// <summary>
        /// Обновление всех свойств
        /// </summary>
        public void UpdateProperty()
        {
            NameTown = town.NameTown;
            UnitCount = town.UnitCount + " (" + town.DynamicUnit + ")";
            Security = town.Security;
            Life = town.Life;
            Education = town.Education;
            Economy = town.Economy;
            News += town.History;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Обработчик события конца игры
        /// </summary>
        private void GameEnd(object o = null, MyEventsArgs e = null)
        {
            if (IsNotNull)
            {
                UnitCount = "0";
                News += town.History;
                town = null;

                _ = MessageBox.Show("Игра окончена");
            }
        }
        #endregion 

        #region Delegate Commands
        public DelegateCommand EducationDelegateCommand { get; set; }
        public DelegateCommand ExtraDelegateCommand { get; set; }
        public DelegateCommand EconomyDelegateCommand { get; set; }
        public DelegateCommand FertilityDelegateCommand { get; set; }
        public DelegateCommand StartGameDelegateCommand { get; set; }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Установка значения
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">Поле свойства</param>
        /// <param name="newValue">Значение</param>
        protected void SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}

