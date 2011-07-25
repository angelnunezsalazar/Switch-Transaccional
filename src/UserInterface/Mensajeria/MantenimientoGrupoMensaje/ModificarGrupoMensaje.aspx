<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ModificarGrupoMensaje.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoGrupoMensaje.ModificarGrupoMensaje" %>

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
                <span class="titulo">Modificar Grupo de Mensaje</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:FormView ID="frmGrupoMensaje" runat="server" DataSourceID="dsGrupoMensaje" DefaultMode="Edit"
                    OnDataBound="FormView1_DataBound" DataKeyNames="GMJ_CODIGO" HorizontalAlign="Center">
                    <EditItemTemplate>
                        <table class="style1">
                            <tr>
                                <td>
                                    Nombre
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("GMJ_NOMBRE") %>' MaxLength="50"
                                        Width="220px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                                        ErrorMessage="Debe ingresar el Nombre" ValidationGroup="GrupoMensaje">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Descripcion
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Bind("GMJ_DESCRIPCION") %>'
                                        TextMode="MultiLine" Rows="5" Width="274px" MaxLength="200" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tipo Mensaje
                                </td>
                                <td>
                                    <asp:DropDownList ID="drlTipoMensaje" runat="server" DataSourceID="dsTipoMensaje"
                                        DataTextField="TMJ_NOMBRE" DataValueField="TMJ_CODIGO" SelectedValue='<%# Eval("TIPO_MENSAJE.TMJ_CODIGO")%>'>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drlTipoMensaje"
                                        ErrorMessage="Debe ingresar el Tipo de Mensaje" InitialValue="-1" ValidationGroup="GrupoMensaje">*</asp:RequiredFieldValidator>
                                    <asp:ObjectDataSource ID="dsTipoMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="obtenerTipoMensaje" TypeName="BusinessLayer.Mensajeria.TipoMensajeBL">
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnAceptar" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="GrupoMensaje" />
                                    <asp:Button ID="btnCancelar" runat="server" PostBackUrl="~/Mensajeria/MantenimientoGrupoMensaje/ConsultarGrupoMensaje.aspx"
                                        Text="Cancelar" />
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </asp:FormView>
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                <asp:ObjectDataSource ID="dsGrupoMensaje" runat="server" DataObjectTypeName="BusinessEntity.GRUPO_MENSAJE"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="obtenerGrupoMensaje"
                    TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL" OnUpdated="dsGrupoMensaje_Updated"
                    OnUpdating="dsGrupoMensaje_Updating" UpdateMethod="modificarGrupoMensaje">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="codigo" QueryStringField="Codigo" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:ValidationSummary ID="vlsProtocolo" runat="server" HeaderText="Se han producido los siguientes errores"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="GrupoMensaje" />
            </td>
        </tr>
    </table>
</asp:Content>
