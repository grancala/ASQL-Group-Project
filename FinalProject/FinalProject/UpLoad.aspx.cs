using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class UpLoad : System.Web.UI.Page
    {
        string username = string.Empty;
        string password = string.Empty;
        bool tablePresent = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            #region common
            // get config
            switch (LoadConfig.Load(Server.MapPath("~/")))
            {
                case -1:
                    status.Text = "Failed Loading Config";
                    break;
                case 0:
                    // not needed
                    break;
                case 1:
                    // success
                    break;
            };

            // get login info
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
            #endregion

            bool successful = false;
            string table = string.Empty;

            LoggedInAs.Text = "Logged in as: " + username;
            
            // check if they have a table
            successful = Database.GetTableName(username, password, out table);
            if (successful)
            {
                // find out contents
                long amountOfContents = 0;
                amountOfContents = Database.TableHasContents(username, password);
                switch (amountOfContents)
                {
                    case -1:
                        Contents.Text = "You have no table";
                        break;
                    case 0:
                        Contents.Text = "You have an empty table";
                        break;
                    default:
                        Contents.Text = "You have a table with " + amountOfContents.ToString() + " rows";
                        break;
                }
                if (amountOfContents >= 0)
                {
                    OverWrite.Enabled = true;
                    tablePresent = true;
                }
                else
                {
                    OverWrite.Enabled = false;
                    tablePresent = false;
                }
            }
            else
            {
                Contents.Text = "You have no table";
                tablePresent = false;
            }
        }


        protected void Upload_Click(object sender, EventArgs e)
        {
            bool fileUploaded = false;
            string saveFileName = string.Empty;

            // check if file uploaded
            if (fileUploader.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fileUploader.FileName);
                    saveFileName = Server.MapPath("~/") + filename;
                    fileUploader.SaveAs(saveFileName);
                    fileUploaded = true;
                }
                catch (Exception ex)
                {
                    fileUploaded = false;
                    status.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }

            if(fileUploaded)
            {
                // username from above
                // password from above
                // tablePresent from above
                bool overwrite = OverWrite.Checked;
                string table = string.Empty;

                // load saveFileName
                FileData file = new FileData();
                if (file.Load(username, saveFileName))
                {
                    // create table
                    if (Database.CreateTable(username, password, overwrite))
                    {
                        // get new table name
                        if (Database.GetTableName(username, password, out table))
                        {
                            if (file.Insert(username, table, tablePresent, overwrite))
                            {
                                status.Text = "OPERATION COMPLETE";
                            }
                            else
                            {
                                status.Text = "Failed inserting data, see log for details";
                            }
                        }
                        else
                        {
                            status.Text = "Failed getting table name, see log for details";
                        }
                    }
                    else
                    {
                        status.Text = "Failed creating table, see log for details";
                    }
                }
                else
                {
                    status.Text = "Failed uploading file, see log for details";
                }
            }
        }

        protected void BackToMain_Click(object sender, EventArgs e)
        {
            Server.Transfer("Main.aspx", true);
        }

        protected void visualization_Click(object sender, EventArgs e)
        {
            // Server.Transfer("Graph.aspx",true);
        }
    }
}
