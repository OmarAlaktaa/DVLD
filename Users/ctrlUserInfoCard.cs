using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class ctrlUserInfoCard : UserControl
    {
        public ctrlUserInfoCard()
        {
            InitializeComponent();
        }

        private User _user { get; set; }

        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                LoadUserInfo(value);
            }
        }

        public void LoadUserInfo(int UserID)
        {
            User = User.Find(UserID);
            if (User == null) // user was not found
            {
                MessageBox.Show($"Cannot load user with ID={UserID.ToString()}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else // User found
            {
                LoadUserInfo(User);
            }
        }

        public void LoadUserInfo(User User)
        {
            if (User == null) return;

            Person person = Person.Find(User.PersonID);
            if (person != null)
            {
                PersonInfoCard.Person = person;
            }
            if (User != null)
            {
                lblUserID.Text = User.UserID.ToString();
                lblUsername.Text = User.Username;
                if (User.IsActive)
                {
                    lblIsActive.Text = "Yes";
                }
                else
                {
                    lblIsActive.Text = "No";
                }
            }
        }
    }
}
