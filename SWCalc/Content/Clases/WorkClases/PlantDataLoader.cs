using SWCalc.Content.Clases.DataClases;
using SWCalc.Content.Clases.DataClases.Plant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SWCalc.Content.Clases.WorkClases
{
    /// <summary>
    /// Класс загрузки данных о растениях
    /// </summary>
    internal class PlantDataLoader
    {
        /// <summary>
        /// Класс информации о растениях
        /// </summary>
        private const string DATA_FILE_NAME = "plants.info.xml";

        /// <summary>
        /// Класс сериализации классов в XML
        /// </summary>
        private XmlSerializer _serializer;
        /// <summary>
        /// Путь к файлу с данными
        /// </summary>
        private string _dataFilePath;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public PlantDataLoader()
        {
            //Инициализируем путь к файлу с данными
            _dataFilePath = $"{Environment.CurrentDirectory}\\{DATA_FILE_NAME}";
            //Инициализуем сериализатор
            _serializer = new XmlSerializer(typeof(PlantsData));
        }

        /// <summary>
        /// Выполняем загрузку данных о растениях
        /// </summary>
        /// <returns>Данные о растениях</returns>
        public PlantsData LoadContent()
        {
            //Инициализируем дефолтные значения
            PlantsData ex = new PlantsData();
            //Если файл данных есть
            if (File.Exists(_dataFilePath))
            {
                //Инициализируем поток для чтения файла
                using (FileStream fs = File.OpenRead(_dataFilePath))
                    //Десериализуем контент файла в класс
                    ex = (PlantsData)_serializer.Deserialize(fs);
            }
            //Возвращаем результат
            return ex;
        }

        /// <summary>
        /// Выполняем сохранение данных о растениях
        /// </summary>
        /// <returns>Данные о растениях</returns>
        public void SaveContent(PlantsData data)
        {
            //Инициализируем поток для записи в файл
            using (FileStream fs = File.OpenWrite(_dataFilePath))
                //Сериализуем денные из класса в файл
                _serializer.Serialize(fs, data);            
        }

    }
}
