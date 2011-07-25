<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarTanqueo.aspx.cs" Inherits="UserInterface.Operacion.MantenimientoTanqueo.ConsultarTanqueo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <span class="titulo">Consultar Tanqueo Mensaje</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 35%">
        <tr>
            <td>
                <span class="texto">Grupo de Mensaje</span>
            </td>
            <td>
                <asp:DropDownList ID="drlGrupoMensaje" runat="server" DataSourceID="dsGrupoMensaje"
                    DataTextField="GMJ_NOMBRE" DataValueField="GMJ_CODIGO" AppendDataBoundItems="True"
                    AutoPostBack="True" EnableViewState="False">
                    <asp:ListItem Value="-1">Seleccionar Grupo de Mensaje</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="dsGrupoMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerGrupoMensaje" TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL">
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <span class="texto">Mensaje</span>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="drlMensaje" runat="server" DataSourceID="dsMensaje" DataTextField="MEN_NOMBRE"
                            DataValueField="MEN_CODIGO" AppendDataBoundItems="True" 
                            AutoPostBack="True" EnableViewState="False">
                            <asp:ListItem Value="-1">Seleccionar Mensaje</asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="dsMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="obtenerMensajePorCodigoGrupoMensaje" TypeName="BusinessLayer.Mensajeria.MensajeBL">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drlGrupoMensaje" DefaultValue="-1" Name="codigoGrupoMensaje"
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drlGrupoMensaje" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
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
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvCampo" runat="server" AutoGenerateColumns="False" DataSourceID="dsCampo"
                            DataKeyNames="CAM_CODIGO,MEN_CODIGO" RowStyle-HorizontalAlign="Center" 
                            OnDataBound="grvCampo_DataBound" ondatabinding="grvCampo_DataBinding">
                            <Columns>
                                <asp:TemplateField HeaderText="Posicion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosicionRelativa" runat="server" Text='<%# Eval("CAM_POSICION_RELATIVA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombreItem" runat="server" Text='<%# Eval("CAM_NOMBRE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tanqueo">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkTanqueo" runat="server" Checked='<%# Bind("CAM_TANQUEO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Destanqueo">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDestanqueo" runat="server" Checked='<%# Bind("CAM_DESTANQUEO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="dsCampo" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="obtenerCampoCuerpo" TypeName="BusinessLayer.Mensajeria.CampoMensajeBL">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drlMensaje" Name="codigoMensaje" PropertyName="SelectedValue"
                                    Type="Int32" />
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
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnAceptar" runat="server" Text="Guardar" Visible="False" OnClick="btnAceptar_Click" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drlMensaje" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drlMensaje" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
