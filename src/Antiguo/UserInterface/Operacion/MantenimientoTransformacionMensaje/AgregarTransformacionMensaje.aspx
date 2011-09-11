<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="AgregarTransformacionMensaje.aspx.cs" Inherits="UserInterface.Operacion.MantenimientoTransformacionMensaje.AgregarTransformacionMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style4
        {
            width: 152px;
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
                <span class="titulo">Agregar Transformación Mensaje</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table style="margin: auto; width: 400px">
        <tr>
            <td>
                <span class="texto">Nombre Transformada</span>
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" MaxLength="200" Width="220px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                    ControlToValidate="txtNombre" 
                    ErrorMessage="Debe ingresar el Nombre Transformada" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style4">
                <span class="texto">Grupo Mensaje Origen</span>
            </td>
            <td>
                <asp:DropDownList ID="drlGrupoMensajeInicio" runat="server" DataSourceID="dsGrupoMensaje"
                    DataTextField="GMJ_NOMBRE" DataValueField="GMJ_CODIGO" AppendDataBoundItems="True"
                    AutoPostBack="True" Width="190px">
                    <asp:ListItem Value="-1">Seleccionar Grupo Mensaje</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvGrupoOrigen" runat="server" 
                    ControlToValidate="drlGrupoMensajeInicio" 
                    ErrorMessage="Debe ingresar el Grupo Mensaje Origen" InitialValue="-1" 
                    ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                <asp:ObjectDataSource ID="dsGrupoMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerGrupoMensaje" TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL">
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <span class="texto">Mensaje Origen</span>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drlMensajeOrigen" runat="server" DataSourceID="dsMensajeOrigen"
                            DataTextField="MEN_NOMBRE" DataValueField="MEN_CODIGO" EnableViewState="False"
                            AppendDataBoundItems="True" Width="190px">
                            <asp:ListItem Value="-1">Seleccionar Mensaje</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvMensajeOrigen" runat="server" 
                            ControlToValidate="drlMensajeOrigen" 
                            ErrorMessage="Debe ingresar el Mensaje Origen" InitialValue="-1" 
                            ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                        <asp:ObjectDataSource ID="dsMensajeOrigen" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="obtenerMensajePorCodigoGrupoMensaje" TypeName="BusinessLayer.Mensajeria.MensajeBL">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drlGrupoMensajeInicio" DefaultValue="-1" Name="codigoGrupoMensaje"
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drlGrupoMensajeInicio" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="style4">
                <span class="texto">Grupo Mensaje Destino</span>
            </td>
            <td>
                <asp:DropDownList ID="drlGrupoMensajeFin" runat="server" DataSourceID="dsGrupoMensaje"
                    DataTextField="GMJ_NOMBRE" DataValueField="GMJ_CODIGO" AppendDataBoundItems="True"
                    AutoPostBack="True" Width="190px">
                    <asp:ListItem Value="-1">Seleccionar Grupo Mensaje</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvGrupoDestino" runat="server" 
                    ControlToValidate="drlGrupoMensajeFin" 
                    ErrorMessage="Debe ingresar el Grupo Mensaje Destino" InitialValue="-1" 
                    ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <span class="texto">Mensaje Destino</span>
            </td>
            <td>
                <asp:UpdatePanel ID="updMensajeFin" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drlMensajeDestino" runat="server" DataSourceID="dsMensajeDestino"
                            DataTextField="MEN_NOMBRE" DataValueField="MEN_CODIGO" AppendDataBoundItems="True"
                            EnableViewState="False" Width="190px">
                            <asp:ListItem Value="-1">Seleccionar Mensaje</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvMensajeDestino" runat="server" 
                            ControlToValidate="drlMensajeDestino" 
                            ErrorMessage="Debe ingresar el Mensaje Destino" InitialValue="-1" 
                            ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                        <asp:ObjectDataSource ID="dsMensajeDestino" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="obtenerMensajePorCodigoGrupoMensaje" TypeName="BusinessLayer.Mensajeria.MensajeBL">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drlGrupoMensajeFin" DefaultValue="-1" Name="codigoGrupoMensaje"
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drlGrupoMensajeFin" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="btnNuevo" runat="server" OnClick="btnAgregar_Click" 
                    Text="Aceptar" ValidationGroup="Grupo1" />
                <asp:Button ID="btnCancelar" runat="server" PostBackUrl="~/Operacion/MantenimientoTransformacionMensaje/ConsultarTransformacionMensaje.aspx"
                    Text="Cancelar" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:ValidationSummary ID="vlsTransformacionMensaje" runat="server" 
                    HeaderText="Se han producido los siguientes errores:" ShowMessageBox="True" 
                    ShowSummary="False" ValidationGroup="Grupo1" />
            </td>
        </tr>
    </table>
</asp:Content>
