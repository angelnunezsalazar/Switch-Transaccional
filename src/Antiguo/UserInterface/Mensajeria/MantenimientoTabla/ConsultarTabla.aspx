<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarTabla.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoTabla.ConsultarTabla" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <span class="titulo">Consultar Tablas</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grvTabla" runat="server" AutoGenerateColumns="False" DataSourceID="dsTabla"
                    ShowFooter="True" DataKeyNames="TBL_CODIGO" OnDataBound="grvTabla_DataBound"
                    HorizontalAlign="Center">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <FooterTemplate>
                                <asp:ImageButton ID="imgAgregar" CommandName="Insert" runat="server" ImageUrl="~/Includes/Imagenes/iconNew.png"
                                    OnClick="imgAgregar_Click" ValidationGroup="NuevaTabla" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNombreEdit" runat="server" Text='<%# Bind("TBL_NOMBRE") %>' MaxLength="50"
                                    Width="150px" ></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtNombreEdit_FilteredTextBoxExtender" 
                                    runat="server" FilterType="LowercaseLetters,UppercaseLetters" TargetControlID="txtNombreEdit">
                                </cc1:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombreEdit"
                                    ErrorMessage="Debe ingresar el Nombre" ValidationGroup="ModificarTabla">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtNombre_FilteredTextBoxExtender" 
                                    runat="server" FilterType="LowercaseLetters,UppercaseLetters" TargetControlID="txtNombre">
                                </cc1:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                                    ErrorMessage="Debe ingresar el Nombre" ValidationGroup="NuevaTabla">*</asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNombreTabla" runat="server" Text='<%# Eval("TBL_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripci&oacute;n">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescripcionEdit" runat="server" Text='<%# Bind("TBL_DESCRIPCION") %>'
                                    MaxLength="200" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDescripcionEdit"
                                    ErrorMessage="Debe ingresar la Descripci&oacute;n" ValidationGroup="ModificarTabla">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="200" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDescripcion"
                                    ErrorMessage="Debe ingresar la Descripci&oacute;n" ValidationGroup="NuevaTabla">*</asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDescripcionTabla" runat="server" Text='<%# Eval("TBL_DESCRIPCION") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgCampos" runat="server" PostBackUrl='<%# "~/Mensajeria/MantenimientoColumna/ConsultarColumna.aspx?Tabla="+ Eval("TBL_CODIGO") %>'
                                    AlternateText="Ver" ImageUrl="~/Includes/Imagenes/iconFind.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgActualizar" runat="server" CommandName="Update" ImageUrl="~/Includes/Imagenes/iconSave.png"
                                    ValidationGroup="ModificarTabla" />
                                <asp:ImageButton ID="imgCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Includes/Imagenes/iconCancel.png" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" runat="server" CommandName="Edit" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEliminar" runat="server" CommandName="Delete" ImageUrl="~/Includes/Imagenes/iconErase.png"
                                    OnClientClick="return confirm('¿Está seguro de que quiere eliminar la Tabla?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsTabla" runat="server" DataObjectTypeName="BusinessEntity.TABLA"
                    DeleteMethod="eliminarTabla" OldValuesParameterFormatString="original_{0}" SelectMethod="obtenerTabla"
                    TypeName="BusinessLayer.Mensajeria.TablaBL" UpdateMethod="modificarTabla" OnDeleted="dsTabla_Deleted"
                    OnUpdated="dsTabla_Updated" OnSelected="dsTabla_Selected"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="vlsNuevaTabla" runat="server" ValidationGroup="NuevaTabla"
        ShowMessageBox="True" ShowSummary="False" HeaderText="Se han producido los siguientes errores:" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="ModificarTabla" HeaderText="Se han producido los siguientes errores:" />
</asp:Content>
