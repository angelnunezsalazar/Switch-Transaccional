<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarCampoCriptografia.aspx.cs" Inherits="UserInterface.Seguridad.MantenimientoCriptografiaCampo.ConsultarCampoCriptografia" %>

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
                <span class="titulo">Consultar Campo Criptografía</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:FormView ID="frmDinamicaCriptografia" runat="server" DataSourceID="dsDinamicaCriptografia"
        HorizontalAlign="Center">
        <ItemTemplate>
            <table class="style1">
                <tr>
                    <td>
                        <span class="texto">Grupo de Mensaje</span>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="lblGrupoMensaje" runat="server" Text='<%# Bind("MENSAJE.GRUPO_MENSAJE.GMJ_NOMBRE") %>'
                            CssClass="texto">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto">Mensaje</span>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="lblMensaje" runat="server" Text='<%# Bind("MENSAJE.MEN_NOMBRE") %>'
                            CssClass="texto" />
                        <asp:HiddenField ID="hdnCodigoMensaje" runat="server" Value='<%# Bind("MENSAJE.MEN_CODIGO") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto">Dinamica Criptografia</span>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="lblNombreDinamica" runat="server" Text='<%# Bind("DNC_NOMBRE") %>'
                            CssClass="texto" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="dsDinamicaCriptografia" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="obtenerDinamicaCriptografia" TypeName="BusinessLayer.Seguridad.DinamicaCriptografiaBL">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="codigoDinamicaCriptografia" QueryStringField="Dinamica"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table class="style1">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" DataSourceID="dsCampoCriptografia" AutoGenerateColumns="False"
                    DataKeyNames="CRC_CODIGO,DNC_CODIGO">
                    <Columns>
                        <asp:TemplateField HeaderText="Campo Origen">
                            <ItemTemplate>
                                <asp:Label ID="lblCampoOrigenItem" runat="server" Text='<%# Eval("CAMPO_RESULTADO.CAM_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Algoritmo" SortExpression="CRC_ALGORITMO">
                            <ItemTemplate>
                                <asp:Label ID="lblAlgoritmo" runat="server" Text='<%# ObtenerDescripcionAlgoritmo(Eval("CRC_ALGORITMO").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo Llave 1" SortExpression="CRC_TIPO_LLAVE_1">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoLlave1" runat="server" Text='<%# ObtenerDescripcionTipoLlave(Eval("CRC_TIPO_LLAVE_1").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Campo Llave 1" SortExpression="CAMPO_LLAVE_1.CAM_NOMBRE">
                            <ItemTemplate>
                                <asp:Label ID="lblCampoLlave1" runat="server" Text='<%# Eval("CAMPO_LLAVE_1.CAM_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CRC_LLAVE_1" HeaderText="Llave Fija 1" SortExpression="CRC_LLAVE_1" />
                        <asp:TemplateField HeaderText="Tipo Llave 2" SortExpression="CRC_TIPO_LLAVE_2">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoLlave2" runat="server" Text='<%# ObtenerDescripcionTipoLlave((Eval("CRC_TIPO_LLAVE_2")??"").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Campo Llave 2" SortExpression="CAMPO_LLAVE_1.CAM_NOMBRE">
                            <ItemTemplate>
                                <asp:Label ID="lblCampoLlave2" runat="server" Text='<%# Eval("CAMPO_LLAVE_2.CAM_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CRC_LLAVE_2" HeaderText="Llave Fija 2" SortExpression="CRC_LLAVE_2" />
                        <asp:TemplateField HeaderText="Operacion" SortExpression="CRC_TIPO_LLAVE_2">
                            <ItemTemplate>
                                <asp:Label ID="lblOperacion" runat="server" Text='<%# ObtenerDescripcionOperacionLlave((Eval("CRC_OPERACION_LLAVE")??"").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgModificar" runat="server" PostBackUrl='<%# "~/Seguridad/MantenimientoCriptografiaCampo/ModificarCampoCriptografia.aspx?Dinamica="+ Eval("DNC_CODIGO")+"&Mensaje="+ObtenerCodigoMensaje()+"&Campo="+Eval("CRC_CODIGO") %>'
                                    AlternateText="Modificar" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEliminar" runat="server" CommandName="Delete" AlternateText="Eliminar"
                                    ImageUrl="~/Includes/Imagenes/iconErase.png" OnClientClick="return confirm('Esta seguro que quiere eliminar el Campo Critografía?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsCampoCriptografia" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerCriptografiaCampo" TypeName="BusinessLayer.Seguridad.CriptografiaCampoBL"
                    OnDeleted="dsCampoCriptografia_Deleted">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoDinamicaCriptografia" QueryStringField="Dinamica"
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
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                &nbsp;
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" />
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
            </td>
        </tr>
    </table>
</asp:Content>
