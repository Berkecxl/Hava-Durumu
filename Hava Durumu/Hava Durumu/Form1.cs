using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hava_Durumu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void Weather()
        {
            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("Lütfen bir lokasyon giriniz!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try 
                {
                    Form1 frm = new Form1();

                    string htmlCode = "";

                    using (WebClient client = new WebClient())
                    {
                        client.Encoding = Encoding.UTF8;
                        htmlCode = client.DownloadString($"http://api.openweathermap.org/data/2.5/weather?q={guna2TextBox1.Text}&appid=73738642c438323a57db3214949e8502&lang=tr");
                    }

                    dynamic stuff = JObject.Parse(htmlCode);

                    dynamic sicaklik = stuff.main.temp;
                    int verisicaklik = sicaklik - 273;
                    guna2HtmlLabel1.Text = verisicaklik.ToString() + " °C";
                    guna2HtmlLabel1.Location = new Point(frm.Size.Width / 2 - guna2HtmlLabel1.Size.Width / 2 - 10, 134);

                    dynamic picture = stuff.weather[0].icon;
                    guna2PictureBox1.Load($"http://openweathermap.org/img/w/{picture}.png");

                    dynamic status = stuff.weather[0].description;
                    guna2HtmlLabel1.Location = new Point(frm.Size.Width / 2 - guna2HtmlLabel1.Size.Width / 2 - 10, 189);
                    guna2HtmlLabel2.Text = status.ToString();

                    dynamic country = stuff.sys.country;
                    dynamic name = stuff.name;
                    guna2HtmlLabel3.Text = $"{name}, {country}";
                    guna2HtmlLabel3.Location = new Point(frm.Size.Width / 2 - guna2HtmlLabel1.Size.Width / 2 - 10, 345);

                    guna2TextBox1.Text = "";
                }
                catch (WebException)
                {
                    MessageBox.Show("Böyle bir lokasyon bulunamadı!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Thread islem = new Thread(() => Weather());
            islem.Start();
        }
    }
}
