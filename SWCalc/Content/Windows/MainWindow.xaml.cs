using SWCalc.Content.Clases.DataClases;
using SWCalc.Content.Clases.DataClases.Additional;
using SWCalc.Content.Clases.DataClases.Global;
using SWCalc.Content.Clases.DataClases.Plant;
using SWCalc.Content.Clases.WorkClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static SWCalc.Content.Clases.DataClases.Global.Enums;

namespace SWCalc.Content.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Класс загрузки параметров о растениях
        /// </summary>
        private PlantDataLoader _plantDataLoader;

        /// <summary>
        /// Конструктор окна
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Инициализатор окна
        /// </summary>
        private void Init()
        {
            //Проставляем дефолтные значения
            GlobalValues.CountPlants = 20;
            CountPlantsTextBox.Text = "20";
            //Инициализируем используемые классы
            _plantDataLoader = new PlantDataLoader();

            //Загружаем данные в панели
            LoadDataToPanels();
        }

        /// <summary>
        /// Загружаем данные в панели
        /// </summary>
        private void LoadDataToPanels()
        {
            //Грузим данные о растениях
            PlantsData data = _plantDataLoader.LoadContent();
            //Грузим данные о растениях по сезонам в контроллы
            SpringPlantsPanel.SetPlantList(data.Spring);
            SummerPlantsPanel.SetPlantList(data.Summer);
            AutumnPlantsPanel.SetPlantList(data.Autumn);
            OtherPlantsPanel.SetPlantList(data.Other);
        }


        /// <summary>
        /// Обработчик события нажатия на кнопку обновления
        /// </summary>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            //Обновляем инфу на панелях
            SpringPlantsPanel.UpdatePlantInfo();
            SummerPlantsPanel.UpdatePlantInfo();
            AutumnPlantsPanel.UpdatePlantInfo();
            OtherPlantsPanel.UpdatePlantInfo();
        }

        /// <summary>
        /// Обработчик события обновления текста
        /// </summary>
        private void CountPlantsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UpdateButton != null)
            {
                //Если распарсить значение удалось
                if (int.TryParse(CountPlantsTextBox.Text, out int result))
                {
                    //Проставляем новое значение количества растений
                    GlobalValues.CountPlants = result;
                    //Включаем кнопку обновления
                    UpdateButton.IsEnabled = true;
                }
                //В случае ошибки парсинга
                else
                    //Отключаем кнопку обновления
                    UpdateButton.IsEnabled = false;

            }
        }
    }
}
