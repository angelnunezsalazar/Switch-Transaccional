<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarReglaMensajeTransaccional.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoMensajeTransaccional.ConsultarReglaMensajeTransaccional" %>

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
                <span class="titulo">Consultar Regla Transaccional</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                <asp:FormView ID="frmMensajeTransaccional" runat="server" DataSourceID="dsMensajeTransaccional"
                HorizontalAlign="Center">
                    <ItemTemplate>
                        <table class="style1">
                            <tr>
                                <td>
                                    <span class="texto">Grupo Mensaje</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblGrupoMensaje" runat="server" Text='<%# Bind("MENSAJE.GRUPO_MENSAJE.GMJ_NOMBRE") %>' CssClass="texto"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="texto">Mensaje</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblMensaje" runat="server" Text='<%# Bind("MENSAJE.MEN_NOMBRE") %>' CssClass="texto"/>
                                    <asp:HiddenField ID="hdnCodigoMensaje" runat="server" Value='<%# Bind("MENSAJE.MEN_CODIGO") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   <span class="texto"> Mensaje Transaccional</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("MTR_NOMBRE") %>' CssClass="texto"/>
                                    <asp:HiddenField ID="hndCodigoMensajeTransaccional" runat="server" Value='<%# Bind("MTR_CODIGO") %>' />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsMensajeTransaccional" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerMensajeTransaccional" TypeName="BusinessLayer.Mensajeria.MensajeTransaccionalBL">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoMensajeTransaccional" QueryStringField="CodigoMensajeTransaccional"
                            Type="Int32" />
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
                <asp:GridView ID="grvReglaMensajeTransaccional" runat="server" AutoGenerateColumns="False"
                    DataSourceID="dsReglaMensajeTransaccional" ShowFooter="True" OnDataBound="grvReglaMensajeTransaccional_DataBound"
                    DataKeyNames="RMT_CODIGO">
                    <Columns>
                        <asp:TemplateField>
                            <FooterTemplate>
                                <asp:ImageButton ID="imgAgregar" runat="server" ImageUrl="~/Includes/Imagenes/iconNew.png"
                                    OnClick="imgAgregar_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Campo">
                            <FooterTemplate>
                                <asp:DropDownList ID="drlCampoFooter" runat="server" DataSourceID="dsCampo" DataTextField="CAM_NOMBRE"
                                    DataValueField="CAM_CODIGO" AppendDataBoundItems="True">
                                    <asp:ListItem Value="-1">Seleccionar Campo</asp:ListItem>
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="dsCampo" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="obtenerCampoNoSelectorNoAsignadoReglaTransaccional" TypeName="BusinessLayer.Mensajeria.CampoMensajeBL">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="frmMensajeTransaccional$hdnCodigoMensaje" Name="codigoMensaje"
                                            PropertyName="Value" Type="Int32" DefaultValue="-1" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCampoItem" runat="server" Text='<%# Eval("CAMPO.CAM_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor" SortExpression="CAM_VALOR_SELECTOR" ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtValorEdit" runat="server" Text='<%# Bind("RMT_VALOR") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtValorFooter" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblValorItem" runat="server" Text='<%# Bind("RMT_VALOR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TipoDato" SortExpression="TIPO_DATO.TDT_NOMBRE">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoDatoItem" runat="server" Text='<%# Eval("CAMPO.TIPO_DATO.TDT_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgActualizar" runat="server" CommandName="Update" ImageUrl="~/Includes/Imagenes/iconSave.png" />
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
                <asp:ObjectDataSource ID="dsReglaMensajeTransaccional" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerReglaMensajeTransaccionalPorMensajeTransaccional" TypeName="BusinessLayer.Mensajeria.ReglaMensajeTransaccionalBL"
                    OnSelected="dsReglaMensajeTransaccional_Selected" DataObjectTypeName="BusinessEntity.REGLA_MENSAJE_TRANSACCIONAL"
                    UpdateMethod="modificarReglaMensajeTransaccional" OnDeleted="dsReglaMensajeTransaccional_Deleted"
                    OnUpdated="dsReglaMensajeTransaccional_Updated">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="frmMensajeTransaccional$hndCodigoMensajeTransaccional"
                            DefaultValue="-1" Name="codigoMensajeTransaccional" PropertyName="Value" Type="Int32" />
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
            <td style="text-align: center">
                <asp:Button ID="Button1" runat="server" PostBackUrl="~/Mensajeria/MantenimientoMensajeTransaccional/ConsultarMensajeTransaccional.aspx"
                    Text="Cancelar" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
