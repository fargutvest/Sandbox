<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Shop.Models.LocalPasswordModel>" %>

<asp:Content ID="manageTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Управление учетной записью
</asp:Content>

<asp:Content ID="manageContent" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Управление учетной записью.</h1>
    </hgroup>

    <p class="message-success"><%: (string)ViewBag.StatusMessage %></p>

    <p>Выполнен вход: <strong><%: User.Identity.Name %></strong>.</p>

    <% if (ViewBag.HasLocalPassword) {
        Html.RenderPartial("_ChangePasswordPartial");
    } else {
        Html.RenderPartial("_SetPasswordPartial");
    } %>

    <section id="externalLogins">
        <%: Html.Action("RemoveExternalLogins") %>

        <h3>Добавить внешнюю учетную запись</h3>
        <%: Html.Action("ExternalLoginsList", new { ReturnUrl = ViewBag.ReturnUrl }) %>
    </section>
</asp:Content>

<asp:Content ID="scriptsContent" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jqueryval") %>
</asp:Content>