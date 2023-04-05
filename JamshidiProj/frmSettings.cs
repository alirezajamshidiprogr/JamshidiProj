using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace JamshidiProj
{
    public partial class frmSettings : Form
    {

        public frmSettings()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "\\SettingData.xml";

            if (string.IsNullOrEmpty(txtPort.Text.Trim()) || string.IsNullOrEmpty(txtIP.Text.Trim()) || cmbScheduleRaining.SelectedIndex == -1 || cmbPortCom.SelectedIndex == -1 || cmbConnectionType.SelectedIndex == -1 || cmbBitRate.SelectedIndex == -1)
            {
                MessageBox.Show(" یک یا برخی از فیلدهای اجباری وارد نشده است ! ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);


            using (XmlWriter writer = XmlWriter.Create("SettingData.xml"))
            {
                writer.WriteStartElement("Setting");//ddd
                writer.WriteElementString("txtSaveTime", txtSaveTime.Text.Trim());
                writer.WriteElementString("txtExcelFileName", txtExcelFileName.Text.Trim());
                writer.WriteElementString("txtExcelAddress", txtExcelAddress.Text.Trim());
                writer.WriteElementString("txtBackup1", txtBackup1.Text.Trim());
                writer.WriteElementString("txtBackup2", txtBackup2.Text.Trim());
                writer.WriteElementString("chbAutoConnect", chbAutoConnect.Checked ? "true" : "false");
                writer.WriteElementString("txtTitle1", txtTitle1.Text.Trim());
                writer.WriteElementString("txtTitle2", txtTitle2.Text.Trim());
                writer.WriteElementString("txtTitle3", txtTitle3.Text.Trim());
                writer.WriteElementString("txtTitle4", txtTitle4.Text.Trim());
                writer.WriteElementString("txtIP", txtIP.Text.Trim());
                writer.WriteElementString("txtPort", txtPort.Text.Trim());
                writer.WriteElementString("cmbScheduleRaining", cmbScheduleRaining.Text);
                writer.WriteElementString("cmbConnectionType", cmbConnectionType.Text);
                writer.WriteElementString("cmbPortCom", cmbPortCom.Text);
                writer.WriteElementString("cmbBitRate", cmbBitRate.Text);
                writer.WriteEndElement();
                writer.Flush();
            }


            MessageBox.Show(" تغییرات با موفقیت اعمال گردید. ! ", "اطلاع", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            this.Close();

        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "\\SettingData.xml";
            if (!System.IO.File.Exists(filePath))
                return;

            XDocument xdoc = XDocument.Load(filePath);

            txtSaveTime.Text = xdoc.Descendants("txtSaveTime").First().Value;
            txtExcelFileName.Text = xdoc.Descendants("txtExcelFileName").First().Value;
            txtExcelAddress.Text = xdoc.Descendants("txtExcelAddress").First().Value;
            txtBackup1.Text = xdoc.Descendants("txtBackup1").First().Value;
            txtBackup2.Text = xdoc.Descendants("txtBackup2").First().Value;
            chbAutoConnect.Checked = xdoc.Descendants("chbAutoConnect").First().Value == "true" ? true : false;
            txtTitle1.Text = xdoc.Descendants("txtTitle1").First().Value;
            txtTitle2.Text = xdoc.Descendants("txtTitle2").First().Value;
            txtTitle3.Text = xdoc.Descendants("txtTitle3").First().Value;
            txtTitle4.Text = xdoc.Descendants("txtTitle4").First().Value;
            txtIP.Text = xdoc.Descendants("txtIP").First().Value;
            txtPort.Text = xdoc.Descendants("txtPort").First().Value;
            cmbScheduleRaining.Text = Convert.ToString(xdoc.Descendants("cmbScheduleRaining").First().Value);
            cmbConnectionType.Text = Convert.ToString(xdoc.Descendants("cmbConnectionType").First().Value);
            cmbPortCom.Text = Convert.ToString(xdoc.Descendants("cmbPortCom").First().Value);
            cmbBitRate.Text = Convert.ToString(xdoc.Descendants("cmbBitRate").First().Value);
        }

        private void btnSelectExcel_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
               txtExcelAddress.Text = folderBrowserDialog1.SelectedPath.ToString();
            }
        }

        private void btnBackupFile_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtBackup1.Text = folderBrowserDialog1.SelectedPath.ToString();
            }
        }
    }
}
