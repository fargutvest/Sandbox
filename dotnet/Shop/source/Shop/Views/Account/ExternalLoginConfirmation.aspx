<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Shop.Models.RegisterExternalLoginModel>" %>

<asp:Content ID="externalLoginConfirmationTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Регистрация
</asp:Content>

<asp:Content ID="externalLoginConfirmationContent" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Регистрация.</h1>
        <h2>Свяжите свою учетную запись в <%: ViewBag.ProviderDisplayName %>.</h2>
    </hgroup>

    <% using (Html.BeginForm("ExternalLoginConfirmation", "Account", new { ReturnUrl = ViewBag.ReturnUrl })) { %>
        <%: Html.AntiForgeryToken() %>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Форма связывания</legend>
            <p>
                Вы успешно прошли проверку через <strong><%: ViewBag.ProviderDisplayName %></strong>.
                Введите имя пользователя на этом сайте ниже и нажмите кнопку "Подтвердить", чтобы
                завершить вход.
            </p>
            <ol>
                <li class="name">
                    <%: Html.LabelFor(m => m.UserName) %>
                    <%: Html.TextBoxFor(m => m.UserName) %>
                    <%: Html.ValidationMessageFor(m => m.UserName) %>
                </li>
            </ol>
            <%: Html.HiddenFor(m => m.ExternalLoginData) %>
            <input type="submit" value="Регистрация" />
        </fieldset>
    <% } %>
</asp:Content>

<asp:Content ID="scriptsContent" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jqueryval") %>
</asp:Content>
