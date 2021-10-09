using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCalc.Content.Clases.DataClases.Plant
{
    /// <summary>
    /// Класс данных о растениях
    /// </summary>
    public class PlantsData
    {
        /// <summary>
        /// Список весенних растений
        /// </summary>
        public List<PlantLoadedInfo> Spring { get; set; }
        /// <summary>
        /// Список летних растений
        /// </summary>
        public List<PlantLoadedInfo> Summer { get; set; }
        /// <summary>
        /// Список осенних растений
        /// </summary>
        public List<PlantLoadedInfo> Autumn { get; set; }
        /// <summary>
        /// Список растений не вошедших в сезоны
        /// </summary>
        public List<PlantLoadedInfo> Other { get; set; }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public PlantsData()
        {
            //Инициализируем дефолтные значения
            Spring = new List<PlantLoadedInfo>();
            Summer = new List<PlantLoadedInfo>();
            Autumn = new List<PlantLoadedInfo>();
            Other = new List<PlantLoadedInfo>();
        }
    }
}
