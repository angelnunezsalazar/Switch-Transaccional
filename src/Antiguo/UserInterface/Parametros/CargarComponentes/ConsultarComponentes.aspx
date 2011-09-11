<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarComponentes.aspx.cs" Inherits="UserInterface.Parametros.CargarComponentes.ConsultarComponentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="style1">
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <span class="titulo">Consultar Componente</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                <asp:GridView ID="grvComponente" runat="server" DataSourceID="dsComponente" AutoGenerateColumns="False"
                    ShowFooter="True" OnDataBound="grvComponente_DataBound" DataKeyNames="COM_CODIGO,COM_ARCHIVO"
                    OnRowCommand="grvComponente_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <FooterTemplate>
                                <asp:ImageButton ID="imgAgregar" runat="server" ImageUrl="~/Includes/Imagenes/iconNew.png"
                                    ValidationGroup="Grupo1" OnClick="imgAgregar_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NOMBRE" SortExpression="COM_NOMBRE">
                            <ItemTemplate>
                                <asp:Label ID="lblNombreItem" runat="server" Text='<%# Bind("COM_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNombreFooter" runat="server" Width="160" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombreFooter" runat="server" ControlToValidate="txtNombreFooter"
                                    ErrorMessage="Debe ingresar el Nombre" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ARCHIVO">
                            <ItemTemplate>
                                <asp:Label ID="lblArchivoItem" runat="server" Text='<%# Bind("COM_ARCHIVO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:FileUpload ID="uplComponenteEdit" runat="server" size="40px" Width="350px" />
                                <asp:RequiredFieldValidator ID="rfvComponenteEdit" runat="server" ControlToValidate="uplComponenteEdit"
                                    ErrorMessage="Debe ingresar la Ruta del Archivo" ValidationGroup="Grupo2">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:FileUpload ID="uplComponenteFooter" runat="server" size="40px" Width="350px" />
                                <asp:RequiredFieldValidator ID="rfvComponenteFooter" runat="server" ControlToValidate="uplComponenteFooter"
                                    ErrorMessage="Debe ingresar la Ruta del Archivo" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" runat="server" CommandName="Edit" AlternateText="Editar"
                                    ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgModificar" runat="server" CommandName="Modificar" CommandArgument='<%# Container.DataItemIndex %>'
                                    ImageUrl="~/Includes/Imagenes/iconSave.png" ValidationGroup="Grupo2" />
                                <asp:ImageButton ID="imgCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Includes/Imagenes/iconCancel.png" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEliminar" runat="server" CommandName="Eliminar" AlternateText="Eliminar"
                                    CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="~/Includes/Imagenes/iconErase.png"
                                    OnClientClick="return confirm('Esta seguro que quiere eliminar el Componente ?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsComponente" runat="server" DataObjectTypeName="BusinessEntity.COMPONENTE"
                    DeleteMethod="eliminarComponente" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerComponente" TypeName="BusinessLayer.Parametros.ComponenteBL"
                    UpdateMethod="modificarComponente" OnSelected="dsComponente_Selected"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:ValidationSummary ID="Summary" runat="server" HeaderText="Se han producido los siguientes errores"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Grupo2" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Se han producido los siguientes errores"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Grupo1" />
            </td>
        </tr>
    </table>
</asp:Content>
