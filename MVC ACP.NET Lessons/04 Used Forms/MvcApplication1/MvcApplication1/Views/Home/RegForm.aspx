<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>RegistrationForm</title>
</head>
<body>
    <%= Html.ValidationSummary() %>
    <h1>Регистрация</h1>
    <% using (Html.BeginForm()) { %>
    Твое имя: <%= Html.TextBox("Name") %>
    Твое мыло: <%= Html.TextBox("Email")%>
    Твой телефон: <%= Html.TextBox("Phone")%>

        Будет весело. Ты решился ?
        <%= Html.DropDownList("WillAttend", new[] {
        new SelectListItem { Text = "Да, я готов",
        Value = bool.TrueString },
        new SelectListItem { Text = "Наверное нет",
        Value = bool.FalseString }
        }, "Выбирай") %>

    <input type="submit" value="Зарегиться" />

    <% } %>

</body>
</html>
