<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="VerEntidadesEnGrupoMensaje.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoGrupoMensaje.VerEntidadesEnGrupoMensaje" %>

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
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                <span class="titulo">Ver Entidades en Grupos de Mensaje</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:FormView ID="FormView1" runat="server" DataSourceID="dsGrupoMensaje" 
                    HorizontalAlign="Center" >
                    <ItemTemplate>
                        <table class="style1">
                            <tr>
                                <td >
                                    <span class="texto">Nombre</span>
                                </td>
                                <td>
                                    &nbsp;<asp:Label ID="lblNombre" runat="server" Text='<%# Eval("GMJ_NOMBRE") %>' CssClass="texto" 
                                         />
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <span class="texto">TipoMensaje</span>
                                </td>
                                <td>
                                    &nbsp;<asp:Label ID="lblTipoMensaje" runat="server" 
                                        Text='<%# Eval("TIPO_MENSAJE.TMJ_NOMBRE") %>' CssClass="texto" >
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsGrupoMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerGrupoMensaje" TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="codigo" QueryStringField="Codigo" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="dsEntidad"
                    ShowFooter="True" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="EDC_CODIGO">
                    <Columns>
                        <asp:TemplateField>
                            <FooterTemplate>
                                <asp:ImageButton ID="imgNuevo" runat="server" ImageUrl="~/Includes/Imagenes/iconNew.png"
                                    OnClick="imgNuevo_Click" ValidationGroup="Entidad" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNumero" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre" SortExpression="EDC_NOMBRE">
                            <FooterTemplate>
                                <asp:DropDownList ID="drlEntidadComunicacion" runat="server" DataSourceID="dsEntidadSinGrupoMensaje"
                                    DataTextField="EDC_NOMBRE" DataValueField="EDC_CODIGO" AppendDataBoundItems="True">
                                    <asp:ListItem Value="-1">Seleccionar Entidad</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="drlEntidadComunicacion" 
                                    ErrorMessage="Debe seleccionar la Entidad de Comunicación" InitialValue="-1" 
                                    ValidationGroup="Entidad">*</asp:RequiredFieldValidator>
                                <asp:ObjectDataSource ID="dsEntidadSinGrupoMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="obtenerEntidadComunicacionSinGrupo" TypeName="BusinessLayer.Comunicacion.EntidadComunicacionBL">
                                </asp:ObjectDataSource>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("EDC_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEliminar" runat="server" CommandName="Delete" ImageUrl="~/Includes/Imagenes/iconErase.png" 
                                OnClientClick="return confirm('Esta seguro que quiere eliminar la Entidad del Grupo de Mensajes?')"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsEntidad" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerEntidadComunicacionEnGrupoMensaje" TypeName="BusinessLayer.Comunicacion.EntidadComunicacionBL"
                    OnSelected="dsEntidad_Selected" DataObjectTypeName="BusinessEntity.ENTIDAD_COMUNICACION"
                    DeleteMethod="eliminarEntidadDeGrupoMensaje" OnDeleted="dsEntidad_Deleted">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="codigoGrupoMensaje" QueryStringField="Codigo" Type="Int32" />
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
                <asp:Button ID="Button1" runat="server" PostBackUrl="~/Mensajeria/MantenimientoGrupoMensaje/ConsultarGrupoMensaje.aspx"
                    Text="Regresar" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    HeaderText="Se han producido los siguientes errores;" ShowMessageBox="True" 
                    ShowSummary="False" ValidationGroup="Entidad" />
            </td>
        </tr>
    </table>
</asp:Content>
