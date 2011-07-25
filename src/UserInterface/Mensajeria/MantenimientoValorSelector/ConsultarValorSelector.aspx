<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarValorSelector.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoValorSelector.ConsultarValorSelector" %>

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
                <span class="titulo">Consultar Valor Selector</span>
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
                <asp:DropDownList ID="drlMensaje" runat="server" DataSourceID="dsMensaje" DataTextField="MEN_NOMBRE"
                    DataValueField="MEN_CODIGO" AppendDataBoundItems="True" AutoPostBack="True" EnableViewState="False">
                    <asp:ListItem Value="-1">Seleccionar Mensaje</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="dsMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerMensajePorCodigoGrupoMensaje" TypeName="BusinessLayer.Mensajeria.MensajeBL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drlGrupoMensaje" DefaultValue="-1" Name="codigoGrupoMensaje"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
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
    <table class="style1">
        <tr>
            <td>
                <asp:GridView ID="grvValorSelector" runat="server" AutoGenerateColumns="False" DataSourceID="dsCampo"
                    DataKeyNames="CAM_CODIGO,MEN_CODIGO">
                    <Columns>
                        <asp:TemplateField HeaderText="Nombre" SortExpression="CAM_NOMBRE">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("CAM_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Longitud" SortExpression="CAM_LONGITUD">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("CAM_LONGITUD") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TipoDato">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoDatoItem" runat="server" Text='<%# Eval("TIPO_DATO.TDT_NOMBRE") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ValorSelectorRequest">
                            <ItemTemplate>
                                <asp:Label ID="lblValorSelectorRequestITem" runat="server" Text='<%# Eval("CAM_VALOR_SELECTOR_REQUEST")==null?"Sin Valor":Eval("CAM_VALOR_SELECTOR_REQUEST") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtValorSelectorRequestEdit" runat="server" Text='<%# Bind("CAM_VALOR_SELECTOR_REQUEST") %>'
                                    MaxLength="50" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValorSelectorRequestEdit"
                                    ErrorMessage="Debe ingresar el Valor Selector Request" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ValorSelectorResponse">
                            <ItemTemplate>
                                <asp:Label ID="lblValorSelectorResponseITem" runat="server" Text='<%# Eval("CAM_VALOR_SELECTOR_RESPONSE")==null?"Sin Valor":Eval("CAM_VALOR_SELECTOR_RESPONSE") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtValorSelectorResponseEdit" runat="server" Text='<%# Bind("CAM_VALOR_SELECTOR_RESPONSE") %>'
                                    MaxLength="50" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtValorSelectorResponseEdit"
                                    ErrorMessage="Debe ingresar el Valor Selector Response" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgActualizar" runat="server" CommandName="Update" ImageUrl="~/Includes/Imagenes/iconSave.png"
                                    ValidationGroup="Grupo" />
                                <asp:ImageButton ID="imgCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Includes/Imagenes/iconCancel.png" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" runat="server" CommandName="Edit" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsCampo" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerCampoSelector" TypeName="BusinessLayer.Mensajeria.CampoMensajeBL"
                    DataObjectTypeName="BusinessEntity.CAMPO" OnUpdated="dsCampo_Updated" UpdateMethod="actualizarValorSelector">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drlMensaje" DefaultValue="-1" Name="codigoMensaje"
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
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:ValidationSummary ID="Summary" runat="server" HeaderText="Se han producido los siguientes errores"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Grupo" />
            </td>
        </tr>
    </table>
</asp:Content>
