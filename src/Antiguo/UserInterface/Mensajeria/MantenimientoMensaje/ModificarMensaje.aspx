<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ModificarMensaje.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoMensaje.ModificarMensaje" %>

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
                <span class="titulo">Agregar Mensaje</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:FormView ID="frmMensaje" runat="server" DataSourceID="dsMensaje" DefaultMode="Edit"
                    OnDataBound="frmMensaje_DataBound" HorizontalAlign="Center" Width="420px" 
                    DataKeyNames="MEN_CODIGO">
                    <EditItemTemplate>
                        <table class="style1">
                            <tr>
                                <td>
                                    <span class="texto">Grupo de Mensaje</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblGrupoMensaje" runat="server" Text='<%# Eval("GRUPO_MENSAJE.GMJ_NOMBRE") %>'
                                        CssClass="texto" />
                                    <input id="lblCodigoGrupoMensaje" runat="server" type="hidden" value='<%# Eval("GRUPO_MENSAJE.GMJ_CODIGO") %>' />
                                </td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="texto">Nombre </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("MEN_NOMBRE") %>' MaxLength="50"
                                        Width="220px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                                        ErrorMessage="Debe ingresar el Nombre" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="texto">Descripción </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Bind("MEN_DESCRIPCION") %>'
                                        TextMode="MultiLine" Rows="5" Width="274px" MaxLength="200" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnAgregar" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="Grupo" />
                                    <asp:Button ID="btnCancelar" runat="server" OnClick="Button2_Click" Text="Cancelar" />
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsMensaje" runat="server" DataObjectTypeName="BusinessEntity.MENSAJE"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="obtenerMensaje" TypeName="BusinessLayer.Mensajeria.MensajeBL"
                    UpdateMethod="modificarMensaje" OnUpdated="dsMensaje_Updated" OnUpdating="dsMensaje_Updating">
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
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:ValidationSummary ID="vlsProtocolo" runat="server" HeaderText="Se han producido los siguientes errores"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Grupo" />
            </td>
        </tr>
    </table>
</asp:Content>
