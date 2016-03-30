<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Messenger.aspx.cs" Inherits="SampleApplication.Messenger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple messenger</title>
    <link rel="stylesheet" href="/Content/bootstrap.css"/>
    <link rel="stylesheet" href="/Content/Site.css"/>
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a href="/" class="navbar-brand">Application name</a>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li><a href="/" class="navbar-brand">Home</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container body-content">
        
        <% if (ViewState["Message"] != null)
           { %>
                <div class="alert alert-success">
                    <%=ViewState["Message"] %>    
                </div>
       <%  } %>

        <form id="form1" name="form1" action="/Messenger.aspx" method="post" runat="server">
            <div class="form-horizontal">
                <br/>
                <h4>Messenger</h4>
                 <hr />
                <div class="form-group">
                    <label class="control-label col-md-2" for="to">Recipient</label>
                    <div class="col-md-10">
                        <input class="form-control" type="text" name="to" />
                    </div>
                </div>
                
                <div class="form-group">
                    <label class="control-label col-md-2" for="to">Message</label>
                    <div class="col-md-10">
                        <textarea class="form-control" name="body" ></textarea>
                    </div>
                </div>
                
                <div class="form-group">
                    <div class="col-md-2">
                        </div>
                    <div class="col-md-10">
                        <input class="form-control btn btn-primary" type="submit" value="Send" />
                    </div>
                </div>
            </div>
        </form>

        <hr />
        <footer>
            <p>&copy; 2016 - My ASP.NET Application</p>
        </footer>
    </div>
</body>
</html>
