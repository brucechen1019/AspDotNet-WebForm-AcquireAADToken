using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormApplication
{
    public partial class Default : System.Web.UI.Page
    {   
        static string tentantID=ConfigurationManager.AppSettings["tenantID"];
        static string clientId = ConfigurationManager.AppSettings["clientId"];
        static string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Async
        protected void btnLoginAsync_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "starting async";
            RegisterAsyncTask(new PageAsyncTask(AuthorizeAsync));
        }

        private async Task AuthorizeAsync()
        {
            string token = string.Empty;
            var context = new AuthenticationContext("https://login.microsoftonline.com/" + tentantID, false);
            try
            {
                ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);
                var result = context.AcquireTokenAsync("https://management.core.windows.net/", clientCredential).ConfigureAwait(false);

                AuthenticationResult resVal = await result;
                token = resVal.AccessToken;
            }
            catch (AdalException ae)
            {
                //error code
                token = ae.InnerException.Message;
            }
            lblStatus.Text = token;
        }
        #endregion


        #region Sync
        protected void btnLoginSync_Click(object sender, EventArgs e)
        {
            lblStatus1.Text = "starting sync";
            lblStatus1.Text = AuthorizeSync();
        }

        private string AuthorizeSync()
        {
            string token = string.Empty;
            var context = new AuthenticationContext("https://login.microsoftonline.com/" + tentantID, false);
            ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);

            //AuthenticationResult result = null;
            //var thread = new Thread(() =>
            //{
            //    try
            //    {
            //        result = context.AcquireTokenAsync("https://management.core.windows.net/", clientCredential).Result;
            //    }
            //    catch (Exception ex)
            //    {
            //        token = ex.InnerException.Message;
            //    }
            //});

            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Name = "AquireTokenThread";
            //thread.Start();
            //thread.Join();

            //if (result == null)
            //{
            //    token = "Failed to obtain the JWT token";
            //}
            //else
            //    token = result.AccessToken;


            try
            {
                token = context.AcquireTokenAsync("https://management.core.windows.net/", clientCredential).Result.AccessToken;
            }
            catch (Exception ex)
            {
                token = ex.InnerException.Message;
            }
            return token;
        } 
        #endregion
    }
}