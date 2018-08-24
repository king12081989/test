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
using Xamarin.Forms.Xaml;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public class RegInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string err { get; set; }
    }






    public partial class regpage : ContentPage
    {
        public regpage()
        {
            List<RegInfo> countrymas = new List<RegInfo>();

            List<RegInfo> regionmas = new List<RegInfo>();
            List<RegInfo> citymas = new List<RegInfo>();
            string site = MainPage.site + "reg.php";

            InitializeComponent();



            //----------------------------country picer
            loadpicerCountry();


            picker.SelectedIndexChanged += picker_SelectedIndexChanged;
            pickerreg.SelectedIndexChanged += pickerreg_SelectedIndexChanged;
            Bregf.Clicked += btregf;
            //Bprov.Clicked += bprovv;

            void pickerreg_SelectedIndexChanged(object sender, EventArgs e)
            {
                pickercity.Items.Clear();
                pickercity.Title = "Загрузка";
                pickercity.IsEnabled = false;

                loadpicercity(int.Parse(regionmas[pickerreg.SelectedIndex].id));
            }

            void picker_SelectedIndexChanged(object sender, EventArgs e)
            {


                pickerreg.Items.Clear();
                pickerreg.Title = "Загрузка";
                pickerreg.IsEnabled = false;
                pickercity.Items.Clear();
                pickercity.Title = "Город";
                pickercity.IsEnabled = false;
                loadpicerRegion(int.Parse(countrymas[picker.SelectedIndex].id));
            }

            /////////////////////////////////////////
            Boolean bprovv(string inputstr, int p)
            {
                string strPattern = "";
                if (p == 1)
                {
                    strPattern = "[а-яА-Яa-zA-Z][^0-9]";
                }
                if (p == 2)
                {
                    strPattern = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";
                }
                if (p == 3)
                {
                    strPattern = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
                }
                if (p == 4)
                {
                    strPattern = "[a-zA-Z0-9]";
                }
                bool y = Regex.IsMatch(inputstr, strPattern);
                if (y == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            async void btregf(object sender, EventArgs e)
            {

                if (Nametxt.Text.Length < 1)
                {
                    await DisplayAlert("Заполните все поля", "Поле 'Имя' не заполнено.", "ОK");
                    Nametxt.Focus();
                }

                else if (bprovv(Nametxt.Text.Trim(), 1) == false)
                {
                    await DisplayAlert("Заполните все поля", "Поле 'Имя' заполнено не верно.", "ОK");
                    Nametxt.Focus();
                }
                else if (Secnametxt.Text.Length < 1)
                {
                    await DisplayAlert("Заполните все поля", "Поле 'Фамилия' не заполнено.", "ОK");
                    Secnametxt.Focus();
                }
                else if (bprovv(Nametxt.Text.Trim(), 1) == false)
                {
                    await DisplayAlert("Заполните все поля", "Поле 'Фамилия' заполнено не верно.", "ОK");
                    Secnametxt.Focus();
                }
                else if (mail.Text.Length < 1)
                {
                    await DisplayAlert("Заполните все поля", "Поле 'e-mail' не заполнено.", "ОK");
                    mail.Focus();
                }
                else if (bprovv(mail.Text.Trim(), 2) == false)
                {
                    await DisplayAlert("Заполните все поля", "Поле 'e-mail' заполнено не верно.", "ОK");
                    mail.Focus();
                }
                else if (phone.Text.Length < 1)
                {
                    await DisplayAlert("Заполните все поля", "Поле 'Телефон' не заполнено.", "ОK");
                    phone.Focus();
                    //await DisplayAlert("Уведомление", picker.SelectedIndex.ToString(), "ОK");
                }
                else if (bprovv(phone.Text.Trim(), 3) == false)
                {
                    await DisplayAlert("Заполните все поля", "Поле 'Телефон' заполнено не верно.", "ОK");
                    phone.Focus();
                }
                //else if (phone.Text.Length < 10)
                //{
                //    await DisplayAlert("Заполните все поля", "Поле 'Телефон' заполнено не верно. ", "ОK");
                //    phone.Focus();
                //}
                else if (picker.SelectedIndex < 0)
                {
                    await DisplayAlert("Заполните все поля", "Страна не выбрана", "ОK");
                    picker.Focus();

                }
                else if (pickerreg.SelectedIndex < 0)
                {
                    await DisplayAlert("Заполните все поля", "Регион не выбран", "ОK");
                    pickerreg.Focus();

                }
                else if (pickercity.SelectedIndex < 0)
                {
                    await DisplayAlert("Заполните все поля", "Город не выбран", "ОK");
                    pickercity.Focus();

                }
                else if (pastxt.Text.Length < 1)
                {
                    await DisplayAlert("Заполните все поля", "Поле 'Пароль' не заполнено.", "ОK");
                    pastxt.Focus();
                    //await DisplayAlert("Уведомление", picker.SelectedIndex.ToString(), "ОK");
                }
                else if (pasctxt.Text.Length < 1)
                {
                    await DisplayAlert("Заполните все поля", "Повторите пароль.", "ОK");
                    pasctxt.Focus();
                    //await DisplayAlert("Уведомление", picker.SelectedIndex.ToString(), "ОK");
                }
                else if (bprovv(pastxt.Text.Trim(), 4) == false)
                {
                    await DisplayAlert("Заполните все поля", "Поле 'Пароль' заполнено не верно.", "ОK");
                    pastxt.Focus();
                }
                else if (bprovv(pasctxt.Text.Trim(), 4) == false)
                {
                    await DisplayAlert("Заполните все поля", "Поле 'Пароль' заполнено не верно.", "ОK");
                    pasctxt.Focus();
                }
                else if (pasctxt.Text.Trim() != pasctxt.Text.Trim())
                {
                    await DisplayAlert("Заполните все поля", "Пароли не совпадают.", "ОK");
                }
                else
                {
                    MD5 md5Hasher = MD5.Create();
                    byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pastxt.Text.Trim()));
                    StringBuilder sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                    {
                        //указывает, что нужно преобразовать элемент в шестнадцатиричную строку длиной в два символа
                        sBuilder.Append(data[i].ToString("x2"));
                    }

                    Bregf.IsEnabled = false;
                    var values = new Dictionary<string, string>();
                    values.Add("act", "reg");
                    values.Add("name", Nametxt.Text.Trim());
                    values.Add("secname", Secnametxt.Text.Trim());
                    values.Add("mail", mail.Text.Trim());
                    values.Add("phone", phone.Text.Trim());
                    values.Add("idcountry", countrymas[picker.SelectedIndex].id);
                    values.Add("idregion", regionmas[pickerreg.SelectedIndex].id);
                    values.Add("idcity", citymas[pickercity.SelectedIndex].id);
                    values.Add("pas", sBuilder.ToString());
                    values.Add("token", MainPage.tokenF.ToString());

                    var content = new FormUrlEncodedContent(values);
                    using (var client = new HttpClient())
                    {
                        try
                        {
                            var httpResponseMessage = await client.PostAsync(site, content);

                            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                            {
                                var content1 = await httpResponseMessage.Content.ReadAsStringAsync();
                                //await DisplayAlert("Уведомление", content1.ToString(), "ОK");
                                content1 = content1.Remove(0, 1);
                                JObject o = JObject.Parse(content1);
                                var regInfo = JsonConvert.DeserializeObject<RateInfo>(o.ToString());
                                if (regInfo.err.Length < 1)
                                {
                                    await DisplayAlert("Уведомление", "Успешно зарегистрировано. Вам отправлено письмо для подтверждения регистрации", "ОK");
                                }
                                else
                                {
                                    await DisplayAlert("Уведомление", regInfo.err.ToString(), "ОK");
                                }


                            }
                        }
                        catch (OperationCanceledException) { }
                        catch (Exception) { await DisplayAlert("Уведомление", "Сбой сети", "ОK"); }
                    }


                    Bregf.IsEnabled = true;


                }

            }



            async void loadpicercity(int idcity)
            {
                citymas.Clear();

                pickercity.IsEnabled = false;
                pickercity.Title = "Загрузка";
                var values = new Dictionary<string, string>();
                values.Add("act", "city");
                values.Add("idcity", idcity.ToString());
                values.Add("token", MainPage.tokenF.ToString());

                var content = new FormUrlEncodedContent(values);
                using (var client = new HttpClient())
                {
                    try
                    {
                        var httpResponseMessage = await client.PostAsync(site, content);

                        if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                        {
                            var content1 = await httpResponseMessage.Content.ReadAsStringAsync();
                            //await DisplayAlert("Уведомление", content1.ToString(), "ОK");
                            content1 = content1.Remove(0, 1);
                            JArray o = JArray.Parse(content1);
                            var rateInfo = JsonConvert.DeserializeObject<List<RegInfo>>(o.ToString());


                            if (rateInfo[0].name != null)
                            {
                                foreach (var c in rateInfo)
                                {

                                    pickercity.Items.Add(c.name.ToString());
                                    //await DisplayAlert("Уведомление", c.id.ToString() + " " + c.name.ToString(), "ОK");
                                    citymas.Add(c);
                                }
                            }
                            else
                            {
                                await DisplayAlert("Уведомление", rateInfo[0].err.ToString(), "ОK");
                            }


                        }
                    }
                    catch (OperationCanceledException) { }
                    catch (Exception) { await DisplayAlert("Уведомление", "Сбой сети", "ОK"); }
                    pickercity.Title = "Город";
                    pickercity.IsEnabled = true;
                }
            }

            /////////////////////////////////////////
            async void loadpicerRegion(int idReg)
            {
                regionmas.Clear();
                pickerreg.Title = "Загрузка";
                pickerreg.IsEnabled = false;
                var values = new Dictionary<string, string>();
                values.Add("act", "region");
                values.Add("idreg", idReg.ToString());

                values.Add("token", MainPage.tokenF.ToString());
                var content = new FormUrlEncodedContent(values);
                using (var client = new HttpClient())
                {
                    try
                    {
                        var httpResponseMessage = await client.PostAsync(site, content);

                        if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                        {
                            var content1 = await httpResponseMessage.Content.ReadAsStringAsync();
                            content1 = content1.Remove(0, 1);
                            JArray o = JArray.Parse(content1);
                            var rateInfo = JsonConvert.DeserializeObject<List<RegInfo>>(o.ToString());


                            if (rateInfo[0].name != null)
                            {
                                foreach (var c in rateInfo)
                                {

                                    pickerreg.Items.Add(c.name.ToString());
                                    //await DisplayAlert("Уведомление", c.id.ToString() + " " + c.name.ToString(), "ОK");
                                    regionmas.Add(c);
                                }
                            }
                            else
                            {
                                await DisplayAlert("Уведомление", rateInfo[0].err.ToString(), "ОK");
                            }


                        }
                    }
                    catch (OperationCanceledException) { }
                    catch (Exception) { await DisplayAlert("Уведомление", "Сбой сети", "ОK"); }
                    pickerreg.Title = "Регион";
                    pickerreg.IsEnabled = true;
                }
            }
            //////////////////////////////////////////////////////////

            async void loadpicerCountry()
            {
                countrymas.Clear();
                var values = new Dictionary<string, string>();
                values.Add("act", "country");
                values.Add("token", MainPage.tokenF.ToString());
                var content = new FormUrlEncodedContent(values);

                using (var client = new HttpClient())
                {
                    try
                    {

                        var httpResponseMessage = await client.PostAsync(site, content);

                        if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                        {
                            var content1 = await httpResponseMessage.Content.ReadAsStringAsync();
                            //await DisplayAlert("fff", content1.ToString(), "ok");
                            //edn.Text = content1;
                            content1 = content1.Remove(0, 1);

                            JArray o = JArray.Parse(content1);
                            //JObject o = JObject.Parse(content1);
                            //await DisplayAlert("f", o.ToString(),"ok");

                            var rateInfo = JsonConvert.DeserializeObject<List<RegInfo>>(content1.ToString());


                            if (rateInfo[0].name != null)
                            {
                                foreach (var c in rateInfo)
                                {

                                    picker.Items.Add(c.name.ToString());

                                    countrymas.Add(c);
                                    //await DisplayAlert("", c.id + i.id, "jj" );
                                    //i += 1;

                                }


                            }
                            else
                            {
                                await DisplayAlert("Уведомление", rateInfo[0].err.ToString(), "ОK");
                            }



                        }




                    }
                    catch (OperationCanceledException) { }
                    catch (Exception) { await DisplayAlert("Уведомление", "Сбой сети", "ОK"); }

                }
                picker.IsEnabled = true;
                picker.Title = "Страна";

            }
        }
    }
}