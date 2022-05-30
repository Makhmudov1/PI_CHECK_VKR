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
        int pageCount = 0;
        int error = 0;
        int minStr = 50;

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
                if (OPF.FileName.EndsWith(".doc") == true || OPF.FileName.EndsWith(".docx") == true)
                {
                    error = 0;
                    doc = DocX.Load(OPF.FileName);
                    MessageBox.Show("Файл добавлен!", OPF.FileName);
                    filename.Text = OPF.FileName;
                }
                else
                {
                    MessageBox.Show("Ошибка! Файл не соответствует формату.\nПоменяйте формат файла и попробуйте снова.",
                        "Неправильный формат файла!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private static bool CheckStructure(DocX doc)
        {
            bool containsIntroduction = false;
            bool containsChapters = false;
            bool containsConclusion = false;
            bool containsLiterature = false;
            bool containsApplication = false;

            foreach (var paragraph in doc.Paragraphs)
            {
                if (paragraph.StyleId == "1")
                {
                    foreach (var text in paragraph.MagicText)
                    {
                        if (text.formatting?.FontFamily?.Name == "Times New Roman" && text.formatting?.Size == 14)
                        {
                            if (paragraph.Text.ToLower().Contains("введение"))
                                containsIntroduction = true;

                            if (paragraph.Text.ToLower().Contains("глава"))
                                containsChapters = true;

                            if (paragraph.Text.ToLower().Contains("заключение"))
                                containsConclusion = true;

                            if (paragraph.Text.ToLower().Contains("список литературы"))
                                containsLiterature = true;

                            if (paragraph.Text.ToLower().Contains("приложение"))
                                containsApplication = true;
                        }
                    }
                }
            }

            return containsIntroduction && containsChapters && containsConclusion && containsLiterature && containsApplication;
        }



        private void button3_Click(object sender, EventArgs e)
        {
            pageCount = doc.GetPageCount();
            MessageBox.Show("Кол-во страниц в документе:" + pageCount);
            if (pageCount < minStr)
                ++error;

            if (CheckStructure(doc) == true)
            {
                MessageBox.Show("Структура есть!");
            }
            else
            {
                MessageBox.Show("Структуры нет!");
                ++error;
            }
            label1.Text += error;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void filename_Click(object sender, EventArgs e)
        {

        }
    }
}
