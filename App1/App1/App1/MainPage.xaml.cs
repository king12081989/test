using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using Xamarin.Forms;
using System.Security.Cryptography;




namespace App1
{


    public class RateInfo
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Secname { get; set; }
        public string country { get; set; }
        //public DateTime Date { get; set; }
        public DateTime Regdate { get; set; }
        public string err { get; set; }
        //public decimal Ask { get; set; }
        //public decimal Bid { get; set; }
    }
    public class SendPost
    {
        public string name { get; set; }
        public string pass { get; set; }
        public string token { get; set; }

    }

    public partial class MainPage : ContentPage
    {
        static public RateInfo userInfo;
        static public string tokenF;
        static public string site;
        static public string site1;
        public MainPage()
        {
            site = "http://king1989.zzz.com.ua/";
            site1 = "https://galvanizing-moistur.000webhostapp.com/";
            Random rnd = new Random();
            DateTime now = DateTime.Now;
            int nowint = now.Millisecond;
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes("User" + rnd.Next(nowint)));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                //указывает, что нужно преобразовать элемент в шестнадцатиричную строку длиной в два символа
                sBuilder.Append(data[i].ToString("x2"));
            }
            tokenF = sBuilder.ToString();
            InitializeComponent();
            Btn_OK.Clicked += btlk;
            Btn_reg.Clicked += btreg;


            async void btlk(object sender, EventArgs e)
            {

                if (Logtxt.Text.Length < 1)
                {
                    await DisplayAlert("Ошибка", "Введите логин", "Ok");
                }
                else if (pastxt.Text.Length < 1)
                {
                    await DisplayAlert("Ошибка", "Введите пароль", "Ok");
                }
                else
                {
                    Btn_OK.IsEnabled = false;
                    string login = Logtxt.Text.Trim();
                    //await DisplayAlert("Ошибка","Введите логин","Ok");

                    string pass = pastxt.Text.Trim();
                    //GET
                    //string url = "https://galvanizing-moistur.000webhostapp.com/index.php";
                    //url += "?name=" + Logtxt.Text;
                    //url += "&token=111";
                    //url += "&pass=" + pastxt.Text;


                    //try
                    //{

                    //    HttpClient client = new HttpClient();
                    //    client.BaseAddress = new Uri(url);
                    //    var response = await client.GetAsync(client.BaseAddress);
                    //    response.EnsureSuccessStatusCode();
                    //    var content = await response.Content.ReadAsStringAsync();
                    //    JObject o = JObject.Parse(content);
                    //    var rateInfo = JsonConvert.DeserializeObject<RateInfo>(o.ToString());



                    //    if (rateInfo.err.Length < 1)
                    //    {

                    //        await DisplayAlert("Уведомление", rateInfo.Id.ToString(), "ОK");
                    //    }
                    //    else
                    //    {
                    //        await DisplayAlert("Уведомление", rateInfo.err.ToString(), "ОK");
                    //    }


                    //}


                    //catch (Exception ex)
                    //{

                    //}




                    //POST


                    var values = new Dictionary<string, string>();
                    values.Add("name", login);
                    values.Add("token", login + sBuilder.ToString());

                    //MD5 md5Hasher1 = MD5.Create();
                    data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pass));
                    sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                    {
                        //указывает, что нужно преобразовать элемент в шестнадцатиричную строку длиной в два символа
                        sBuilder.Append(data[i].ToString("x2"));
                    }
                    values.Add("pass", sBuilder.ToString());
                    values.Add("act", "login");
                    //tokenF = login + sBuilder.ToString();
                    //txt3.Text = sBuilder.ToString();
                    var content = new FormUrlEncodedContent(values);

                    using (var client = new HttpClient())
                    {
                        try
                        {
                            var httpResponseMessage = await client.PostAsync(site + "login.php", content);

                            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                            {
                                var content1 = await httpResponseMessage.Content.ReadAsStringAsync();
                                //await DisplayAlert("Уведомление", content1.ToString(), "ОK");
                                content1 = content1.Remove(0, 1);

                                JObject o = JObject.Parse(content1);
                                var rateInfo = JsonConvert.DeserializeObject<RateInfo>(o.ToString());
                                if (rateInfo.err.Length < 1)
                                {

                                    //await DisplayAlert("Уведомление", rateInfo.Regdate.ToString(), "ОK");
                                    userInfo = rateInfo;
                                    await Navigation.PushModalAsync(new Main());
                                }
                                else
                                {
                                    await DisplayAlert("Уведомление", rateInfo.err.ToString(), "ОK");
                                }


                            }
                        }
                        catch (OperationCanceledException) { }
                        catch (Exception) { await DisplayAlert("Уведомление", "Сбой сети", "ОK"); }
                    }
                    Btn_OK.IsEnabled = true;
                }





            }





            async void btreg(object sender, EventArgs e)
            {
                await Navigation.PushAsync(new regpage());
            }

        }
    }
}
