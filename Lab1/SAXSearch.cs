using System;
using System.Collections.Generic;
using System.Xml;
using Lab1;

namespace Lab1;

public class SaxSearch : ISearch
{
    public List<Cadre> Search(string xmlFilePath, Dictionary<string, string> filterCadres)
    {
        // Створіть XmlReader
        XmlReader reader = XmlReader.Create(xmlFilePath);

        // Створіть список Cadre
        List<Cadre> cadres = new List<Cadre>();
        Cadre cadre = null;

        // Читайте XML-файл
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    if (reader.Name == "Cadr")
                    {
                        cadre = new Cadre();
                    }
                    else if (cadre != null)
                    {
                        if (reader.Name == "Fullname")
                        {
                            cadre.Fullname = reader.ReadElementContentAsString();
                        }
                        else if (reader.Name == "TypeOfeducation")
                        {
                            cadre.TypeOfeducation = reader.ReadElementContentAsString();
                        }
                        else if (reader.Name == "NameOfFac")
                        {
                            cadre.NameOfFac = reader.ReadElementContentAsString();
                        }
                        else if (reader.Name == "EducInstitution")
                        {
                            cadre.EducInstitution = reader.ReadElementContentAsString();
                        }
                        else if (reader.Name == "Department")
                        {
                            cadre.Department = reader.ReadElementContentAsString();
                        }
                        else if (reader.Name == "StartDate")
                        {
                            cadre.StartDate = reader.ReadElementContentAsString();
                        }
                        else if (reader.Name == "Chair")
                        {
                            cadre.Chair = reader.ReadElementContentAsString();
                        }
                        else if (reader.Name == "FinalDate")
                        {
                            cadre.FinalDate = reader.ReadElementContentAsString();
                        }
                    }
                    break;
                case XmlNodeType.EndElement:
                    if (reader.Name == "Cadr")
                    {
                        cadres.Add(cadre);
                        cadre = null;
                    }
                    break;
            }
        }

        // Виконайте пошук за допомогою LINQ
        var filteredCadres = cadres
            .Where(c => filterCadres.All(filter =>
                filter.Key == "Fullname" ?
                c.GetType().GetProperty(filter.Key).GetValue(c, null).ToString().Contains(filter.Value) :
                c.GetType().GetProperty(filter.Key).GetValue(c, null).ToString() == filter.Value))
            .ToList();

        return filteredCadres;
    }
}
