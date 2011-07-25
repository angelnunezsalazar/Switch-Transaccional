<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarDinamica.aspx.cs" Inherits="UserInterface.Operacion.MantenimientoDinamica.ConsultarDinamica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .style3
        {
            width: 200px;
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
                <span class="titulo">Consultar Pasos Dinámica</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 390px">
        <tr>
            <td class="style3">
                <span class="texto">Grupo Mensaje Inicio</span>
            </td>
            <td>
                <asp:DropDownList ID="drlGrupoMensajeInicio" runat="server" DataSourceID="dsGrupoMensaje"
                    DataTextField="GMJ_NOMBRE" DataValueField="GMJ_CODIGO" AppendDataBoundItems="True"
                    AutoPostBack="True" Width="230px" OnDataBound="drlGrupoMensajeInicio_DataBound">
                    <asp:ListItem Value="-1">Seleccionar Grupo</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="dsGrupoMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerGrupoMensaje" TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL">
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <span class="texto">Mensaje Inicio</span>
            </td>
            <td>
                <asp:UpdatePanel ID="udpMensaje" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drlMensaje" runat="server" DataSourceID="dsMensaje" DataTextField="MEN_NOMBRE"
                            DataValueField="MEN_CODIGO" AppendDataBoundItems="True" EnableViewState="False"
                            Width="230px" AutoPostBack="True" OnDataBound="drlMensaje_DataBound">
                            <asp:ListItem Value="-1">Seleccionar Mensaje</asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="dsMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
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
            <td class="style3">
                <span class="texto">Mensaje Transaccional</span>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drlMensajeTransaccional" runat="server" AutoPostBack="True"
                            DataSourceID="dsMensajeTransaccional" DataTextField="MTR_NOMBRE" DataValueField="MTR_CODIGO"
                            OnSelectedIndexChanged="drlMensajeTransaccional_SelectedIndexChanged" AppendDataBoundItems="True"
                            EnableViewState="False" Width="230px" Style="height: 22px" OnDataBound="drlMensajeTransaccional_DataBound">
                            <asp:ListItem Value="-1">Seleccionar Mensaje Transaccional</asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="dsMensajeTransaccional" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="obtenerMensajeTransaccionalPorMensaje" TypeName="BusinessLayer.Mensajeria.MensajeTransaccionalBL">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drlMensaje" DefaultValue="-1" Name="codigoMensaje"
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drlMensaje" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table class="style2">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:GridView ID="grvPasoDinamica" runat="server" AutoGenerateColumns="False" DataSourceID="dsPasoDinamica"
                            OnDataBound="grvPasoDinamica_DataBound" DataKeyNames="PDT_CODIGO">
                            <Columns>
                                <asp:BoundField DataField="PDT_NUMERO" HeaderText="NUMERO PASO" SortExpression="PDT_NUMERO" />
                                <asp:TemplateField HeaderText="FUNCIONALIDAD" SortExpression="PDT_FUNCIONALIDAD">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# ObtenerNombreTipoFuncionalidad(Eval("PDT_FUNCIONALIDAD").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DESCRIPCION">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("PDT_INFORMACION_ADICIONAL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FIN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFin" runat="server" Text='<%# Boolean.Parse(Eval("PDT_FIN").ToString())?"Verdadero":"Falso" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="dsPasoDinamica" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="obtenerDinamicaTransaccional" TypeName="BusinessLayer.Operacion.PasoDinamicaBL">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drlMensajeTransaccional" Name="codigoMensajeTransaccional"
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
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />&nbsp;
                        <asp:Button ID="btnEliminar" runat="server" Height="26px" OnClick="btnEliminar_Click"
                            Text="Eliminar" />
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="drlMensajeTransaccional" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
