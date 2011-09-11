<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarMensajeTransaccional.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoMensajeTransaccional.ConsultarMensajeTransaccional" %>

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
                <span class="titulo">Consultar Mensaje Transaccional</span>
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
                    AutoPostBack="True" EnableViewState="False" Width="200px">
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drlMensaje" runat="server" DataSourceID="dsMensaje" DataTextField="MEN_NOMBRE"
                            DataValueField="MEN_CODIGO" AppendDataBoundItems="True" AutoPostBack="True" EnableViewState="False"
                            Width="200px">
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
    <table class="style1">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table class="style1">
                            <tr>
                                <td>
                                    <asp:GridView ID="grvMensajeTransaccional" runat="server" DataSourceID="dsMensajeTransaccional"
                                        AutoGenerateColumns="False" OnDataBound="GridView1_DataBound" ShowFooter="True"
                                        DataKeyNames="MTR_CODIGO">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumeroItem" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Includes/Imagenes/iconNew.png"
                                                        OnClick="ImageButton4_Click" ValidationGroup="Grupo1" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre" SortExpression="MTR_NOMBRE">
                                                <ItemTemplate>
                                                    &nbsp;
                                                    <asp:Label ID="lblNombreItem" runat="server" Text='<%# Bind("MTR_NOMBRE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    &nbsp;<asp:TextBox ID="txtNombreEdit" runat="server" Text='<%# Bind("MTR_NOMBRE") %>'
                                                        MaxLength="50" Width="200px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombreEdit"
                                                        ErrorMessage="Debe ingresar el Nombre" ValidationGroup="Grupo2">*</asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    &nbsp;<asp:TextBox ID="txtNombreFooter" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombreFooter"
                                                        ErrorMessage="Debe ingresar el Nombre" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Condicion" SortExpression="MTR_NOMBRE" FooterStyle-Width="160px">
                                                <ItemTemplate>
                                                    &nbsp;<asp:Label ID="lblCondicionMensajeItem" runat="server" Text='<%# Bind("CONDICION_MENSAJE.CNM_NOMBRE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:RadioButtonList ID="rblCondicionMensajeEdit" runat="server" DataSourceID="dsCondicionMensajeEdit"
                                                        DataTextField="CNM_NOMBRE" DataValueField="CNM_CODIGO" RepeatDirection="Horizontal"
                                                        SelectedValue='<%# Eval("CONDICION_MENSAJE.CNM_CODIGO")%>'>
                                                    </asp:RadioButtonList>
                                                    <asp:ObjectDataSource ID="dsCondicionMensajeEdit" runat="server" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="obtenerCondicionMensaje" TypeName="BusinessLayer.Mensajeria.CondicionMensajeBL">
                                                    </asp:ObjectDataSource>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:RadioButtonList ID="rblCondicionMensajeFooter" runat="server" DataSourceID="dsCondicionMensajeFooter"
                                                        DataTextField="CNM_NOMBRE" DataValueField="CNM_CODIGO" RepeatDirection="Horizontal"
                                                        OnDataBound="rblCondicionMensajeFooter_DataBound">
                                                    </asp:RadioButtonList>
                                                    <asp:ObjectDataSource ID="dsCondicionMensajeFooter" runat="server" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="obtenerCondicionMensaje" TypeName="BusinessLayer.Mensajeria.CondicionMensajeBL">
                                                    </asp:ObjectDataSource>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" PostBackUrl='<%# "~/Mensajeria/MantenimientoMensajeTransaccional/ConsultarReglaMensajeTransaccional.aspx?CodigoMensajeTransaccional="+ Eval("MTR_CODIGO") %>'
                                                        AlternateText="Ver" ImageUrl="~/Includes/Imagenes/iconFind.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="imgActualizar" runat="server" CommandName="Update" ImageUrl="~/Includes/Imagenes/iconSave.png"
                                                        ValidationGroup="Grupo2" />
                                                    <asp:ImageButton ID="imgCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Includes/Imagenes/iconCancel.png" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEditar" runat="server" CommandName="Edit" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Delete" AlternateText="Eliminar"
                                                        ImageUrl="~/Includes/Imagenes/iconErase.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="dsMensajeTransaccional" runat="server" DataObjectTypeName="BusinessEntity.MENSAJE_TRANSACCIONAL"
                                        DeleteMethod="eliminarMensajeTransaccional" InsertMethod="insertarMensajeTransaccional"
                                        OldValuesParameterFormatString="original_{0}" SelectMethod="obtenerMensajeTransaccionalPorMensaje"
                                        TypeName="BusinessLayer.Mensajeria.MensajeTransaccionalBL" UpdateMethod="modificarMensajeTransaccional"
                                        OnSelected="dsMensajeTransaccional_Selected" OnSelecting="dsMensajeTransaccional_Selecting"
                                        OnDeleted="dsMensajeTransaccional_Deleted" OnUpdated="dsMensajeTransaccional_Updated"
                                        OnUpdating="dsMensajeTransaccional_Updating">
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
                                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drlMensaje" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:ValidationSummary ID="Summary" runat="server" HeaderText="Se han producido los siguientes errores"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Grupo2" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Se han producido los siguientes errores"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Grupo1" />
            </td>
        </tr>
    </table>
</asp:Content>
