using SWCalc.Content.Clases.DataClases;
using SWCalc.Content.Clases.DataClases.Plant;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SWCalc.Content.Clases.DataClases.Global.Enums;

namespace SWCalc.Content.Controls
{
    /// <summary>
    /// Логика взаимодействия для PlantInfo.xaml
    /// </summary>
    public partial class PlantInfo : UserControl
    {
        /// <summary>
        /// Класс рассчитанной информации о растении
        /// </summary>
        private PlaantCalculatedInfo _calculatedInfo;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public PlantInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загружаем картинку по строке пути
        /// </summary>
        /// <param name="path">Путь к файлу картинки на диске</param>
        /// <returns>Класс картинки</returns>
        private BitmapImage LoadImageByPath(string path)
        {
            BitmapImage ex = null;
            //Если файл с картинкой реально существует
            if (File.Exists(path))
            {
                ex = new BitmapImage();
                ex.BeginInit();
                //Считываем байты файла в поток в памяти
                ex.StreamSource = new MemoryStream(File.ReadAllBytes(path));
                ex.EndInit();
            }
            return ex;
        }

        /// <summary>
        /// Получаем значение вилимости по флагу
        /// </summary>
        /// <param name="flag">Значение флага</param>
        /// <returns>Значение видимости</returns>
        private Visibility GetVisiblityFromFlag(bool flag) =>
            flag ? Visibility.Visible : Visibility.Collapsed;




        /// <summary>
        /// Обноваляем рассчитанные значения
        /// </summary>
        public void UpdateCalculatedInfo() =>
            SetPlantCalculatedInfo(_calculatedInfo);

        /// <summary>
        /// Проставляем в контролл загружаемую информацию о растении
        /// </summary>
        /// <param name="info">Загруженная информация о растении</param>
        public void SetPlantLoadedInfo(PlantLoadedInfo info)
        {
            //ПРоставляем основные значения
            PlantNameTextBlock.Text = info.Name;
            PlaniIconImage.Source = LoadImageByPath(info.GetIconPath());
            //Проставляем цену на семена
            PriseJojaRun.Text = info.PriceJoja.ToString();
            PrisePiereRun.Text = info.PricePiere.ToString();
            PriseOtherRun.Text = info.PriceOther.ToString();
            //Проставляем информацию о сборе
            HarvestRun.Text = info.SingleHarvest.ToString();
            RipeningTimeRun.Text = info.RipeningTime.ToString();
            AdditionalTimeRun.Text = info.AdditionalTime.ToString();
            //Проставляем инфу о ценах продажи
            PriceOrdinaryRun.Text = info.GetGradePrice(Grades.Ordinary).ToString();
            PriceSilverRun.Text = info.GetGradePrice(Grades.Silver).ToString();
            PriceGoldRun.Text = info.GetGradePrice(Grades.Gold).ToString();
            PriceIridiumRun.Text = info.GetGradePrice(Grades.Iridium).ToString();
            //Обновляем видимость цен
            PriseOtherTextBlock.Visibility = GetVisiblityFromFlag(info.PriceOther != 0);
            PriseJojaTextBlock.Visibility = GetVisiblityFromFlag(info.PriceJoja != 0);
            PrisePiereTextBlock.Visibility = GetVisiblityFromFlag(info.PricePiere != 0);
            //Обновляем видимость доп. времени
            AdditionalTimeTextBlock.Visibility = GetVisiblityFromFlag(info.AdditionalTime != 0);
        }

        /// <summary>
        /// Проставляем рассчитанную информацию о растении
        /// </summary>
        /// <param name="info">Рассчитанная информация о растении</param>
        public void SetPlantCalculatedInfo(PlaantCalculatedInfo info)
        {
            //Запоминаем переданное знавчение
            _calculatedInfo = info;
            //Проставляем общий сбор
            HarvestCountRun.Text = info.HarvestCount.ToString();
            //Проставляем инфу о ценах продажи
            SeasonPriceOrdinaryRun.Text = info.GetGradePrice(Grades.Ordinary).ToString();
            SeasonPriceSilverRun.Text = info.GetGradePrice(Grades.Silver).ToString();
            SeasonPriceGoldRun.Text = info.GetGradePrice(Grades.Gold).ToString();
            SeasonPriceIridiumRun.Text = info.GetGradePrice(Grades.Iridium).ToString();
            //Проставляем цены за производные
            SeasonPriceJuiceRun.Text = info.JuicePrice.ToString();
            SeasonPriceJamRun.Text = info.JamPrice.ToString();
            SeasonPricePicklesRun.Text = info.PiclkesPrice.ToString();
            SeasonPriceVineRun.Text = info.VinePrice.ToString();
            //Проставляем видимость контроллам с производными
            //Сок и вино
            JuiceTextBlock.Visibility = GetVisiblityFromFlag(info.IsAllowToJuice);
            VineTextBlock.Visibility = GetVisiblityFromFlag(!info.IsAllowToJuice);
            //Джем и консервы
            PicklesTextBlock.Visibility = GetVisiblityFromFlag(info.IsAllowToPickles);
            JamTextBlock.Visibility = GetVisiblityFromFlag(!info.IsAllowToPickles);
            //Скрываем панель обработки, если растение нельзя переработать
            ProducePanel.Visibility = GetVisiblityFromFlag(!info.IsNotProcessed);
        }
    }
}
