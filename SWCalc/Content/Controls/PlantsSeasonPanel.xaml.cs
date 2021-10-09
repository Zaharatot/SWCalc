using SWCalc.Content.Clases.DataClases;
using SWCalc.Content.Clases.DataClases.Plant;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWCalc.Content.Controls
{
    /// <summary>
    /// Логика взаимодействия для PlantsSeasonPanel.xaml
    /// </summary>
    public partial class PlantsSeasonPanel : UserControl
    {
        /// <summary>
        /// Строка названия сезона
        /// </summary>
        public string SeasonName
        {
            get => SeasonNameTextBlock.Text;
            set => SeasonNameTextBlock.Text = value;
        }
        /// <summary>
        /// Цвет обводки сезона
        /// </summary>
        public Brush SeasonForeground
        {
            get => MainBorder.BorderBrush;
            set => MainBorder.BorderBrush = value;
        }
        /// <summary>
        /// Цвет заднего плана сезона
        /// </summary>
        public Brush SeasonBlackground
        {
            get => MainBorder.Background;
            set => MainBorder.Background = value;
        }
       
        /// <summary>
        /// Флаг скрытия контента
        /// </summary>
        private bool _isCollapsed;

        /// <summary>
        /// Конструктор контролла
        /// </summary>
        public PlantsSeasonPanel()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Инициализаатор контролла
        /// </summary>
        private void Init()
        {
            //Проставляем дефолтные значения
            _isCollapsed = true;
        }

        /// <summary>
        /// Обработчик событяи клика по заголовку
        /// </summary>
        private void HeaderBorder_MouseDown(object sender, MouseButtonEventArgs e) =>
            //Обновляем статус отображения дочерних панелей
            UpdateCollapesdStatus();

        /// <summary>
        /// Обработчик событяи наведения курсора на заголовок
        /// </summary>
        private void HeaderBorder_MouseEnter(object sender, MouseEventArgs e) =>
            //Просто меняем прозрачность заголвока
            HeaderBorder.Opacity = 0.6;

        /// <summary>
        /// Обработчик событяи ухода курсора с заголовка
        /// </summary>
        private void HeaderBorder_MouseLeave(object sender, MouseEventArgs e) =>
            //Просто меняем прозрачность заголвока
            HeaderBorder.Opacity = 1;


        /// <summary>
        /// Обновляем статус отображения дочерних панелей
        /// </summary>
        private void UpdateCollapesdStatus()
        {
            //Инвертируем значение сттауса
            _isCollapsed = !_isCollapsed;
            //Отображаем панели по статусу
            PlantsPanel.Visibility = (_isCollapsed) ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Создаём панель информации о растении
        /// </summary>
        /// <param name="info">Информация о растении</param>
        /// <returns>Созданная панель</returns>
        private PlantInfo CreatePlantPanel(PlaantCalculatedInfo info)
        {
            //Инициализируем контролл
            PlantInfo elem = new PlantInfo();
            //Проставляем в контролл загруженную инфу
            elem.SetPlantLoadedInfo(info.GetLoadedInfo());
            //Инициализируем и проставляем в контролл вычисленную инфу
            elem.SetPlantCalculatedInfo(info);
            //Возвращаем контролл
            return elem;
        }

        /// <summary>
        /// Получаем список рассчитанной информации о растениях
        /// </summary>
        /// <param name="plants">Список информации о растениях</param>
        /// <returns>Список рассчитанной информации о растениях</returns>
        private List<PlaantCalculatedInfo> GetCalculatedInfoList(List<PlantLoadedInfo> plants) =>
            //Конвертируем список загруженной информации в список рассчитанной
            plants.ConvertAll(plant => new PlaantCalculatedInfo(plant));

        /// <summary>
        /// Обновляем информацию о растениях
        /// </summary>
        public void UpdatePlantInfo()
        {
            //Проходимся по панелям
            foreach (PlantInfo panel in PlantsPanel.Children)
                //Для каждой из них вызываем обновление инфы
                panel.UpdateCalculatedInfo();
        }


        /// <summary>
        /// Вставляем в контролл список растений
        /// </summary>
        /// <param name="plants">Список информации о растениях</param>
        public void SetPlantList(List<PlantLoadedInfo> plants)
        {
            //Получаем список рассчитанной информации о растениях
            List<PlaantCalculatedInfo> calculatedInfo = GetCalculatedInfoList(plants);
            //Сортируем список по максимальнйо цене
            calculatedInfo = calculatedInfo.OrderByDescending(info => info.GetMaxPrice()).ToList();
            //Удаляем все дочерние контроллы
            PlantsPanel.Children.Clear();
            //Проходимся по списку растений
            foreach (var plant in calculatedInfo)
                //Создаём и доабвляем на панель информацию о растении
                PlantsPanel.Children.Add(CreatePlantPanel(plant));
        }
    }
}
