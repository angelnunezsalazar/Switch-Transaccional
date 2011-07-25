<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarEstadoTerminal.aspx.cs" Inherits="UserInterface.Terminales.MantenimientoEstadoTerminal.ConsultarEstadoTerminal" %>

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
                <span class="titulo">Consultar Estado Terminal</span>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="grvEstadoTerminal" runat="server" AutoGenerateColumns="False" DataSourceID="dsEstadoTerminal"
                    ShowFooter="True" DataKeyNames="EST_CODIGO" OnDataBound="grvEstadoTerminal_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <FooterTemplate>
                                <asp:ImageButton ID="imgAgregar" CommandName="Insert" runat="server" ImageUrl="~/Includes/Imagenes/iconNew.png"
                                    OnClick="Button4_Click" ValidationGroup="NuevoEstado" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre" >
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNombreEdit" runat="server" Text='<%# Bind("EST_NOMBRE") %>' MaxLength="50" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombreEdit"
                                    ErrorMessage="Debe ingresar el Nombre" ValidationGroup="ModificarEstado">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescripcion"
                                    ErrorMessage="Debe ingresar el Nombre" ValidationGroup="NuevoEstado">*</asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNombreItem" runat="server" Text='<%# Eval("EST_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgActualizar" runat="server" CommandName="Update" ImageUrl="~/Includes/Imagenes/iconSave.png"
                                    ValidationGroup="ModificarEstado" />
                                <asp:ImageButton ID="imgCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Includes/Imagenes/iconCancel.png" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" runat="server" CommandName="Edit" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEliminar" runat="server" CommandName="Delete" ImageUrl="~/Includes/Imagenes/iconErase.png" 
                                OnClientClick="return confirm('Esta seguro que quiere eliminar el Estado Terminal?')"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsEstadoTerminal" runat="server" DataObjectTypeName="BusinessEntity.ESTADO_TERMINAL"
                    DeleteMethod="eliminarEstadoTerminal" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerEstadoTerminal" TypeName="BusinessLayer.Terminales.EstadoTerminalBL"
                    UpdateMethod="modificarEstadoTerminal" OnDeleted="dsEstadoTerminal_Deleted" OnUpdated="dsEstadoTerminal_Updated"
                    OnSelected="dsEstadoTerminal_Selected"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="vlsNuevoEstado" runat="server" ValidationGroup="NuevoEstado"
        ShowMessageBox="True" ShowSummary="False" HeaderText="Se han producido los siguientes errores:" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="ModificarEstado" HeaderText="Se han producido los siguientes errores:" />
</asp:Content>
