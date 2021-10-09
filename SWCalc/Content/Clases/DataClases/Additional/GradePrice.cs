using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SWCalc.Content.Clases.DataClases.Global.Enums;

namespace SWCalc.Content.Clases.DataClases.Additional
{
    /// <summary>
    /// Класс описывающий связь цены и грейда
    /// </summary>
    public class GradePrice
    {
        /// <summary>
        /// Значение грейда
        /// </summary>
        public Grades Grade { get; set; }
        /// <summary>
        /// Значение стоимости
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public GradePrice()
        {
            //Проставляем дефолтные значения
            Grade = Grades.Ordinary;
            Cost = 0;
        }
    }
}
