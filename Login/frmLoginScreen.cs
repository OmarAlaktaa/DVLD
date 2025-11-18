using System;
using System.Drawing;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool isDragging = false;
        private Point dragStartPoint;
        private void frmLoginScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location;
            }
        }
        
        private void frmLoginScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point screenPos = PointToScreen(e.Location);
                this.Location = new Point(screenPos.X - dragStartPoint.X, screenPos.Y - dragStartPoint.Y);
            }
        }

        private void frmLoginScreen_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login(tbUsername.Text, tbPassword.Text, chkRememberMe.Checked);
        }

        private void Login(string Username, string Password, bool RememberMe)
        {
            if (Users.Login(Username, Password, RememberMe))
            {
                frmMain Form = new frmMain(this);
                Form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username, Password!", "Wrong Cardinailts!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            if (Users.RememberUser)
            {
                User User = User.GetLastUserSignedIn();
                if (User != null)
                {
                    tbUsername.Text = User.Username;
                    tbPassword.Text = User.Password;
                    chkRememberMe.Checked = true;
                }
            }
        }

    }
}
