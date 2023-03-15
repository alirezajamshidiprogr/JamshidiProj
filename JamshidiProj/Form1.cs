using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JamshidiProj
{
    public partial class frmMain : Form
    {
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

            lblDateTime.Text =  pc.GetYear(dt) + "-" + pc.GetMonth(dt).ToString("00") + "-" + pc.GetDayOfMonth(dt).ToString("00") + " - " +  " ساعت : " + dt.Hour + ":" + dt.Minute + ":" + dt.Second; 
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            MessageBox.Show("برنامه هواشناسی  "  +Environment.NewLine + " برنامه نویس : ساسان جمالی" + Environment.NewLine + "شماره همراه : 09901456659", "درباره برنامه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "\\SettingData.xml";
            if (!System.IO.File.Exists(filePath))
                return;

            XDocument xdoc = XDocument.Load(filePath);

            lblTitel_1.Text = xdoc.Descendants("txtTitle1").First().Value;
            lblTitle2.Text = xdoc.Descendants("txtTitle2").First().Value;
            lblTitle3.Text = xdoc.Descendants("txtTitle3").First().Value;
            lblTitle4.Text = xdoc.Descendants("txtTitle4").First().Value;
        }
    }
}
