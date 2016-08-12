<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="labweb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Lab asp studenst</h1>
        <p class="lead">lab</p>
        <p><a href="http://www.smilek.in.ua" >just link example &raquo;</a></p>
    </div>    <asp:ListView ID="categoryList"
        ItemType="labweb.Students" runat="server" SelectMethod="GetStudents">
        <ItemTemplate> <%#: Item.Name%> </ItemTemplate>
            <%--<b style="font-size: large; font-style: normal">
                <a href="/ProductList.aspx?id=<%#: Item.Id %>">
                    <%#: Item.Name%>
                </a>
            </b>--%>
        <ItemSeparatorTemplate> | </ItemSeparatorTemplate>
    </asp:ListView>
</asp:Content>