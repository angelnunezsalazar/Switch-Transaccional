<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true" CodeBehind="ModificarReglasValidacion.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoReglasValidacion.ModificarReglasValidacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:FormView ID="fvGrupoValidacion" runat="server" 
        DefaultMode="Edit" DataKeyNames="GRV_CODIGO" 
        DataSourceID="dsGrupoValidacion">
        <EditItemTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        C&oacute;digo
                    </td>
                    <td>
                        <asp:Label ID="Codigo" runat="server" 
                            Text='<%# Bind("GRV_CODIGO") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Nombre
                    </td>
                    <td>
                        <asp:Label ID="Nombre" runat="server" 
                            Text='<%# Bind("GRV_NOMBRE") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Grupo de Mensajes
                    </td>
                    <td>
                        <asp:Label ID="lblGrupoMensaje" runat="server" Text='<%# Bind("MENSAJE.GRUPO_MENSAJE.GMJ_NOMBRE") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Mensaje
                    </td>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server" Text='<%# Bind("MENSAJE.MEN_NOMBRE") %>' />
                        <asp:HiddenField ID="hdnCodigoMensaje" runat="server" Value='<%# Bind("MENSAJE.MEN_CODIGO") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Campo
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCampo" runat="server" DataSourceID="dsCampo" 
                            DataTextField="CAM_NOMBRE" DataValueField="CAM_CODIGO" 
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="dsCampo" runat="server" 
                            OldValuesParameterFormatString="original_{0}" SelectMethod="obtenerCampo" 
                            TypeName="BusinessLayer.Mensajeria.CampoMensajeBL">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hdnCodigoMensaje" DefaultValue="0" 
                                    Name="codigoMensaje" PropertyName="Value" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
            <br />
            <div align="center">
                <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" 
                    onclick="btnAdicionar_Click"/>
            </div>
        </EditItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="dsGrupoValidacion" runat="server" 
        DataObjectTypeName="BusinessEntity.GRUPO_VALIDACION" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="obtenerGrupoValidacion" 
        TypeName="BusinessLayer.Mensajeria.GrupoValidacionBL" 
        UpdateMethod="modificarGrupoValidacion">
        <SelectParameters>
            <asp:QueryStringParameter Name="mensaje" QueryStringField="Codigo" 
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
