using System;
using DVLD_BusinessLayer;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmApplicationDetails : Form
    {
        public frmApplicationDetails(DVLD_BusinessLayer.Application App)
        {
            InitializeComponent();
            ApplicationInfo.application = App;
        }

        public frmApplicationDetails(int ApplicationID)
        {
            InitializeComponent();
            ApplicationInfo.application = DVLD_BusinessLayer.Application.GetApplication(ApplicationID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
