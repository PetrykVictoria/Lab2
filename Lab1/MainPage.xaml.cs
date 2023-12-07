using Microsoft.Maui.Controls.Shapes;
using System.Xml.Linq;
using System.Xml.Xsl;


namespace Lab1;

public class Cadre
{
    public string Fullname { get; set; }
    public string NameOfFac { get; set; }
    public string Department { get; set; }
    public string Chair { get; set; }
    public string TypeOfeducation { get; set; }
    public string EducInstitution { get; set; }
    public string StartDate { get; set; }
    public string FinalDate { get; set; }
}
public partial class MainPage : ContentPage
{
    private List<Cadre> cadres; //створюємо об'єкт типу ліст кадрів іпотім об'єкти для пошуку
    public ISearch LinqSearch = new LinqSearch();
    public ISearch DomSearch = new DomSearch();
    public ISearch SaxSearch= new SaxSearch();
    private string xmlFilePath = "";
    public Dictionary<string, string> filterCadres = new Dictionary<string, string>();

    public MainPage()
    {
        InitializeComponent();
    }
    private async void LoadDataFromXml()
    {
        try
        {
            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            cadres = xmlDoc.Descendants("Cadr")
                .Select(c => new Cadre
                {
                    Fullname = c.Element("Fullname")?.Value,
                    NameOfFac = c.Element("Facultyinformation")?.Element("NameOfFac")?.Value,
                    Department = c.Element("Facultyinformation")?.Element("Department")?.Value,
                    Chair = c.Element("Chair")?.Value,
                    TypeOfeducation = c.Element("TypeOfeducation")?.Value,
                    EducInstitution = c.Element("EducInstitution")?.Value,
                    StartDate = c.Element("StartDate")?.Value,
                    FinalDate = c.Element("FinalDate")?.Value
                })
                .ToList();

            // Заповнення Picker даними
            if (cadres.Any())
            {
                NameOfFacPicker.ItemsSource = cadres.Select(c => c.NameOfFac).Distinct().ToList();
                DepartmentPicker.ItemsSource = cadres.Select(c => c.Department).Distinct().ToList();
                ChairPicker.ItemsSource = cadres.Select(c => c.Chair).Distinct().ToList();
                TypeOfEducPicker.ItemsSource = cadres.Select(c => c.TypeOfeducation).Distinct().ToList();
                EducInstitutionPicker.ItemsSource = cadres.Select(c => c.EducInstitution).Distinct().ToList();
                StartDatePicker.ItemsSource = cadres.Select(c => c.StartDate).Distinct().ToList();
                FinalDatePicker.ItemsSource = cadres.Select(c => c.FinalDate).Distinct().ToList();

            }
            else
            {
                await DisplayAlert("Помилка", "XML не містить дані", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Помилка завантаження даних: {ex.Message}", "OK");
        }
    }

    private void FullNameBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            FullNameEntry.IsEnabled = true;
        }
        else
        {
            FullNameEntry.IsEnabled = false;
            FullNameEntry.Text = string.Empty; 
        }
    }

