using System;
using DVLD_BusinessLayer;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        Detain detain = null;

        private void GenerateDetainApplication()
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            if (Globals.CurrentUser == null) return;
            else lblCreatedBy.Text = Globals.CurrentUser.Username;
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            llShowLicenseInfo.Enabled = false;
            btnDetainLicense.Enabled = false;
            GenerateDetainApplication();
        }

        private void PreventToDetain(int LicenseID, int DetainID)
        {
            lblDetainID.Text = DetainID.ToString();
            MessageBox.Show($"License with ID={LicenseID}, is already detained, you cannot datain it until you release it.", "Already Detained!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            btnDetainLicense.Enabled = false;
        }

        private void InitializeLicenseFound()
        {
            ClearDetain();
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;
        }

        private void ClearDetain()
        {
            detain = null;
            lblDetainID.Text = "[???]";
        }

        private void ClearApplication()
        {
            llShowLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            lblLicenseID.Text = "[???]";
        }

        private void ctrlSearchLicense_LicenseFound(object sender, DVLD_BusinessLayer.License e)
        {
            lblLicenseID.Text = e.LicenseID.ToString();
            InitializeLicenseFound();
            if (e.IsDetained(out int detainID))
            {
                PreventToDetain(e.LicenseID, detainID);
                return;
            }
            btnDetainLicense.Enabled = true;
        }

        private bool ValidToDetain()
        {
            if (tbFineFees.Text.Length == 0)
            {
                MessageBox.Show("Please enter fine fees?", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private bool ConfirmToDetain()
        {
            return MessageBox.Show($"Are you sure you want to detain license with ID={ctrlSearchLicense.license.LicenseID} ?", "Confirm To Detain", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void DetainLicenseProccess()
        {
            if (!ConfirmToDetain()) return;
            if (!ValidToDetain()) return;
            if (Double.TryParse(tbFineFees.Text, out double FineFees))
            {
                detain = ctrlSearchLicense.license.Detain(FineFees);
                if (detain != null) // License Detained
                {
                    ctrlSearchLicense.EditLicenseDetained(); 
                    lblDetainID.Text = detain.DetainID.ToString();
                    btnDetainLicense.Enabled = false;
                    MessageBox.Show($"Licenes with ID={ctrlSearchLicense.license.LicenseID} detained successfully, detainID={detain.DetainID}", "License Detain!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            DetainLicenseProccess();
        }

        private void tbFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validations.ValidateInput.ValidateNumberInput(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlSearchLicense.license == null)
                MessageBox.Show("No license selected to show", "No License!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else
            {
                frmLicenseInfo Form = new frmLicenseInfo(ctrlSearchLicense.license);
                Form.ShowDialog();
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlSearchLicense.license == null)
                MessageBox.Show("No license selected to show", "No License!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            if (ctrlSearchLicense.license.Driver == null) return;
            else
            {
                frmLicenseHistory Form = new frmLicenseHistory(ctrlSearchLicense.license.Driver.PersonID);
                Form.ShowDialog();
            }
        }

        private void ctrlSearchLicense_LicenseNotFound(object sender, EventArgs e)
        {
            ClearDetain();
            ClearApplication();
        }
    }
}
