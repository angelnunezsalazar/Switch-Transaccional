<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Navegacion.aspx.cs" Inherits="UserInterface.Frames.Navegacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="siteMap">
            <ParentNodeStyle CssClass="nodo" />
            <SelectedNodeStyle CssClass="nodo_seleccionado" />
            <DataBindings>
                <asp:TreeNodeBinding DataMember="SiteMapNode" NavigateUrlField="Url" Target="frameContenido"
                    TextField="Title" />
            </DataBindings>
            <RootNodeStyle CssClass="nodo_raiz" />
            <NodeStyle CssClass="nodo" />
        </asp:TreeView>
        <asp:SiteMapDataSource ID="siteMap" runat="server" />
    </div>
    </form>
</body>
</html>
