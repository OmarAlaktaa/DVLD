using System.Windows.Forms;
using DVLD_BusinessLayer;
using System;

namespace DVLD
{
    public partial class frmUpdateApplicationType : Form
    {
        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            LoadApplicationType(ApplicationTypeID);
        }

        ApplicationType AppType;

        public delegate void ApplicationUpdatedEventHandler();

        public event ApplicationUpdatedEventHandler ApplicationUpdated;

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            SaveUpdate();
        }

        private void SaveUpdate()
        {
            AppType.ApplicationTypeTitle = tbTitle.Text;
            AppType.ApplicationTypeFees = Convert.ToDouble(tbFees.Text);
            if (AppType.Save())
            {
                MessageBox.Show("Application Type Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ApplicationUpdated?.Invoke();
            }
            else
            {
                MessageBox.Show("Error While saving data!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadApplicationType(int ApplicationTypeID)
        {
            AppType = new ApplicationType(ApplicationTypeID);
            lblID.Text = AppType.ApplicationTypeID.ToString();
            tbTitle.Text = AppType.ApplicationTypeTitle;
            tbFees.Text = AppType.ApplicationTypeFees.ToString();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void tbFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validations.ValidateInput.ValidateNumberInput(e);
        }
    }
}
