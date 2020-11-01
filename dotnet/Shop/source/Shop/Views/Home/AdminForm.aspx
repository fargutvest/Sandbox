<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>AdminForm</title>
</head>
<body>
    <%= Html.ValidationSummary() %>
    <h1>Админка</h1>
    <% using (Html.BeginForm()) { %>
    Название товара: <%= Html.TextBox("Name") %>
    Количество: <%= Html.TextBox("Count")%>
    Штрих код: <%= Html.TextBox("Barcode")%>
    <% } %>

</body>
</html>
