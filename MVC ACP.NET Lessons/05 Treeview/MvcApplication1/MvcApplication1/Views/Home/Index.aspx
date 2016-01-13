<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="MvcApplication1.Helpers" %> 
<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <% Html.RenderNavMenu(); %>
    <h1>Сейшен !</h1>
    <%= ViewData["greeting"] %>! Для участия в вечеринке, ты должен сообщить о себе.
    <%= Html.ActionLink("Перейти к регистрации", "RegForm") %>
</body>
</html>
