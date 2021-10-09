using SWCalc.Content.Clases.DataClases.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SWCalc.Content.Clases.DataClases.Global.Enums;

namespace SWCalc.Content.Clases.DataClases.Plant
{
    /// <summary>
    /// Рассчитанная информация о растении
    /// </summary>
    public class PlaantCalculatedInfo
    {
        /// <summary>
        /// Количество собираемых за сезон растений
        /// </summary>
        public int HarvestCount => 
            //В зависимости от возможности собрать несколько урожаев с
            //куста, считаем среднее количество возможных полученных
            //плодов, с указанного в параметров количества кустов
            (_loadedInfo.AdditionalTime > 0) ? CalculateMultCount() : CalculateSingleCount();

        /// <summary>
        /// Стоимость продажи сока
        /// </summary>
        public int JuicePrice => (int)(GetOrdinaryPrice() * 2.25 * HarvestCount) - GetSeedsPrice();
        /// <summary>
        /// Стоимость продажи вина
        /// </summary>
        public int VinePrice => GetOrdinaryPrice() * 3 * HarvestCount - GetSeedsPrice();
        /// <summary>
        /// Стоимость продажи джема
        /// </summary>
        public int JamPrice => (50 + GetOrdinaryPrice() * 2) * HarvestCount - GetSeedsPrice();
        /// <summary>
        /// Стоимость продажи солений
        /// </summary>
        public int PiclkesPrice => (50 + GetOrdinaryPrice() * 2) * HarvestCount - GetSeedsPrice();

        /// <summary>
        /// Флаг переработки в сок
        /// Если не стоит, то перерабортка идёт в вино
        /// </summary>
        public bool IsAllowToJuice => _loadedInfo.IsAllowToJuice;
        /// <summary>
        /// Флаг переработки в консервы
        /// Если не стоит, то перерабортка идёт в джем
        /// </summary>
        public bool IsAllowToPickles => _loadedInfo.IsAllowToPickles;
        /// <summary>
        /// Флаг невозможности обработать растение
        /// </summary>
        public bool IsNotProcessed => _loadedInfo.IsNotProcessed;


        /// <summary>
        /// Загруженная информация о растении
        /// </summary>
        private PlantLoadedInfo _loadedInfo;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="loadedInfo">Загруженная информация о растении</param>
        public PlaantCalculatedInfo(PlantLoadedInfo loadedInfo)
        {
            //Проставляем переданные значения
            _loadedInfo = loadedInfo;
        }

        /// <summary>
        /// Получаем количество урожаев с куста за сезон
        /// </summary>
        /// <returns>Количество урожаев с куста за сезон</returns>
        private int CalculateMultCount()
        {
            //Получаем время созревания куста
            int hTime = GetPlantTime() - _loadedInfo.RipeningTime;
            //Получаем количество урожаев с одного куста
            double hCt = hTime / (double)_loadedInfo.AdditionalTime + 1;
            //Получаем количество единиц ресурса с куста
            double resCt = hCt * _loadedInfo.SingleHarvest;
            //ПОлучаем количество урожая со всех кустов
            double allCt = resCt * GlobalValues.CountPlants;
            //Возвращаем результат как целое число
            return (int)allCt;
        }

        /// <summary>
        /// Получаем количество урожаев с куста за сезон
        /// </summary>
        /// <returns>Количество урожаев с куста за сезон</returns>
        private int CalculateSingleCount()
        {
            //Получаем количество урожаев с одного куста
            double hCt = GetPlantTime() / (double)_loadedInfo.RipeningTime;
            //Получаем количество единиц ресурса с куста
            double resCt = hCt * _loadedInfo.SingleHarvest;
            //ПОлучаем количество урожая со всех кустов
            double allCt = resCt * GlobalValues.CountPlants;
            //Возвращаем результат как целое число
            return (int)allCt;
        }

        /// <summary>
        /// ПОлучаем цену за семена
        /// </summary>
        /// <returns>Цена за семена</returns>
        private int GetSeedsPrice()
        {
            int countSeeds;
            //Если куст садим один раз
            if (_loadedInfo.AdditionalTime > 0)
                //Нужно одно семечко
                countSeeds = 1;
            //Если куст садим несколько раз
            else
                //Нужно по семечку на каждый сбор урожая
                countSeeds = GetPlantTime() / _loadedInfo.RipeningTime;
            //ВОзвращаем количество покупок семян
            return _loadedInfo.GetMinPrice() * countSeeds * GlobalValues.CountPlants;
        }

        /// <summary>
        /// Получаем дефолтную цену за растение
        /// </summary>
        /// <returns>Дефолтная цена за растение</returns>
        private int GetOrdinaryPrice() => 
            //Получаем цену за растение
            _loadedInfo.GetGradePrice(Grades.Ordinary);

        /// <summary>
        /// Пполучаем время роста культуры
        /// </summary>
        /// <returns>Время роста культуры</returns>
        private int GetPlantTime() =>
            GlobalValues.SEASON_TIME * _loadedInfo.TimeModifier.GetValueOrDefault(1);


        /// <summary>
        /// Получаем класс загруженной информации
        /// </summary>
        /// <returns>Класс загруженной информации</returns>
        public PlantLoadedInfo GetLoadedInfo() => _loadedInfo;

        /// <summary>
        /// Если указана цена для данного грейда
        /// </summary>
        /// <param name="grade">Грейд для получения цены</param>
        /// <returns>Цена для грейда</returns>
        public int GetGradePrice(Grades grade) =>
             //Получаем цену за один экземпляр, минусуем цену
             //семян и множим её на весь урожай за сезон
             _loadedInfo.GetGradePrice(grade) * HarvestCount - GetSeedsPrice();

        /// <summary>
        /// Получаем максимальную цену
        /// </summary>
        /// <returns>Максимальная цена</returns>
        public int GetMaxPrice()
        {
            //Если обрабатывать нельзя
            if (_loadedInfo.IsNotProcessed)
                //Просто возвращаем максимальную цену
                return GetGradePrice(Grades.Iridium);
            //Если обрабатывать можно
            else
            {
                //Если можно давить сок
                if (IsAllowToJuice)
                {
                    //Если можно мариновать
                    if (IsAllowToPickles)
                        //Возвращаем цену от сока и солений
                        return Math.Max(JuicePrice, PiclkesPrice);
                    //Если нельзя мариновать
                    else
                        //Возвращаем цену от сока и варенья
                        return Math.Max(JuicePrice, JamPrice);
                }
                //Если нельзя давить сок
                else
                {
                    //Если можно мариновать
                    if (IsAllowToPickles)
                        //Возвращаем цену от вина и солений
                        return Math.Max(VinePrice, PiclkesPrice);
                    //Если нельзя мариновать
                    else
                        //Возвращаем цену от вина и варенья
                        return Math.Max(VinePrice, JamPrice);
                }
            }
        }
    }
}