    private void TypeOfEducBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            TypeOfEducPicker.IsEnabled = true;
        }
        else
        {
            TypeOfEducPicker.IsEnabled = false;
            TypeOfEducPicker.SelectedIndex = -1;
        }
    }

    private void NameOfFacBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            NameOfFacPicker.IsEnabled = true;
        }
        else
        {
            NameOfFacPicker.IsEnabled = false;
            NameOfFacPicker.SelectedIndex = -1;
        }
    }

    private void EducInstitutionBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            EducInstitutionPicker.IsEnabled = true;
        }
        else
        {
            EducInstitutionPicker.IsEnabled = false;
            EducInstitutionPicker.SelectedIndex = -1;
        }
    }

    private void DepartmentBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            DepartmentPicker.IsEnabled = true;
        }
        else
        {
            DepartmentPicker.IsEnabled = false;
            DepartmentPicker.SelectedIndex = -1;
        }
    }

    private void StartDateBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            StartDatePicker.IsEnabled = true;
        }
        else
        {
            StartDatePicker.IsEnabled = false;
            StartDatePicker.SelectedIndex = -1;
        }
    }

    private void ChairBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            ChairPicker.IsEnabled = true;
        }
        else
        {
            ChairPicker.IsEnabled = false;
            ChairPicker.SelectedIndex = -1;
        }
    }

    private void FinalDateBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            FinalDatePicker.IsEnabled = true;
        }
        else
        {
            FinalDatePicker.IsEnabled = false;
            FinalDatePicker.SelectedIndex = -1;
        }
    }

    private async void ChooseFileButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync();

            if (result != null)
            {
                // Отримайте обраний файл
                xmlFilePath = result.FullPath;


                if (System.IO.Path.GetExtension(xmlFilePath = result.FullPath)?.ToLower() == ".xml")
                {

                    await DisplayAlert("Успіх", "Файл обрано", "OK");
                    LoadDataFromXml();
                }
                else
                {
                    await DisplayAlert("Помилка", "Оберіть файл типу XML.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error picking file: {ex.Message}", "OK");
        }
    }

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        FullNameEntry.Text = string.Empty;
        TypeOfEducPicker.SelectedIndex = -1;
        NameOfFacPicker.SelectedIndex = -1;
        EducInstitutionPicker.SelectedIndex = -1;
        DepartmentPicker.SelectedIndex = -1;
        StartDatePicker.SelectedIndex = -1;
        ChairPicker.SelectedIndex = -1;
        FinalDatePicker.SelectedIndex = -1;

        FullNameBox.IsChecked = false;
        TypeOfEducBox.IsChecked = false;
        NameOfFacBox.IsChecked = false;
        EducInstitutionBox.IsChecked = false;
        DepartmentBox.IsChecked = false;
        StartDateBox.IsChecked = false;
        ChairBox.IsChecked = false;
        FinalDateBox.IsChecked = false;

        SearchSAX.IsChecked = false;
        SearchDOM.IsChecked = false;
        SearchLINQ.IsChecked = false;

        SearchResultsCollectionView.ItemsSource = null;
    }

    private async void CloseButtonClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Підтвердження виходу", "Ви справді хочете вийти?", "Так", "Ні");
        if (answer)
        {
            // Вийти з додатку
            MainThread.BeginInvokeOnMainThread(() => System.Environment.Exit(0));
        }
    }


    private async void Transformation_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (xmlFilePath != string.Empty)
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load("C:\\Users\\Викуся\\source\\repos\\Lab1\\Lab1\\Transform.xsl");
                xslt.Transform(xmlFilePath, System.IO.Path.ChangeExtension(xmlFilePath, "html"));
                await DisplayAlert("Успішно", "Конвертація пройшла успішно", "OK");
            }
            else
            {
                await DisplayAlert("Помилка", "Файл не обрано.", "OK");
            }
        }

        catch (Exception ex)
        {
            await DisplayAlert("Помилка", $"Помилка конвертації: {ex.Message}", "OK");
        }
    }

    private void SearchFilterCadres(string key, string value, bool isEnabled)
    {
        if (!string.IsNullOrEmpty(value) && isEnabled)
        {
            filterCadres[key] = value;
        }
    }
    private void SearchButton_Clicked(object sender, EventArgs e)
    {
        if (xmlFilePath != string.Empty)
        {
            if (SearchLINQ.IsChecked == true || SearchSAX.IsChecked == true || SearchDOM.IsChecked == true)
            {
                SearchFilterCadres("Fullname", FullNameEntry.Text, FullNameEntry.IsEnabled);
                SearchFilterCadres("TypeOfeducation", (string)TypeOfEducPicker.SelectedItem, TypeOfEducPicker.IsEnabled);
                SearchFilterCadres("NameOfFac", (string)NameOfFacPicker.SelectedItem, NameOfFacPicker.IsEnabled);
                SearchFilterCadres("EducInstitution", (string)EducInstitutionPicker.SelectedItem, EducInstitutionPicker.IsEnabled);
                SearchFilterCadres("Department", (string)DepartmentPicker.SelectedItem, DepartmentPicker.IsEnabled);
                SearchFilterCadres("StartDate", (string)StartDatePicker.SelectedItem, StartDatePicker.IsEnabled);
                SearchFilterCadres("Chair", (string)ChairPicker.SelectedItem, ChairPicker.IsEnabled);
                SearchFilterCadres("FinalDate", (string)FinalDatePicker.SelectedItem, FinalDatePicker.IsEnabled);

                if (SearchLINQ.IsChecked == true)
                {
                    var filteredCadres = LinqSearch.Search(xmlFilePath, filterCadres);
                    SearchResultsCollectionView.ItemsSource = filteredCadres;
                }
                if (SearchSAX.IsChecked == true)
                {
                    var filteredCadres = SaxSearch.Search(xmlFilePath, filterCadres);
                    SearchResultsCollectionView.ItemsSource = filteredCadres;
                }
                if (SearchDOM.IsChecked == true)
                {
                    var filteredCadres = DomSearch.Search(xmlFilePath, filterCadres);
                    SearchResultsCollectionView.ItemsSource = filteredCadres;
                }

                filterCadres.Clear();
            }
            else
            {
                DisplayAlert("Помилка", "Оберіть метод пошуку.", "ОК");

            }
        }
        else
        {
            DisplayAlert("Помилка", "Оберіть файл.", "ОК");
        }
       
    }

}