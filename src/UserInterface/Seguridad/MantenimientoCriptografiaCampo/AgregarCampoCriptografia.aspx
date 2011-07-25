<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="AgregarCampoCriptografia.aspx.cs" Inherits="UserInterface.Seguridad.MantenimientoCriptografiaCampo.AgregarCampoCriptografia" %>

<%@ Import Namespace="BusinessLayer.Seguridad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 150px;
        }
        .tabla
        {
            width: 365px;
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
                <span class="titulo">Agregar Campo Criptografía </span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:FormView ID="frmDinamicaCriptografia" runat="server" DataSourceID="dsDinamicaCriptografia"
                    HorizontalAlign="Center">
                    <ItemTemplate>
                        <table style="margin: auto;" class="tabla_mantenimiento tabla">
                            <tr>
                                <td class="style2">
                                    <span class="texto">Grupo de Mensaje</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblGrupoMensaje" runat="server" Text='<%# Bind("MENSAJE.GRUPO_MENSAJE.GMJ_NOMBRE") %>'
                                        CssClass="texto">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <span class="texto">Mensaje</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblMensaje" runat="server" Text='<%# Bind("MENSAJE.MEN_NOMBRE") %>'
                                        CssClass="texto" />
                                    <asp:HiddenField ID="hdnCodigoMensaje" runat="server" Value='<%# Bind("MENSAJE.MEN_CODIGO") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <span class="texto">Dinamica Criptografia</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblNombreDinamica" runat="server" Text='<%# Bind("DNC_NOMBRE") %>'
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
                <table style="margin: auto;" class="tabla_mantenimiento tabla">
                    <tr>
                        <td class="style2">
                            <span class="texto">Campo Origen</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlCampoOrigen" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" DataSourceID="dsCamposMensaje" DataTextField="CAM_NOMBRE"
                                DataValueField="CAM_CODIGO">
                                <asp:ListItem Value="-1">Seleccionar Campo</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCampoOrigen" runat="server" ControlToValidate="drlCampoOrigen"
                                ErrorMessage="Debe ingresar el Campo Origen" InitialValue="-1" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="dsCamposMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerCampo" TypeName="BusinessLayer.Mensajeria.CampoMensajeBL">
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="-1" Name="codigoMensaje" QueryStringField="Mensaje"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
                <asp:FormView ID="frmCampo" runat="server" DataSourceID="dsCampo" HorizontalAlign="Center">
                    <ItemTemplate>
                        <table style="margin: auto;" class="tabla_mantenimiento tabla">
                            <tr>
                                <td class="style2">
                                    <span class="texto">Longitud</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblLongitudItem" CssClass="texto" runat="server" Text='<%# Bind("CAM_LONGITUD") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <span class="texto">Requerido</span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkRequeridoItem" runat="server" Checked='<%# Bind("CAM_REQUERIDO") %>'
                                        Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <span class="texto">Tipo de Dato</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblTipoDatoItem" CssClass="texto" runat="server" Text='<%# Bind("TIPO_DATO.TDT_NOMBRE") %>' />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsCampo" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerCampo" TypeName="BusinessLayer.Mensajeria.CampoMensajeBL">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoMensaje" QueryStringField="Mensaje"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="drlCampoOrigen" DefaultValue="-1" Name="codigoCampo"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <table style="margin: auto;" class="tabla_mantenimiento tabla">
                    <tr>
                        <td class="style2">
                            <span class="texto">Algoritmo</span></td>
                        <td>
                            <asp:DropDownList ID="drlAlgoritmo" runat="server" AppendDataBoundItems="True" 
                                DataSourceID="dsAlgoritmo" DataTextField="Value" DataValueField="Key">
                                <asp:ListItem Value="-1">Seleccionar Algortimo</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAlgoritmo" runat="server" ControlToValidate="drlAlgoritmo"
                                ErrorMessage="Debe ingresar el Algoritmo" InitialValue="-1" 
                                ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="dsAlgoritmo" runat="server" 
                                OldValuesParameterFormatString="original_{0}" SelectMethod="obtenerTipoLlave" 
                                TypeName="BusinessLayer.Seguridad.AlgoritmoBL"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <span class="texto">Tipo Llave 1</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlTipoLlave1" runat="server" AppendDataBoundItems="True" DataSourceID="dsTipoLlave"
                                DataTextField="Value" DataValueField="Key" AutoPostBack="True">
                                <asp:ListItem Value="-1">Seleccionar Tipo Llave</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvTipoLlave1" runat="server" ControlToValidate="drlTipoLlave1"
                                ErrorMessage="Debe ingresar el Tipo Llave 1" InitialValue="-1" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="dsTipoLlave" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerTipoLlave" TypeName="BusinessLayer.Seguridad.TipoLlaveBL">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
                <%if (this.drlTipoLlave1.SelectedValue == TipoLlaveBL.obtenerCodigoCampo().ToString())
                  { %>
                <div id="divCampoLlave1">
                    <table style="margin: auto;" class="tabla_mantenimiento tabla">
                        <tr>
                            <td class="style2">
                                <span class="texto">Campo Llave 1</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="drlCampoLlave1" runat="server" AppendDataBoundItems="True"
                                    DataSourceID="dsCamposMensaje" DataTextField="CAM_NOMBRE" DataValueField="CAM_CODIGO">
                                    <asp:ListItem Value="-1">Seleccionar Campo</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCampoLlave1" runat="server" ControlToValidate="drlCampoLlave1"
                                    ErrorMessage="Debe ingresar el Campo Llave 1" InitialValue="-1" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <%} %>
                <%if (this.drlTipoLlave1.SelectedValue == TipoLlaveBL.obtenerCodigoLlaveFija().ToString())
                  { %>
                <div id="divLlaveFija1">
                    <table style="margin: auto;" class="tabla_mantenimiento tabla">
                        <tr>
                            <td class="style2">
                                <span class="texto">Llave Fija 1</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLlaveFija1" runat="server" MaxLength="64" Width="170"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLlaveFija1" runat="server" ControlToValidate="txtLlaveFija1"
                                    ErrorMessage="Debe ingresar la Llave Fija 1" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <%} %>
                <table style="margin: auto;" class="tabla_mantenimiento tabla">
                    <tr>
                        <td class="style2">
                            <span class="texto">Segunda Llave</span>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkSegundaLlave" runat="server" AutoPostBack="True" />
                        </td>
                    </tr>
                </table>
                <%if (this.chkSegundaLlave.Checked)
                  { %>
                <div id="divSegundaLlave">
                    <table style="margin: auto;" class="tabla_mantenimiento tabla">
                        <tr>
                            <td class="style2">
                                <span class="texto">Tipo Llave 2</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="drlTipoLlave2" runat="server" AppendDataBoundItems="True" DataSourceID="dsTipoLlave"
                                    DataTextField="Value" DataValueField="Key" AutoPostBack="True">
                                    <asp:ListItem Value="-1">Seleccionar Tipo Llave</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTipoLlave2" runat="server" 
                                    ControlToValidate="drlTipoLlave2" ErrorMessage="Debe ingresar el Tipo Llave 2" 
                                    InitialValue="-1" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <%if (this.drlTipoLlave2.SelectedValue == TipoLlaveBL.obtenerCodigoCampo().ToString())
                      { %>
                    <div id="divCampoLlave2">
                        <table style="margin: auto;" class="tabla_mantenimiento tabla">
                            <tr>
                                <td class="style2">
                                    <span class="texto">Campo Llave 2</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drlCampoLlave2" runat="server" AppendDataBoundItems="True"
                                        DataSourceID="dsCamposMensaje" DataTextField="CAM_NOMBRE" DataValueField="CAM_CODIGO">
                                        <asp:ListItem Value="-1">Seleccionar Campo</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCampoLlave2" runat="server" 
                                        ControlToValidate="drlCampoLlave2" 
                                        ErrorMessage="Debe ingresar el Campo Llave 2" InitialValue="-1" 
                                        ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%} %>
                    <%if (this.drlTipoLlave2.SelectedValue == TipoLlaveBL.obtenerCodigoLlaveFija().ToString())
                      { %>
                    <div id="divLlaveFija2">
                        <table style="margin: auto;" class="tabla_mantenimiento tabla">
                            <tr>
                                <td class="style2">
                                    <span class="texto">Llave Fija 2</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLlaveFija2" runat="server" MaxLength="64" Width="170"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLlaveFija2" runat="server" 
                                        ControlToValidate="txtLlaveFija2" ErrorMessage="Debe ingresar la Llave Fija 2" 
                                        ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%} %>
                    <table style="margin: auto;" class="tabla_mantenimiento tabla">
                        <tr>
                            <td class="style2">
                                <span class="texto">Operación Llave</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="drlOperacionLlave" runat="server" AppendDataBoundItems="True"
                                    DataSourceID="dsOperacionLlave" DataTextField="Value" DataValueField="Key">
                                    <asp:ListItem Value="-1">Seleccionar Operacion Llave</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="drlOperacionLlave" 
                                    ErrorMessage="Debe ingresar la Operación Llave" InitialValue="-1" 
                                    ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                                <asp:ObjectDataSource ID="dsOperacionLlave" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="obtenerOperacionLlave" TypeName="BusinessLayer.Seguridad.OperacionLlaveBL">
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </div>
                <%} %>
                <table style="margin: auto;" class="tabla_mantenimiento  tabla">
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click"
                                ValidationGroup="Grupo" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                                onclick="btnCancelar_Click" />
                        </td>
                    </tr>
                </table>
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
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Se han producido los siguientes errores:"
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Grupo" />

</asp:Content>
