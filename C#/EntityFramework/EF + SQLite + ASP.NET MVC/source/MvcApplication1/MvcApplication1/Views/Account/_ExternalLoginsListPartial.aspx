﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ICollection<Microsoft.Web.WebPages.OAuth.AuthenticationClientData>>" %>

<% if (Model.Count == 0) { %>
    <div class="message-info">
        <p>Внешние службу проверки подлинности не настроены. См. в <a href="http://go.microsoft.com/fwlink/?LinkId=252166">этой статье</a>
        сведения о настройке входа через сторонние службы в этом приложении ASP.NET.</p>
    </div>
<% } else {
    using (Html.BeginForm("ExternalLogin", "Account", new { ReturnUrl = ViewBag.ReturnUrl })) { %>
    <%: Html.AntiForgeryToken() %>
    <fieldset id="socialLoginList">
        <legend>Вход через другую службу</legend>
        <p>
        <% foreach (Microsoft.Web.WebPages.OAuth.AuthenticationClientData p in Model) { %>
            <button type="submit" name="provider" value="<%: p.AuthenticationClient.ProviderName %>" title="Войдите, используя <%: p.DisplayName %> свою учетную запись."><%: p.DisplayName%></button>
        <% } %>
        </p>
    </fieldset>
    <% }
} %>
