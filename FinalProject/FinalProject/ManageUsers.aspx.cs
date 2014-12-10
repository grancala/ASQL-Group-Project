///FILE : ManageUsers.cs
///PROJECT : ASQL - Group Project
///PROGRAMMER(S) : Nick Whitney, Constantine Grigoriadis, Jim Raithby
///FIRST VERSION : 12/6/2014
///DESCRIPTION : Allows the user to manage uses, create, update and remove
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    /// <summary>
    /// Code behind for ManageUsers.aspx
    /// </summary>
    public partial class ManageUsers : System.Web.UI.Page
    {
        /// <summary>
        /// Ensures config is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // get config
            switch (LoadConfig.Load(Server.MapPath("~/")))
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


        /// <summary>
        /// Attempts to register a user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// Attempts to update a user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// Attempts to remove a user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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