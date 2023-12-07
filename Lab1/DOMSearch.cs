using System;
using System.Collections.Generic;
using System.Xml;
using Lab1;

namespace Lab1
{
    public class DomSearch : ISearch
    {
        public List<Cadre> Search(string xmlFilePath, Dictionary<string, string> filterCadres)
        {
            // Завантажте XML-файл
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            // Перетворіть XML в список Cadre
            List<Cadre> cadres = new List<Cadre>();
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                Cadre cadre = new Cadre
                {
                    Fullname = node["Fullname"].InnerText,
                    TypeOfeducation = node["TypeOfeducation"].InnerText,
                    NameOfFac = node["Facultyinformation"]["NameOfFac"].InnerText,
                    EducInstitution = node["EducInstitution"].InnerText,
                    Department = node["Facultyinformation"]["Department"].InnerText,
                    StartDate = node["StartDate"].InnerText,
                    Chair = node["Chair"].InnerText,
                    FinalDate = node["FinalDate"].InnerText
                };
                cadres.Add(cadre);
            }

            // Виконайте пошук за допомогою DOM
            var filteredCadres = cadres
                .Where(cadre => filterCadres.All(filter =>
                    filter.Key == "Fullname" ?
                    cadre.GetType().GetProperty(filter.Key).GetValue(cadre, null).ToString().Contains(filter.Value) :
                    cadre.GetType().GetProperty(filter.Key).GetValue(cadre, null).ToString() == filter.Value))
                .ToList();

            return filteredCadres;
        }
    }
}
