using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JamshidiProj
{
    public partial class frmMain : Form
    {
        bool isGreen = true;
        bool isConnect = true;
        static bool _continue;
        static SerialPort _serialPort;
        List<ListObject> li = new List<ListObject>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //DialogResult dr = MessageBox.Show("آیا قصد خروج از برنامه را دارید؟","سوال",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2,MessageBoxOptions.RtlReading);
            //if (dr == DialogResult.Yes)
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            DateTime dt = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();

            lblDateTime.Text = pc.GetYear(dt) + "-" + pc.GetMonth(dt).ToString("00") + "-" + pc.GetDayOfMonth(dt).ToString("00") + " - " + " ساعت : " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;


            if (!isConnect)
            {
                lblStatus.BackColor = Color.Red;
                lblStatus.Text = "وضعیت اتصال : قطع";
            }
            else
            {
                lblStatus.Text = "وضعیت اتصال : برقرار";

                if (isGreen)
                {
                    isGreen = false;
                    lblStatus.BackColor = Color.White;
                }
                else
                {
                    lblStatus.BackColor = Color.Chartreuse;
                    isGreen = true;
                }
            }


            // ایجاد فایل CSV 
            // اگر وصل باشد تولید میشود 
            if (isConnect)
            {
                CreateCsvFile();
            }
        }

        private void CreateCsvFile()
        {
            string filePath = Application.StartupPath + "\\SettingData.xml";
            if (!System.IO.File.Exists(filePath))
                return;

            XDocument xdoc = XDocument.Load(filePath);

            li.Add(new ListObject
            {
                DateTime = DateTime.Now,
                RainFall = Convert.ToDecimal(txtRainRate.Text.Trim()),

                humidity1 = Convert.ToDecimal(humidity1.Text.Trim()),
                humidity2 = Convert.ToDecimal(humidity2.Text.Trim()),
                humidity3 = Convert.ToDecimal(humidity3.Text.Trim()),
                humidity4 = Convert.ToDecimal(humidity4.Text.Trim()),

                temperature1 = Convert.ToDecimal(temperature1.Text.Trim()),
                temperature2 = Convert.ToDecimal(temperature2.Text.Trim()),
                temperature3 = Convert.ToDecimal(temperature3.Text.Trim()),
                temperature4 = Convert.ToDecimal(temperature4.Text.Trim()),

                winddirect1 = Convert.ToDecimal(winddirect1.Text.Trim()),
                winddirect2 = Convert.ToDecimal(winddirect2.Text.Trim()),
                winddirect3 = Convert.ToDecimal(winddirect3.Text.Trim()),
                winddirect4 = Convert.ToDecimal(winddirect4.Text.Trim()),

                windspeed1 = Convert.ToDecimal(windspeed1.Text.Trim()),
                windspeed2 = Convert.ToDecimal(windspeed2.Text.Trim()),
                windspeed3 = Convert.ToDecimal(windspeed3.Text.Trim()),
                windspeed4 = Convert.ToDecimal(windspeed4.Text.Trim()),

            });

            li.First().humidity1 = li.First().humidity2 = li.First().humidity3 = li.First().humidity4 = 0;
            li.First().temperature1 = li.First().temperature2 = li.First().temperature3 = li.First().temperature4 = 0;
            li.First().winddirect1 = li.First().winddirect3 = li.First().winddirect3 = li.First().winddirect4 = 0;
            li.First().windspeed1 = li.First().windspeed2 = li.First().windspeed3 = li.First().windspeed4 = 0;

            li.First().humidity1 = li.Sum(item => item.humidity1) - li.First().humidity1;
            li.First().humidity2 = li.Sum(item => item.humidity2) - li.First().humidity2;
            li.First().humidity3 = li.Sum(item => item.humidity3) - li.First().humidity3;
            li.First().humidity4 = li.Sum(item => item.humidity4) - li.First().humidity4;

            li.First().temperature1 = li.Sum(item => item.temperature1) - li.First().temperature1;
            li.First().temperature2 = li.Sum(item => item.temperature2) - li.First().temperature2;
            li.First().temperature3 = li.Sum(item => item.temperature3) - li.First().temperature3;
            li.First().temperature4 = li.Sum(item => item.temperature4) - li.First().temperature4;

            li.First().winddirect1 = li.Sum(item => item.winddirect1) - li.First().winddirect1;
            li.First().winddirect2 = li.Sum(item => item.winddirect2) - li.First().winddirect2;
            li.First().winddirect3 = li.Sum(item => item.winddirect3) - li.First().winddirect3;
            li.First().winddirect4 = li.Sum(item => item.winddirect4) - li.First().winddirect4;

            li.First().windspeed1 = li.Sum(item => item.windspeed1) - li.First().windspeed1;
            li.First().windspeed2 = li.Sum(item => item.windspeed2) - li.First().windspeed2;
            li.First().windspeed3 = li.Sum(item => item.windspeed3) - li.First().windspeed3;
            li.First().windspeed4 = li.Sum(item => item.windspeed4) - li.First().windspeed4;

            try
            {
                var configPersons = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false
                };

                using (var writer = new StreamWriter(xdoc.Descendants("txtExcelAddress").First().Value + xdoc.Descendants("txtExcelFileName").First().Value + ".csv"))
                using (var csv = new CsvWriter(writer, configPersons))
                {
                    csv.WriteRecords(li);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" خطایی در ایجاد فایل رخ داد  ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                return;
            }
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            MessageBox.Show("برنامه هواشناسی  " + Environment.NewLine + " برنامه نویس : ساسان جمالی" + Environment.NewLine + "شماره همراه : 09901456659", "درباره برنامه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        }
        //
        private void frmMain_Load(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "\\SettingData.xml";
            if (!System.IO.File.Exists(filePath))
                return;

            XDocument xdoc = XDocument.Load(filePath);

            //lblTitel_1.Text = xdoc.Descendants("txtTitle1").First().Value;
            //lblTitle2.Text = xdoc.Descendants("txtTitle2").First().Value;
            //lblTitle3.Text = xdoc.Descendants("txtTitle3").First().Value;
            //lblTitle4.Text = xdoc.Descendants("txtTitle4").First().Value;
        }

        private void temperature3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConDisConn_Click(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "\\SettingData.xml";
            if (!System.IO.File.Exists(filePath))
            {
                MessageBox.Show("تنظیمات برنامه برای خواندن اطلاعات را وارد نمایید .", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetStateConnectionApp();

            XDocument xdoc = XDocument.Load(filePath);
            string connectionType = Convert.ToString(xdoc.Descendants("cmbConnectionType").First().Value);
            string bitRate = Convert.ToString(xdoc.Descendants("cmbBitRate").First().Value);
            // اگر پورت سریال بود
            if (connectionType == "Serial Port")
            {

            }

        }


        private void SetStateConnectionApp()
        {
            isConnect = !isConnect;

            if (!isConnect)
            {
                winddirect1.Text = winddirect2.Text = winddirect3.Text = winddirect4.Text = 0.ToString();
                temperature1.Text = temperature2.Text = temperature3.Text = temperature4.Text = 0.ToString();
                humidity1.Text = humidity2.Text = humidity3.Text = humidity4.Text = 0.ToString();
                windspeed1.Text = windspeed2.Text = windspeed3.Text = windspeed4.Text = 0.ToString();
                panel3.Enabled = false;
                txtRainRate.Text = 0.ToString();
            }
            else
            {
                panel3.Enabled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel20_Paint(object sender, PaintEventArgs e)
        {

        }

    }


}

