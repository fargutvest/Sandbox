<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcApplication1.Models.GuestResponseModel>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Thanks</title>
</head>
<body>
    <h1>Привет, <%= Html.Encode(Model.Name) %>!</h1>
    <% if (Model.WillAttend == true){ %>
        Отлично, что ты придешь !
        <% } else { %>
        Очень жаль.
    <% } %>
</body>
</html>
