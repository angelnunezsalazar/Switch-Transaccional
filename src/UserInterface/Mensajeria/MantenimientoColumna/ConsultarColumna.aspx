<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarColumna.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoColumna.ConsultarColumna" %>

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
                <span class="titulo">Consultar Columna</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:FormView ID="frmTabla" runat="server" DataSourceID="dsTabla" HorizontalAlign="Center">
        <ItemTemplate>
            <table class="style1">
                <tr>
                    <td>
                        <span class="texto">Tabla</span>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="lblTabla" runat="server" Text='<%# Eval("TBL_NOMBRE") %>' CssClass="texto">
                        </asp:Label>
                        <asp:HiddenField ID="hdnCodigo" runat="server" Value='<%# Bind("TBL_CODIGO") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto">Descripción</span>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("TBL_DESCRIPCION") %>'
                            CssClass="texto">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="dsTabla" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="obtenerTablaPorCodigo" TypeName="BusinessLayer.Mensajeria.TablaBL">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="codigoTabla" QueryStringField="Tabla"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table class="style1">
        <tr>
            <td>
                <asp:GridView ID="grvColumna" runat="server" DataSourceID="dsColumna" AutoGenerateColumns="False"
                    ShowFooter="True" OnDataBound="grvcolumna_DataBound" DataKeyNames="COL_CODIGO">
                    <Columns>
                        <asp:TemplateField>
                            <FooterTemplate>
                                <asp:ImageButton ID="imgAgregar" runat="server" ImageUrl="~/Includes/Imagenes/iconNew.png"
                                    ValidationGroup="Grupo1" OnClick="imgAgregar_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre" SortExpression="COL_NOMBRE">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNombreEdit" runat="server" Text='<%# Bind("COL_NOMBRE") %>' MaxLength="50"
                                    Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombreEdit" runat="server" ControlToValidate="txtNombreEdit"
                                    ErrorMessage="Debe ingresar el Nombre" ValidationGroup="Grupo2">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revNombreEdit" runat="server" ControlToValidate="txtNombreEdit"
                                    ErrorMessage="Debe ingresar un Nombre válido [a-b;A-z]" ValidationExpression="\w*"
                                    ValidationGroup="Grupo2">*</asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNombreFooter" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombreFooter" runat="server" ControlToValidate="txtNombreFooter"
                                    ErrorMessage="Debe ingresar el Nombre" ValidationGroup="Grupo1" Display="Dynamic">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revNombreFooter" runat="server" ControlToValidate="txtNombreFooter"
                                    Display="Static" ErrorMessage="Debe ingresar un Nombre válido [a-b;A-z]" ValidationExpression="\w*"
                                    ValidationGroup="Grupo1">*</asp:RegularExpressionValidator>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNombreItem" runat="server" Text='<%# Eval("COL_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Longitud" SortExpression="COL_LONGITUD">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLongitudEdit" runat="server" Text='<%# Bind("COL_LONGITUD") %>'
                                    MaxLength="3" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLongitudEdit" runat="server" ControlToValidate="txtLongitudEdit"
                                    ErrorMessage="Debe ingresar la Longitud" ValidationGroup="Grupo2">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtLongitudFooter" runat="server" MaxLength="3" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLongitudFooter" runat="server" ControlToValidate="txtLongitudFooter"
                                    ErrorMessage="Debe ingresar la Longitud" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLongitudItem" runat="server" Text='<%# Eval("COL_LONGITUD") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo Dato">
                            <EditItemTemplate>
                                <asp:DropDownList ID="drlTipoEdit" runat="server" DataSourceID="dsTipoDato" DataTextField="TDC_NOMBRE"
                                    DataValueField="TDC_CODIGO" SelectedValue='<%# Eval("TIPO_DATO_COLUMNA.TDC_CODIGO") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Value="-1">Seleccionar Tipo Dato</asp:ListItem>
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="dsTipoDato" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="obtenerTipoDatoColumna" TypeName="BusinessLayer.Mensajeria.TipoDatoColumnaBL">
                                </asp:ObjectDataSource>
                                <asp:RequiredFieldValidator ID="rfvTipoEdit" runat="server" ControlToValidate="drlTipoEdit"
                                    ErrorMessage="Debe ingresar el Tipo Dato Columna" InitialValue="-1" ValidationGroup="Grupo2">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="drlTipoFooter" runat="server" DataSourceID="dsTipoDato" DataTextField="TDC_NOMBRE"
                                    DataValueField="TDC_CODIGO" AppendDataBoundItems="true">
                                    <asp:ListItem Value="-1">Seleccionar Tipo Dato</asp:ListItem>
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="dsTipoDato" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="obtenerTipoDatoColumna" TypeName="BusinessLayer.Mensajeria.TipoDatoColumnaBL">
                                </asp:ObjectDataSource>
                                <asp:RequiredFieldValidator ID="rfvTipoFooter" runat="server" ControlToValidate="drlTipoFooter"
                                    ErrorMessage="Debe ingresar el Tipo Dato Columna" InitialValue="-1" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTipoItem" runat="server" Text='<%# Eval("TIPO_DATO_COLUMNA.TDC_NOMBRE") %>'></asp:Label>
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
                                    ImageUrl="~/Includes/Imagenes/iconErase.png" OnClientClick="return confirm('Esta seguro que quiere eliminar la Columna ?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsColumna" runat="server" DataObjectTypeName="BusinessEntity.COLUMNA"
                    DeleteMethod="eliminarColumna" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerColumna" TypeName="BusinessLayer.Mensajeria.ColumnaBL" OnSelected="dsColumna_Selected"
                    UpdateMethod="modificarColumna" OnDeleted="dsColumna_Deleted" OnUpdated="dsColumna_Updated"
                    OnUpdating="dsColumna_Updating">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoTabla" QueryStringField="Tabla"
                            Type="Int32" />
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
        <tr>
            <td style="text-align: center">
                <asp:ValidationSummary ID="Summary" runat="server" HeaderText="Se han producido los siguientes errores"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Grupo2" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Se han producido los siguientes errores"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Grupo1" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button runat="server" PostBackUrl="~/Mensajeria/MantenimientoTabla/ConsultarTabla.aspx" Text="Regresar"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
