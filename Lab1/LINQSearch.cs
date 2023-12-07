using Lab1;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public class LinqSearch : ISearch
{
    public List<Cadre> Search(string xmlFilePath, Dictionary<string, string> filterCadres)
    {
        // Завантажте XML-файл
        XDocument doc = XDocument.Load(xmlFilePath);

        // Перетворіть XML в список Cadre
        List<Cadre> cadres = doc.Descendants("Cadr")
            .Select(x => new Cadre
            {
                Fullname = (string)x.Element("Fullname"),
                TypeOfeducation = (string)x.Element("TypeOfeducation"),
                NameOfFac = (string)x.Element("Facultyinformation").Element("NameOfFac"),
                EducInstitution = (string)x.Element("EducInstitution"),
                Department = (string)x.Element("Facultyinformation").Element("Department"),
                StartDate = (string)x.Element("StartDate"),
                Chair = (string)x.Element("Chair"),
                FinalDate = (string)x.Element("FinalDate")
            })
            .ToList();

        // Виконайте пошук за допомогою LINQ
        var filteredCadres = cadres
            .Where(cadre => filterCadres.All(filter =>
                filter.Key == "Fullname" ?
                cadre.GetType().GetProperty(filter.Key).GetValue(cadre, null).ToString().Contains(filter.Value) :
                cadre.GetType().GetProperty(filter.Key).GetValue(cadre, null).ToString() == filter.Value))
            .ToList();

        return filteredCadres;
    }
}



