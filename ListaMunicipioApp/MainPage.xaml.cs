﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using ListaMunicipioApp.Model;

namespace ListaMunicipioApp
{
    public partial class MainPage : ContentPage
    {
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            string UF = textUF.Text.ToUpper();
            var client = new HttpClient();
            var json = await client.GetStringAsync($"http://ibge.herokuapp.com/municipio/?val={UF}");
            var dados = JsonConvert.DeserializeObject<Object>(json);

            JObject municipios = JObject.Parse(json);

            Dictionary<string, string> dadosMunicipios = municipios.ToObject<Dictionary <string, string>>();

            List<Municipios> lista = new List<Municipios>();
            foreach (KeyValuePair<string, string> municipio in dadosMunicipios)
            {
                Municipios ObjMunicipio = new Municipios();
                ObjMunicipio.nome = municipio.Key;
                ObjMunicipio.codigo = municipio.Value;
                lista.Add(ObjMunicipio);
            }

            /*ArrayList lista = new ArrayList();
            foreach (KeyValuePair<string, string> municipio in dadosMunicipios)
            {
                lista.Add(municipio.Key);
            }*/
            listaMunicipio.ItemsSource = lista;

        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
