///FILE : Login.aspx.cs
///PROJECT : ASQL - Group Project
///PROGRAMMER(S) : Nick Whitney, Constantine Grigoriadis, Jim Raithby
///FIRST VERSION : 12/6/2014
///DESCRIPTION : Code behind for the login.aspx
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    /// <summary>
    /// Code behind for login.aspx
    /// </summary>
    public partial class Login : System.Web.UI.Page
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
        /// Attempts to log in a user, redirects on pass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Login_Click(object sender, EventArgs e)
        {
            string username = txtUserLogin.Text;
            string password = txtPassLogin.Text;
            bool loggedIn = false;
            loggedIn = Database.LoginUser(username,password);
            if(loggedIn)
            {
                Session["username"] = username;
                Session["password"] = password;
                Server.Transfer("Main.aspx", true);
            }
            else
            {
                Error.Text = "Failed Login, username or password is Incorrect";
            }
        }


        /// <summary>
        /// Registers a user, provides feedback at bottom of page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Register_Click(object sender, EventArgs e)
        {
            string username = txtUserReg.Text;
            bool created = false;
            created = Database.CreateUser(username);
            if(created)
            {
                Error.Text = "Created user";
            }
            else
            {
                Error.Text = "Error creating user";
            }
        }

    }
}