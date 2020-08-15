<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <h1>Сейшен !</h1>
    <%= ViewData["greeting"] %>! Для участия в вечеринке, ты должен сообщить о себе.
    <%= Html.ActionLink("Перейти к регистрации", "RegForm") %>
</body>
</html>
