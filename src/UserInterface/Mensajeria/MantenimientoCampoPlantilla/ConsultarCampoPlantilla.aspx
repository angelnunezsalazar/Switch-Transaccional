<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarCampoPlantilla.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoCampoPlantilla.ConsultarCampoPlantilla" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="style2">
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <span class="titulo">Consultar Campo Plantilla </span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 34%">
        <tr>
            <td>
                <span class="texto">Grupo Mensaje</span>
            </td>
            <td>
                <asp:DropDownList ID="drlGrupoMensaje" runat="server" DataSourceID="dsGrupoMensaje"
                    DataTextField="GMJ_NOMBRE" DataValueField="GMJ_CODIGO" AppendDataBoundItems="true"
                    AutoPostBack="True" OnSelectedIndexChanged="drlGrupoMensaje_SelectedIndexChanged"
                    OnDataBound="drlGrupoMensaje_DataBound">
                    <asp:ListItem Value="-1">Seleccionar Grupo de Mensaje</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="dsGrupoMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerGrupoMensaje" TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL"
                    OnSelected="dsGrupoMensaje_Selected"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table class="style2">
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" DataSourceID="dsCabecera" AutoGenerateColumns="False"
                    DataKeyNames="CMP_CODIGO">
                    <Columns>
                        <asp:BoundField DataField="CMP_NOMBRE" HeaderText="Nombre" SortExpression="CMP_NOMBRE" />
                        <asp:BoundField DataField="CMP_LONGITUD" HeaderText="Longitud" SortExpression="CMP_LONGITUD" />
                        <asp:TemplateField HeaderText="TipoDato">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoDatoItem" runat="server" Text='<%#Eval("TIPO_DATO.TDT_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CheckBoxField DataField="CMP_SELECTOR" HeaderText="Selector" SortExpression="CMP_SELECTOR" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" PostBackUrl='<%# "~/Mensajeria/MantenimientoCampoPlantilla/ModificarCampoPlantilla.aspx?CodigoCampoPlantilla="+ Eval("CMP_CODIGO")+"&CodigoGrupoMensaje=" + this.drlGrupoMensaje.SelectedValue %>'
                                    AlternateText="Modificar" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Delete" AlternateText="Eliminar"
                                    ImageUrl="~/Includes/Imagenes/iconErase.png" OnClientClick="return confirm('Esta seguro que desea eliminar el Campo Plantilla?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsCabecera" runat="server" DataObjectTypeName="BusinessEntity.CAMPO_PLANTILLA"
                    DeleteMethod="eliminarCampoPlantilla" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerCampoPlantillaCabeceraPorCodigoGrupoMensaje" TypeName="BusinessLayer.Mensajeria.CampoPlantillaBL"
                    OnSelecting="dsCampoPlantilla_Selecting" OnDeleted="dsCampoPlantilla_Deleted">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drlGrupoMensaje" DefaultValue="-1" Name="codigoGrupoMensaje"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <asp:GridView ID="grdCampoPlantilla" runat="server" AutoGenerateColumns="False" DataSourceID="dsCampoPlantilla"
                    DataKeyNames="CMP_CODIGO" OnDataBound="grdCampoPlantilla_DataBound">
                    <Columns>
                        <asp:BoundField DataField="CMP_POSICION_RELATIVA" HeaderText="Posición" SortExpression="CMP_POSICION_RELATIVA" />
                        <asp:BoundField DataField="CMP_NOMBRE" HeaderText="Nombre" SortExpression="CMP_NOMBRE" />
                        <asp:CheckBoxField DataField="CMP_VARIABLE" HeaderText="Variable" SortExpression="CMP_VARIABLE" />
                        <asp:BoundField DataField="CMP_LONGITUD_CABECERA" HeaderText=" Longitud Cabecera" SortExpression="CMP_LONGITUD_CABECERA" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="CMP_LONGITUD" HeaderText="Longitud Cuerpo" SortExpression="CMP_LONGITUD" ItemStyle-HorizontalAlign="Center"/>
                        
                        <asp:TemplateField HeaderText="TipoDato">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoDatoItem" runat="server" Text='<%#Eval("TIPO_DATO.TDT_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" PostBackUrl='<%# "~/Mensajeria/MantenimientoCampoPlantilla/ModificarCampoPlantilla.aspx?CodigoCampoPlantilla="+ Eval("CMP_CODIGO")+"&CodigoGrupoMensaje=" + this.drlGrupoMensaje.SelectedValue %>'
                                    AlternateText="Modificar" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Delete" AlternateText="Eliminar"
                                    ImageUrl="~/Includes/Imagenes/iconErase.png" OnClientClick="return confirm('Esta seguro que desea eliminar el Campo Plantilla?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsCampoPlantilla" runat="server" DataObjectTypeName="BusinessEntity.CAMPO_PLANTILLA"
                    DeleteMethod="eliminarCampoPlantilla" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerCampoPlantillaCuerpoPorCodigoGrupoMensaje" TypeName="BusinessLayer.Mensajeria.CampoPlantillaBL"
                    OnSelecting="dsCampoPlantilla_Selecting" OnDeleted="dsCampoPlantilla_Deleted">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drlGrupoMensaje" DefaultValue="-1" Name="codigoGrupoMensaje"
                            PropertyName="SelectedValue" Type="Int32" />
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
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
