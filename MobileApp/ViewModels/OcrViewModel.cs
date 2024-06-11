using MobileApp.Views;
using System.ComponentModel;
using System.Windows.Input;
using Azure;
using Azure.AI.Vision.ImageAnalysis;
using System.Net;
using System.Text.RegularExpressions;

namespace MobileApp.ViewModels;

public class OcrViewModel : INotifyPropertyChanged
{
    public OcrViewModel(string numeUtilizator, string denumireAliment)
    {
        ClientOcr = new ImageAnalysisClient(new Uri(EndpointOcr), new AzureKeyCredential(CheieOcr));
        RegexKcal = new Regex(@"(\d+)\skcal$");
        NumeUtilizator = numeUtilizator;
        DenumireAliment = denumireAliment;
        ComandaIntoarcereLaAlimentNou = new Command(IntoarceLaAlimentNou);
    }

    private void IntoarceLaAlimentNou()
    {
        Application.Current.MainPage = new PaginaAlimentNou(NumeUtilizator);
    }

    public async void ExtrageValoriNutritionale(BinaryData fotografie)
    {
        ImageAnalysisResult resultat =
            await ClientOcr.AnalyzeAsync(fotografie, VisualFeatures.Read);

        var liniiDetectieOcr = resultat.Read.Blocks.First().Lines;

        for (int i = 0; i < liniiDetectieOcr.Count; i++)
        {
            if (RegexKcal.IsMatch(liniiDetectieOcr.ElementAt(i).Text))
            {
                CaloriiAliment = RegexKcal.Match(liniiDetectieOcr.ElementAt(i).Text).Groups[1].Value;
                continue;
            }

            try
            {
                if (liniiDetectieOcr.ElementAt(i - 1).Text.ToLower().Contains("grăsimi"))
                {
                    GrasimiAliment = liniiDetectieOcr.ElementAt(i).Text.Split(' ').First();
                    continue;
                }

                if (liniiDetectieOcr.ElementAt(i - 1).Text.ToLower().Contains("glucide"))
                {
                    GlucideAliment = liniiDetectieOcr.ElementAt(i).Text.Split(' ').First();
                    continue;
                }

                if (liniiDetectieOcr.ElementAt(i - 1).Text.ToLower().Contains("proteine"))
                {
                    ProteineAliment = liniiDetectieOcr.ElementAt(i).Text.Split(' ').First();
                    continue;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                continue;
            }
        }

        //Application.Current.MainPage = new PaginaValidareValori(
        //    NumeUtilizator, DenumireAliment, CaloriiAliment, GrasimiAliment, GlucideAliment, ProteineAliment);
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaIntoarcereLaAlimentNou { get; private set; }
    private string NumeUtilizator { get; init; }
    private string DenumireAliment { get; init; }
    private string CaloriiAliment { get; set; }
    private string GrasimiAliment { get; set; }
    private string GlucideAliment { get; set; }
    private string ProteineAliment { get; set; }
    private Regex RegexKcal { get; init; }
    private ImageAnalysisClient ClientOcr { get; init; }
    private string CheieOcr { get => "CHEIE_OCR"; }
    private string EndpointOcr { get => "ENDPOINT_OCR"; }
}
