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
    public partial class Fast : Form
    {
        int pageCount = 0;
        int error = 0;
        int minStr = 50;
        
        private OpenFileDialog OPF;
        private DocX doc;

        public Fast()
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

        private void Fast_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                error = 0;
                doc = DocX.Load(OPF.FileName);
                MessageBox.Show("Файл добавлен!", OPF.FileName);
                filename.Text = OPF.FileName;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            foreach (var item in checkedListBox1.CheckedItems) {
                if (item == "Проверка структуры ВКР")
                {
                    MessageBox.Show("Функция 1 работает!");
                }
                else if (item == "Проверка объёма работы")
                {
                    pageCount = doc.GetPageCount();
                    MessageBox.Show("Кол-во страниц в документе:" + pageCount);
                    if (pageCount < minStr)
                        ++error;
                    label1.Text += error;
                }
                else if (item == "Проверка текста")
                {
                    MessageBox.Show("Функция 3 работает!");
                }
                else if (item == "Проверка таблиц/изображений")
                {
                    MessageBox.Show("Функция 4 работает!");
                }
                else if (item == "Проверка оформления приложений")
                {
                    MessageBox.Show("Функция 5 работает!");
                }
                else if (item == "Проверка оформления ссылок")
                {
                    MessageBox.Show("Функция 6 работает!");
                }
            }
        }
    }
}
