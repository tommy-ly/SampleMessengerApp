using System;
using SampleApplication.Messaging;

namespace SampleApplication
{
    public partial class Messenger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Account/Login?returnUrl=/messenger.aspx");
            }

            if (IsPostBack)
            {
                new SendMessageService().SendMessage(User.Identity.Name, Request.Form["to"], Request.Form["body"]);
                ViewState.Add("Message", "Message sent");
            }
        }
    }
}