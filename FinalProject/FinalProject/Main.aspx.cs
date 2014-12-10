using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class Main : System.Web.UI.Page
    {
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


        protected void login_Click(object sender, EventArgs e)
        {
            Server.Transfer("Login.aspx", true);
        }


        protected void manage_Click(object sender, EventArgs e)
        {
            Server.Transfer("ManageUsers.aspx", true);
        }


        protected void upload_Click(object sender, EventArgs e)
        {
            Server.Transfer("UpLoad.aspx", true);
        }


        protected void visualize_Click(object sender, EventArgs e)
        {
            Server.Transfer("Visualize.aspx",true);
        }
    }
}