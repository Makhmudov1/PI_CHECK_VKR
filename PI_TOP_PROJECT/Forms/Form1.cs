using FontAwesome.Sharp;
using PI_TOP_PROJECT.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace PI_TOP_PROJECT
{
    public partial class Form1 : Form
    {
        //fields
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;

        private int borderSize = 2;
        public Form1()
        {
            InitializeComponent();
            random = new Random();

            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(98,102,244);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd,int wMsg, int wParam,int lParam);

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cb_full_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            base.WndProc(ref m);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Color SelectThemeColor() 
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index) {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }


        private void ActivateButton(object btnSender) {
            if (btnSender != null) {
                if (currentButton != (IconButton)btnSender) {

                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (IconButton)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color,-0.3);
                    panelLogo_logo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    
                }
            }
        }

        private void DisableButton() {
            foreach (Control previousBtn in panelMenu.Controls) {
                if (previousBtn.GetType() == typeof(IconButton)) {
                    previousBtn.BackColor = Color.FromArgb(98, 102, 244);
                    previousBtn.ForeColor = Color.White;
                    previousBtn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender) {
            if (activeForm != null) {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;

        }



        

        private void iconButton2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Main(),sender);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Full(), sender);
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Fast(), sender);
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new About(), sender);
        }
    }
}
