﻿using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace MobileApp.Models;

public sealed class ConexiuneHttpsSingleton
{
    public static ConexiuneHttpsSingleton ObtineInstanta()
    {
        if (Instanta == null)
            Instanta = new ConexiuneHttpsSingleton();

        return Instanta;
    }

    private ConexiuneHttpsSingleton()
    {
        Client = new HttpClient();
        Client.BaseAddress = new Uri("https://webapilicenta.azurewebsites.net/");
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        EsteTokenSalvat = false;
    }

    public async Task TrimiteCerereHttpGetAsincron(string uriCerere)
    {
        AdaugaAntetAutorizare();
        Raspuns = await Client.GetAsync(uriCerere);
    }

    public async Task TrimiteCerereHttpPostAsincron<Schema>(
        string uriCerere,
        Schema valori,
        bool esteConectareUtilizator)
    {
        if (EsteTokenSalvat)
            AdaugaAntetAutorizare();

        string json = JsonSerializer.Serialize(valori);
        var continut = new StringContent(json, Encoding.UTF8, "application/json");
        Raspuns = await Client.PostAsync(uriCerere, continut);

        if (!EsteTokenSalvat && Raspuns.IsSuccessStatusCode && esteConectareUtilizator)
        {
            StocheazaToken();
            EsteTokenSalvat = true;
        }
    }

    private void AdaugaAntetAutorizare()
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
    }

    private async void StocheazaToken()
    {
        string tokenNeprelucrat = await Raspuns.Content.ReadAsStringAsync();
        Token = tokenNeprelucrat.Trim('"');
    }

    public HttpClient Client { get; private set; }
    public HttpResponseMessage Raspuns { get; private set; }
    private string Token { get; set; }
    private bool EsteTokenSalvat { get; set; }

    private static ConexiuneHttpsSingleton Instanta {  get; set; }
}