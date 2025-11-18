using System;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmUpdateTestType : Form
    {
        public frmUpdateTestType(int TestTypeID)
        {
            InitializeComponent();
            LoadTestType(TestTypeID);
        }

        TestType test;

        public delegate void TestUpdatedEventHandler();
        public event TestUpdatedEventHandler TestUpdated;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadTestType(int TestTypeID)
        {
            test = new TestType(TestTypeID);
            lblID.Text = TestTypeID.ToString();
            tbTitle.Text = test.Title;
            tbDescription.Text = test.Description;
            tbFees.Text = test.Fees.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveUpdate();
        }

        private void SaveUpdate()
        {
            test.Title = tbTitle.Text;
            test.Fees = Convert.ToDouble(tbFees.Text);
            if (test.Save())
            {
                MessageBox.Show("Application Type Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TestUpdated?.Invoke();
            }
            else
            {
                MessageBox.Show("Error While saving data!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validations.ValidateInput.ValidateNumberInput(e);
        }
    }
}
