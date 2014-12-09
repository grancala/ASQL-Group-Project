using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class Login : System.Web.UI.Page
    {
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