using System;
using System.Windows.Forms;
using DVLD_BusinessLayer;
using System.Data;

namespace DVLD
{
    public partial class frmManageDetainedLicenses : Form
    {
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _LoadDetainedLicenses()
        {
            dgvDetainedLicenses.DataSource = License.GetDetainedLicenses();
        }

        private void _LoadFilters()
        {
            cbFilters.DataSource = Enum.GetValues(typeof(SearchFilter.ListDetainedLicensesFilter));
        }

        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            _LoadDetainedLicenses();
            _LoadFilters();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense Form = new frmReleaseDetainedLicense();
            Form.ShowDialog();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense Form = new frmDetainLicense();
            Form.ShowDialog();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilters.SelectedIndex == (int)SearchFilter.ListDetainedLicensesFilter.None)
            {
                dgvDetainedLicenses.DataSource = License.GetDetainedLicenses();
                txtFilter.Visible = false;
                pnlReleased.Visible = false;
            }
            else
            {
                if (cbFilters.SelectedIndex == (int)SearchFilter.ListDetainedLicensesFilter.IsReleased)
                {
                    txtFilter.Visible = false;
                    pnlReleased.Visible = true;
                }
                else
                {
                    txtFilter.Visible = true;
                    pnlReleased.Visible = false;
                }
            }

            txtFilter.Focus();
        }

        private void _FilterResult()
        {
            switch ((SearchFilter.ListDetainedLicensesFilter)cbFilters.SelectedIndex)
            {
                case SearchFilter.ListDetainedLicensesFilter.DetainID:
                    {
                        dgvDetainedLicenses.DataSource = License.GetDetainedLicensesFilterBy(SearchFilter.ListDetainedLicensesFilter.DetainID, txtFilter.Text);
                        break;
                    }
                case SearchFilter.ListDetainedLicensesFilter.IsReleased:
                    {
                        dgvDetainedLicenses.DataSource = License.GetDetainedLicensesFilterBy(SearchFilter.ListDetainedLicensesFilter.IsReleased, rbReleased.Checked.ToString());
                        break;
                    }
                case SearchFilter.ListDetainedLicensesFilter.FullName:
                    {
                        dgvDetainedLicenses.DataSource = License.GetDetainedLicensesFilterBy(SearchFilter.ListDetainedLicensesFilter.FullName, txtFilter.Text);
                        break;
                    }
                case SearchFilter.ListDetainedLicensesFilter.NationalNo:
                    {
                        dgvDetainedLicenses.DataSource = License.GetDetainedLicensesFilterBy(SearchFilter.ListDetainedLicensesFilter.NationalNo, txtFilter.Text);
                        break;
                    }
                case SearchFilter.ListDetainedLicensesFilter.ReleaseApplicationID:
                    {
                        dgvDetainedLicenses.DataSource = License.GetDetainedLicensesFilterBy(SearchFilter.ListDetainedLicensesFilter.ReleaseApplicationID, txtFilter.Text);
                        break;
                    }
            }
        
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                dgvDetainedLicenses.DataSource = License.GetDetainedLicenses();
            }
            else
                _FilterResult();
        }

        private void rbReleased_CheckedChanged(object sender, EventArgs e)
        {
            _FilterResult();
        }

        private void rbNotReleased_CheckedChanged(object sender, EventArgs e)
        {
            _FilterResult();
        }

        private void dgvDetainedLicenses_DataSourceChanged(object sender, EventArgs e)
        {
            lblNumberOfDetainedLicenses.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }
    }
}
