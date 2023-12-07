using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab1.MainPage;

namespace Lab1;
public interface ISearch
{ 
    List<Cadre> Search(string xmlFilePath, Dictionary <string, string> filterCadres);
}




