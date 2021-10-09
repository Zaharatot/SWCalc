using SWCalc.Content.Clases.DataClases.Additional;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static SWCalc.Content.Clases.DataClases.Global.Enums;

namespace SWCalc.Content.Clases.DataClases.Plant
{
    /// <summary>
    /// Класс информации о растении
    /// </summary>
    public class PlantLoadedInfo
    {
        /// <summary>
        /// Название растения
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Имя файла иконки растения
        /// </summary>
        public string IconName { get; set; }
        /// <summary>
        /// Цена покупки семян в Joja-март
        /// </summary>
        public int PriceJoja { get; set; }
        /// <summary>
        /// Цена покупки семян у Пьера
        /// </summary>
        public int PricePiere { get; set; }
        /// <summary>
        /// Цена покупки в другом месте
        /// </summary>
        public int PriceOther { get; set; }
        /// <summary>
        /// Словарь грейдов, проассоциированных с ценами
        /// </summary>
        public List<GradePrice> GradePrices { get; set; }
        /// <summary>
        /// Урожай с одного куста
        /// </summary>
        public double SingleHarvest { get; set; }
        /// <summary>
        /// Время выращивания урожая
        /// </summary>
        public int RipeningTime { get; set; }
        /// <summary>
        /// Время до дополнительного сбора
        /// </summary>
        public int AdditionalTime { get; set; }
        /// <summary>
        /// Флаг переработки в сок
        /// Если не стоит, то перерабортка идёт в вино
        /// </summary>
        public bool IsAllowToJuice { get; set; }
        /// <summary>
        /// Флаг переработки в консервы
        /// Если не стоит, то перерабортка идёт в джем
        /// </summary>
        public bool IsAllowToPickles { get; set; }
        /// <summary>
        /// Флаг невозможности обработать растение
        /// </summary>
        public bool IsNotProcessed { get; set; }
        /// <summary>
        /// Значение модификатора времени
        /// </summary>
        public int? TimeModifier { get; set; }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public PlantLoadedInfo()
        {
            //Проставляем дефолтные значения
            IconName = Name = "";
            PriceJoja = PricePiere = PriceOther = 
                RipeningTime = AdditionalTime = 0;
            SingleHarvest = 0;
            TimeModifier = null;
            IsAllowToJuice = IsAllowToPickles = IsNotProcessed = false;
            GradePrices = new List<GradePrice>();
        }

        /// <summary>
        /// Если указана цена для данного грейда
        /// </summary>
        /// <param name="grade">Грейд для получения цены</param>
        /// <returns>Цена для грейда</returns>
        public int GetGradePrice(Grades grade)
        {
            int ex = 0;
            //Ищем цену для такого грейда
            GradePrice price = GradePrices.FirstOrDefault(gr => gr.Grade == grade);
            //Если цена найдена
            if (price != null)
                //Возвращаем её значение
                ex = price.Cost;
            //По дефолту вернём 0
            return ex;
        }

        /// <summary>
        /// Получаем минимальную цену на семена
        /// </summary>
        /// <returns>Минимальная цена на семена</returns>
        public int GetMinPrice()
        {
            int ex = 0;
            //Если есть цена на joja
            if (PriceJoja != 0)
            {
                //Если есть цена у пьера
                if (PricePiere != 0)
                {
                    //Если есть все три цены
                    if (PriceOther != 0)
                        //Получаем минимальную
                        ex = Math.Min(PriceJoja, Math.Min(PricePiere, PriceOther));
                    //Если есть две цены
                    else
                        //Получаем минимальную
                        ex = Math.Min(PriceJoja, PricePiere);
                }
                //Если есть только две цены
                else if (PriceOther != 0)
                    //Получаем минимальную
                    ex = Math.Min(PriceJoja, PriceOther);
                //Если цена толшько одна
                else
                    //Ставим её
                    ex = PriceJoja;
            }
            //Если есть цена от Пьера
            else if(PricePiere != 0)
            {
                //Если есть две цены
                if (PriceOther != 0)
                    //Получаем минимальную
                    ex = Math.Min(PricePiere, PriceOther);
                //Если есть только одна
                else
                    //Ставим её
                    ex = PriceJoja;
            }
            //Если есть только третья цена
            else if(PriceOther != 0)
                //Ставим её
                ex = PriceJoja;
            //Возвращаем результат
            return ex;
        }

        /// <summary>
        /// Получаем полный путь к иконке
        /// </summary>
        /// <returns>Полный путь к иконке</returns>
        public string GetIconPath() =>
            //Формируем строку пути к файлу иконки
            $"{Environment.CurrentDirectory}\\Icons\\{IconName}";
    }
}
