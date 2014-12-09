using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // get config
            switch (LoadConfig.Load())
            {
                case -1:
                    Error.Text = "Failed Loading Config";
                    break;
                case 0:
                    // not needed
                    break;
                case 1:
                    // success
                    break;
            };
        }


        protected void Register_Click(object sender, EventArgs e)
        {
            string username = txtUserReg.Text;
            bool created = false;
            created = Database.CreateUser(username);
            if (created)
            {
                Error.Text = "Created user";
            }
            else
            {
                Error.Text = "Error creating user";
            }
        }


        protected void Update_Click(object sender, EventArgs e)
        {
            string username = txtUserUpdate.Text;
            string oldPass = txtOldUpdate.Text;
            string newPass = txtNewUpdate.Text;
            bool updated = false;
            updated = Database.UpdateUser(username, oldPass, newPass);
            if (updated)
            {
                Error.Text = "Updated user";
            }
            else
            {
                Error.Text = "Failed updating user";
            }
        }


        protected void Remove_Click(object sender, EventArgs e)
        {
            string username = txtUserRem.Text;
            string password = txtPassRem.Text;
            bool removed = false;
            removed = Database.DropUser(username, password);
            if (removed)
            {
                Error.Text = "Removed user";
            }
            else
            {
                Error.Text = "Error removing user";
            }
        }
    }
}