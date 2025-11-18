using DVLD;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmPersonDetails : Form
    {
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            this.PersonID = PersonID;
        }

        private int PersonID;

        private void frmPersonDetails_Load(object sender, System.EventArgs e)
        {
            PersonInfoCard.LoadPersonInfo(PersonID);
        }

        public delegate void PersonSavedEventHandler(int PersonID);
        
        public event PersonSavedEventHandler PersonSaved;

        private void PersonInfoCard_OnPersonSaved(int obj)
        {
            PersonSaved?.Invoke(obj);
        }
    }
}
