using Dapper;
using Assist_WebConfig.Data;
using Assist_WebConfig.Helpers;
using Assist_WebConfig.Models;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Assist_WebConfig
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMsg.Visible = false;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.MainDB))
            {
                string email = txtUserName.Text.Trim();
                string passEncrypted = Encrypt.GetSHA256(txtPassword.Text.Trim());

                UserModel user = null;

                if (email == null || passEncrypted == null)
                {
                    lblErrorMsg.Visible = true;
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@email", email);
                    param.Add("@pass", passEncrypted);

                    user = DapperORM.ReturnList<UserModel>("WebGetAuthUser", param).FirstOrDefault<UserModel>();

                    if (user != null)
                    {
                        Session["username"] = txtUserName.Text.Trim();
                        Session["userType"] = user.Name;
                        Session["userId"] = user.UserId;

                        Response.Redirect("/AssistWebConfig/home/Index");
                    }
                    else
                    {
                        lblErrorMsg.Visible = true;
                    }
                }
            }
        }
        protected void linkForgetPass_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AssistWebConfig/User/StartRecovery");
        }
    }
}