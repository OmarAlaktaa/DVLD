using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmDriversList : Form
    {
        public frmDriversList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxFilterVisiblity()
        {
            if ((SearchFilter.ManageDriversFilters)cbFilter.SelectedIndex
                == SearchFilter.ManageDriversFilters.None)
            {
                tbFilter.Visible = false;
            }
            else
            {
                tbFilter.Visible = true;
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxFilterVisiblity();
            tbFilter.Focus();
        }

        private void frmDriversList_Load(object sender, EventArgs e)
        {
            dgvDrivers.DataSource = Driver.GetAllDrivers();
            lblRecords.Text = dgvDrivers.Rows.Count.ToString();
            cbFilter.DataSource = Enum.GetValues(typeof(SearchFilter.ManageDriversFilters));
        }


        private void FilterProccess()
        {
            if (string.IsNullOrEmpty(tbFilter.Text))
            {
                dgvDrivers.DataSource = Driver.GetAllDrivers();
                lblRecords.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }
            switch((SearchFilter.ManageDriversFilters)cbFilter.SelectedIndex)
            {
                case SearchFilter.ManageDriversFilters.None:
                    {
                        dgvDrivers.DataSource = Driver.GetAllDrivers();
                        break;
                    }
                case SearchFilter.ManageDriversFilters.DriverID:
                    {
                        int DriverID = Convert.ToInt32(tbFilter.Text);
                        dgvDrivers.DataSource = Driver.GetAllDriversWithDriverIDFilter(DriverID);
                        break;
                    }
                case SearchFilter.ManageDriversFilters.PersonID:
                    {
                        int PersonID = Convert.ToInt32(tbFilter.Text);
                        dgvDrivers.DataSource = Driver.GetAllDriversWithPersonIDFilter(PersonID);
                        break;
                    }
                case SearchFilter.ManageDriversFilters.NationalNo:
                    {
                        string NationalNo = tbFilter.Text;
                        dgvDrivers.DataSource = Driver.GetAllDriversWithNationalNoFilter(NationalNo);
                        break;
                    }
                case SearchFilter.ManageDriversFilters.FullName:
                    {
                        string Name = tbFilter.Text;
                        dgvDrivers.DataSource = Driver.GetAllDriversWithFullNameFilter(Name);
                        break;
                    }
                default:
                    {
                        dgvDrivers.DataSource = Driver.GetAllDrivers();
                        break;
                    }
            }
            lblRecords.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            FilterProccess();
        }
    }
}
