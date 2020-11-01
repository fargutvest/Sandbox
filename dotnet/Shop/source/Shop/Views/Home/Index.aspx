<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <h1 align="center">Магазин</h1>

    <div align="center"><%= ViewData["greeting"] %>! Добро пожаловать в наш магазин!.</div>
    <br />
    <div align="center"><%= Html.ActionLink("Перейти в админку", "AdminForm") %></div>
</body>
</html>
