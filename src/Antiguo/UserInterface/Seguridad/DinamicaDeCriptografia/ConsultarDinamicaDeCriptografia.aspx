<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarDinamicaDeCriptografia.aspx.cs" Inherits="UserInterface.Seguridad.DinamicaDeCriptografia.ConsultarDinamicaDeCriptografia" %>

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
                <span class="titulo">Consultar Dinamica de Criptografía</span>
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
                <asp:GridView ID="grvDinamicaCriptografia" runat="server" DataSourceID="dsDinamicaCriptografia"
                    AutoGenerateColumns="False" ShowFooter="True" OnDataBound="grvDinamicaCriptografia_DataBound" DataKeyNames="DNC_CODIGO">
                    <Columns>
                        <asp:TemplateField>
                            <FooterTemplate>
                                <asp:ImageButton ID="imgAgregar" runat="server" ImageUrl="~/Includes/Imagenes/iconNew.png"
                                    OnClick="imgAgregar_Click" ValidationGroup="Grupo1"/>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre" SortExpression="DNC_NOMBRE">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNombreEdit" runat="server" Text='<%# Bind("DNC_NOMBRE") %>' MaxLength="50" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombreEdit" runat="server" ControlToValidate="txtNombreEdit"
                                    ErrorMessage="Debe ingresar el Nombre" ValidationGroup="Grupo2">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNombreFooter" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombreFooter" runat="server" ControlToValidate="txtNombreFooter"
                                    ErrorMessage="Debe ingresar el Nombre" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNombreItem" runat="server" Text='<%# Eval("DNC_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo" SortExpression="DNC_TIPO">
                            <EditItemTemplate>
                                <asp:DropDownList ID="drlTipoEdit" runat="server" SelectedValue='<%# Bind("DNC_TIPO") %>'>
                                    <asp:ListItem Value="-1">Seleccionar Tipo Criptografia</asp:ListItem>
                                    <asp:ListItem Value="1">Encriptación</asp:ListItem>
                                    <asp:ListItem Value="2">Desencriptacion</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTipoEdit" runat="server" ControlToValidate="drlTipoEdit"
                                    ErrorMessage="Debe ingresar el Tipo Criptografia" InitialValue="-1" ValidationGroup="Grupo2">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="drlTipoFooter" runat="server">
                                    <asp:ListItem Value="-1">Seleccionar Tipo Criptografia</asp:ListItem>
                                    <asp:ListItem Value="1">Encriptación</asp:ListItem>
                                    <asp:ListItem Value="2">Desencriptación</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTipoFooter" runat="server" ControlToValidate="drlTipoFooter"
                                    ErrorMessage="Debe ingresar el Tipo Criptografia" InitialValue="-1" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTipoItem" runat="server" Text='<%# String.Equals(Eval("DNC_TIPO").ToString(),"1")?"Encriptación":"Desencriptación" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" PostBackUrl='<%# "~/Seguridad/MantenimientoCriptografiaCampo/ConsultarCampoCriptografia.aspx?Dinamica="+ Eval("DNC_CODIGO") %>'
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
                                    ImageUrl="~/Includes/Imagenes/iconErase.png" OnClientClick="return confirm('Esta seguro que quiere eliminar la Dinamica Criptografía ?')"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsDinamicaCriptografia" runat="server" DataObjectTypeName="BusinessEntity.DINAMICA_CRIPTOGRAFIA"
                    DeleteMethod="eliminarDinamicaCriptografia" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerDinamicaCriptografiaPorMensaje" 
                    TypeName="BusinessLayer.Seguridad.DinamicaCriptografiaBL" 
                    onselected="dsDinamicaCriptografia_Selected" 
                    onselecting="dsDinamicaCriptografia_Selecting" 
                    UpdateMethod="modificarDinamicaCriptografia" 
                    ondeleted="dsDinamicaCriptografia_Deleted" 
                    onupdated="dsDinamicaCriptografia_Updated">
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
