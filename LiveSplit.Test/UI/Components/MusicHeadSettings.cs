using LiveSplit.UI;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace LiveSplit.UI.Components
{
    public partial class MusicHeadSettings : UserControl
    {
        public LayoutMode Mode { get; set; }
        public event Action<float> VolumeChanged;

        public float Volume => (volumeSliderBar?.Maximum > 0) ? volumeSliderBar.Value / (float)volumeSliderBar.Maximum : 1f;
        public MusicHeadSettings()
        {
            InitializeComponent();
            textFilePath.ReadOnly = true;
            textFilePath.Text = "DefaultMusicDirectory";
            // Initialize volume label value
            if (volumeValueLabel != null)
                volumeValueLabel.Text = ((int)(Volume * 100)).ToString() + "%";
        }

        public string MusicDirectory
        {
            get => textFilePath.Text?.Trim();
            set => textFilePath.Text = value ?? string.Empty;
        }


        public XmlNode GetSettings(XmlDocument document)
        {
            XmlElement xmlSettings = document.CreateElement("Settings");
            CreateSettingsNode(document, xmlSettings);
            return xmlSettings;
        }
        public void SetSettings(XmlNode settings)
        {
            var element = (XmlElement)settings;
            textFilePath.Text = SettingsHelper.ParseString(element["Filepath"]);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.0") ^
                SettingsHelper.CreateSetting(document, parent, "Filepath", textFilePath.Text);
        }

        private void MusicHeadSettings_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textFilePath.Text = dialog.SelectedPath;
                }
            }
        }

        private void volumeSliderBar_ValueChanged(object sender, EventArgs e)
        {
            float v = Volume;
            // Update displayed percentage
            if (volumeValueLabel != null)
                volumeValueLabel.Text = ((int)(v * 100)).ToString() + "%";

            VolumeChanged?.Invoke(v);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
