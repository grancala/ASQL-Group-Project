///FILE : Main.cs
///PROJECT : ASQL - Group Project
///PROGRAMMER(S) : Nick Whitney, Constantine Grigoriadis, Jim Raithby
///FIRST VERSION : 12/6/2014
///DESCRIPTION : Contains the code behind for the main menu page
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    /// <summary>
    /// Code behind for Main.aspx
    /// </summary>
    public partial class Main : System.Web.UI.Page
    {
        /// <summary>
        /// Ensures config is loaded and user is logged in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region common
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

            // get login info
            string username = "";
            string password = "";
            if (Session["username"] != null)
            {
                username = (string)Session["username"];
            }
            if (Session["password"] != null)
            {
                password = (string)Session["password"];
            }
            if (username == "" || password == "")
            {
                // not logged in
                Server.Transfer("Login.aspx", true);
            }
            LoggedInAs.Text = "WELCOME: " + username;
            
            #endregion
        }

        /// <summary>
        /// Redirects to user management
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void manage_Click(object sender, EventArgs e)
        {
            Server.Transfer("ManageUsers.aspx", true);
        }


        /// <summary>
        /// Redirects to uploading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void upload_Click(object sender, EventArgs e)
        {
            Server.Transfer("UpLoad.aspx", true);
        }


        /// <summary>
        /// Redirects to charting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void visualize_Click(object sender, EventArgs e)
        {
            Server.Transfer("Visualize.aspx",true);
        }
    }
}