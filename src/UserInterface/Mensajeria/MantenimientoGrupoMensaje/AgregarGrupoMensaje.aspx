<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="AgregarGrupoMensaje.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoGrupoMensaje.AgregarGrupoMensaje" %>

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
                <span class="titulo">Agregar Grupo de Mensaje</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:FormView ID="frmGrupoMensaje" runat="server" DataSourceID="dsGrupoMensaje" DefaultMode="Insert"
                    OnDataBound="FormView1_DataBound" HorizontalAlign="Center" Width="400px">
                    <InsertItemTemplate>
                        <table class="style1">
                            <tr>
                                <td>
                                    <span class="texto">Nombre </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("GMJ_NOMBRE") %>' MaxLength="50"
                                        Width="220px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                                        ErrorMessage="Debe ingresar el Nombre" ValidationGroup="AgregarGrupo">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="texto">Descripción </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Bind("GMJ_DESCRIPCION") %>'
                                        TextMode="MultiLine" Rows="5" Width="274px" MaxLength="200" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="texto">Tipo de Mensaje </span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drlTipoMensaje" runat="server" DataSourceID="dsTipoMensaje"
                                        DataTextField="TMJ_NOMBRE" DataValueField="TMJ_CODIGO" AppendDataBoundItems="True">
                                        <asp:ListItem Value="-1">Seleccionar Tipo de Mensaje</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drlTipoMensaje"
                                        ErrorMessage="Debe ingresar el Tipo de Mensaje" InitialValue="-1" ValidationGroup="AgregarGrupo">*</asp:RequiredFieldValidator>
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
                                    <asp:Button ID="Button1" runat="server" CommandName="Insert" Text="Aceptar" ValidationGroup="AgregarGrupo" />
                                    <asp:Button ID="Button2" runat="server" PostBackUrl="~/Mensajeria/MantenimientoGrupoMensaje/ConsultarGrupoMensaje.aspx"
                                        Text="Cancelar" />
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsGrupoMensaje" runat="server" DataObjectTypeName="BusinessEntity.GRUPO_MENSAJE"
                    InsertMethod="insertarGrupoMensaje" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerGrupoMensaje" TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL"
                    ConflictDetection="CompareAllValues" OnInserted="dsGrupoMensaje_Inserted" OnInserting="dsGrupoMensaje_Inserting">
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
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:ValidationSummary ID="vlsProtocolo" runat="server" HeaderText="Se han producido los siguientes errores"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="AgregarGrupo" />
            </td>
        </tr>
    </table>
</asp:Content>
