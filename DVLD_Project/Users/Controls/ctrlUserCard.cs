using BusinessLayer;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Users.Controls
{
    public partial class ctrlUserCard : UserControl
    {
        private int _UserID = -1;
        private clsUsers _User;

        public int UserID { get { return _UserID; } }

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private void _ResetUserInfo()
        {
            //ctrlPersonCard1.ResetPersonInfoCard();
            _UserID = -1;
            lblUserID.Text = "[????]";
            lblUserName.Text = "[????]";
            lblIsActive.Text = "[????]";
        }

        private void _FillUserInfo()
        {
            _UserID = _User.UserID;

            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _UserID.ToString();
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = _User.IsActive ? "Yes" : "No";
        }

        public void LoadUserInfo(int UserID)
        {
            _User = clsUsers.FindByUserID(UserID);
            if (_User == null)
            {
                _ResetUserInfo();
                MessageBox.Show("No User with UserID : " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();
        }

        /// <summary>
        /// Loads user information by username. 
        /// NOTE: Kept for future scalability (e.g., Search functionality or profile management).
        /// </summary>
        public void LoadUserInfo(string UserName)
        {
            _User = clsUsers.FindByUsername(UserName);
            if (_User == null)
            {
                _ResetUserInfo();
                MessageBox.Show("No User with UserName : " + UserName.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();
        }
    }
}
