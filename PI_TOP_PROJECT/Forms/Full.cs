using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace PI_TOP_PROJECT.Forms
{
    public partial class Full : Form
    {
        private OpenFileDialog OPF;
        private DocX doc;
        public Full()
        {
            InitializeComponent();
            OPF = new OpenFileDialog();
            OPF.Filter = "Word files(*.doc, *.docx)|*.doc;*.docx";
        }

        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(IconButton))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;

                }
            }

        }

        private void Full_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                doc = DocX.Load(OPF.FileName);
                MessageBox.Show("Файл добавлен!", OPF.FileName);
                filename.Text = OPF.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Запуск!", OPF.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void filename_Click(object sender, EventArgs e)
        {

        }
    }
}
