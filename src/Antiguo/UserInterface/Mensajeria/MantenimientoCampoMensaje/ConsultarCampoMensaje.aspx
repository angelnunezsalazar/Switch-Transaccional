<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarCampoMensaje.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoCampoMensaje.ConsultarCampoMensaje" %>

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
                <span class="titulo">Consultar Campo Mensaje </span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:FormView ID="frmMensaje" runat="server" DataSourceID="dsMensaje" HorizontalAlign="Center">
        <ItemTemplate>
            <table width="300px">
                <tr>
                    <td>
                        <span class="texto">Grupo de Mensaje</span>
                    </td>
                    <td>
                        <asp:Label ID="lblGrupoMensaje" CssClass="texto" runat="server" Text='<%# Bind("GRUPO_MENSAJE.GMJ_NOMBRE") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto">Mensaje</span>
                    </td>
                    <td>
                        <asp:Label ID="lblNombreMensaje" CssClass="texto" runat="server" Text='<%# Bind("MEN_NOMBRE") %>' />
                        <asp:HiddenField ID="hdnCodigoMensaje" runat="server" Value='<%# Bind("MEN_CODIGO") %>' />
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
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="dsMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="obtenerMensaje" TypeName="BusinessLayer.Mensajeria.MensajeBL">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="codigo" QueryStringField="Codigo"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table class="style2">
        <tr>
            <td>
                <asp:GridView ID="grvCampoCabecera" runat="server" AutoGenerateColumns="False" DataSourceID="dsCampoCabecera"
                    DataKeyNames="CAM_CODIGO,MEN_CODIGO" OnDataBound="grvCampo_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Nombre" SortExpression="CAM_NOMBRE">
                            <ItemTemplate>
                                <asp:Label ID="lblNombreItem" runat="server" Text='<%# Eval("CAM_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Requerido" SortExpression="CAM_REQUERIDO">
                            <ItemTemplate>
                                <asp:Label ID="lblRequeridoItem" runat="server" Text='<%# Requerido(Eval("CAM_REQUERIDO").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Longitud" SortExpression="CAM_LONGITUD">
                            <ItemTemplate>
                                <asp:Label ID="lblLongitudItem" runat="server" Text='<%# Bind("CAM_LONGITUD") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Selector" SortExpression="TIPO_DATO">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectorItem" runat="server" Checked='<%# Eval("CAM_SELECTOR") %>'
                                    Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TipoDato" SortExpression="TIPO_DATO">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoDatoItem" runat="server" Text='<%# Eval("TIPO_DATO.TDT_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsCampoCabecera" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerCampoCabecera" TypeName="BusinessLayer.Mensajeria.CampoMensajeBL"
                    DataObjectTypeName="BusinessEntity.CAMPO">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="codigoMensaje" QueryStringField="Codigo" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <asp:GridView ID="grvCampo" runat="server" AutoGenerateColumns="False" DataSourceID="dsCampo"
                    DataKeyNames="CAM_CODIGO,MEN_CODIGO" ShowFooter="True" OnDataBound="grvCampo_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Posicion" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblPosicionRelativa" runat="server" Text='<%# Eval("CAM_POSICION_RELATIVA") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/Includes/Imagenes/iconNew.png"
                                    OnClick="btnNuevo_Click" ValidationGroup="Grupo" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre" SortExpression="CAM_NOMBRE">
                            <ItemTemplate>
                                <asp:Label ID="lblNombreItem" runat="server" Text='<%# Eval("CAM_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="txtNombreEdit" runat="server" Text='<%# Eval("CAM_NOMBRE") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="drlCampoPlantillaFooter" runat="server" AppendDataBoundItems="True"
                                    DataSourceID="dsCampoPlantilla" DataTextField="CMP_NOMBRE" DataValueField="CMP_CODIGO">
                                    <asp:ListItem Value="-1">Seleccionar Campo Plantilla</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe seleccionar el Campo Plantilla"
                                    ControlToValidate="drlCampoPlantillaFooter" InitialValue="-1" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                                <asp:ObjectDataSource ID="dsCampoPlantilla" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="obtenerCampoPlantillaNoAsignadosMensaje" TypeName="BusinessLayer.Mensajeria.CampoPlantillaBL">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="frmMensaje$hdnCodigoMensaje" DefaultValue="-1" Name="codigoMensaje"
                                            PropertyName="Value" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Requerido" SortExpression="CAM_REQUERIDO">
                            <ItemTemplate>
                                <asp:Label ID="lblRequeridoItem" runat="server" Text='<%# Requerido(Eval("CAM_REQUERIDO").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:RadioButtonList ID="rblRequeridoEdit" runat="server" RepeatDirection="Horizontal"
                                    SelectedValue='<%# Bind("CAM_REQUERIDO") %>'>
                                    <asp:ListItem Value="True">Requerido</asp:ListItem>
                                    <asp:ListItem Value="False">Opcional</asp:ListItem>
                                </asp:RadioButtonList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:RadioButtonList ID="rblRequeridoFooter" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="True" Selected="True">Requerido</asp:ListItem>
                                    <asp:ListItem Value="False">Opcional</asp:ListItem>
                                </asp:RadioButtonList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:CheckBoxField DataField="CAM_VARIABLE" HeaderText="Variable" />
                        <asp:BoundField DataField="CAM_LONGITUD_CABECERA" HeaderText=" Longitud Cabecera"/>
                        <asp:TemplateField HeaderText="Longitud Cuerpo" SortExpression="CAM_LONGITUD">
                            <ItemTemplate>
                                <asp:Label ID="lblLongitudItem" runat="server" Text='<%# Bind("CAM_LONGITUD") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblLongitudEdit" runat="server" Text='<%# Bind("CAM_LONGITUD") %>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TipoDato" SortExpression="TIPO_DATO">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoDatoItem" runat="server" Text='<%# Eval("TIPO_DATO.TDT_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgActualizar" runat="server" CommandName="Update" ImageUrl="~/Includes/Imagenes/iconSave.png" />
                                <asp:ImageButton ID="imgCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Includes/Imagenes/iconCancel.png" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" runat="server" CommandName="Edit" AlternateText="Editar"
                                    ImageUrl="~/Includes/Imagenes/iconEdit.png" Visible='<%# (bool)Eval("CAM_SELECTOR")?false:true %>' />
                                <asp:Image ID="imgBlancoEditar" runat="server" ImageUrl="~/Includes/Imagenes/blank.gif"
                                    Visible='<%# (bool)Eval("CAM_SELECTOR")?true:false %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEliminar" runat="server" CommandName="Delete" AlternateText="Eliminar"
                                    ImageUrl="~/Includes/Imagenes/iconErase.png" Visible='<%# (bool)Eval("CAM_SELECTOR")?false:true %>' />
                                <asp:Image ID="imgBlancoEliminar" runat="server" ImageUrl="~/Includes/Imagenes/blank.gif"
                                    Visible='<%# (bool)Eval("CAM_SELECTOR")?true:false %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsCampo" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerCampoCuerpo" TypeName="BusinessLayer.Mensajeria.CampoMensajeBL"
                    OnSelected="dsCampo_Selected" DataObjectTypeName="BusinessEntity.CAMPO" DeleteMethod="eliminarCampo"
                    UpdateMethod="modificarCampo" ondeleted="dsCampo_Deleted">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="codigoMensaje" QueryStringField="Codigo" Type="Int32" />
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
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
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
                <asp:ValidationSummary ID="vlsProtocolo" runat="server" HeaderText="Se han producido los siguientes errores"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Grupo" />
            </td>
        </tr>
    </table>
</asp:Content>
