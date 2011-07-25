<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="AgregarCampoPlantilla.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoCampoPlantilla.AgregarCampoPlantilla" %>

<%@ Import Namespace="BusinessLayer.Mensajeria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .columnaIzquierda
        {
            width: 130px;
        }
        .tabla
        {
            width: 400px;
        }
    </style>

    <script language="javascript" type="text/javascript">

        function OcultarMostrarLongitudCabecera() {
            seleccionado = document.getElementById('<%= this.frmCampoPlantilla.FindControl("chkVariable").ClientID %>').checked;
            if (seleccionado) {
                document.getElementById('divVariable').style["display"] = "Block";
            }
            else {
                document.getElementById('divVariable').style["display"] = "None";
            }

        }

        function CambiarSelector() {
            seleccionado = document.getElementById('<%= this.frmCampoPlantilla.FindControl("chkCabecera").ClientID %>').checked;
            if (seleccionado) {
                document.getElementById('divSelector').style["display"] = "Block";
                document.getElementById('divPosicionRelativa').style["display"] = "None";

                if (document.getElementById('<%= this.frmGrupoMensaje.FindControl("hdnTipoMensajeCodigo").ClientID %>').value
                == '<%= TipoMensajeBL.obtenerCodigoBitmap().ToString() %>') {
                    document.getElementById('divBitmap').style["display"] = "Block";
                }

            }
            else {
                document.getElementById('divSelector').style["display"] = "None";
                document.getElementById('divPosicionRelativa').style["display"] = "Block";
                document.getElementById('<%= this.frmCampoPlantilla.FindControl("txtPosicionRelativa").ClientID %>').value = "";
                document.getElementById('<%= this.frmCampoPlantilla.FindControl("chkSelector").ClientID %>').checked = false;

                if (document.getElementById('<%= this.frmGrupoMensaje.FindControl("hdnTipoMensajeCodigo").ClientID %>').value
                == '<%= TipoMensajeBL.obtenerCodigoBitmap().ToString() %>') {
                    document.getElementById('divBitmap').style["display"] = "None";
                    document.getElementById('<%= this.frmCampoPlantilla.FindControl("chkBitmap").ClientID %>').checked = false;
                }

            }

        }

        function ValidarLongitudCabecera(sender, args) {
            args.IsValid = true;
            seleccionado = document.getElementById('<%= this.frmCampoPlantilla.FindControl("chkVariable").ClientID %>').checked;
            if (seleccionado) {
                if (document.getElementById('<%= this.frmCampoPlantilla.FindControl("txtLongitudCabecera").ClientID %>').value == "") {
                    args.IsValid = false;
                }
            }

        }

        function ValidarPosicionRelativa(sender, args) {
            args.IsValid = true;
            seleccionado = document.getElementById('<%= this.frmCampoPlantilla.FindControl("chkCabecera").ClientID %>').checked;
            if (!seleccionado) {
                if (document.getElementById('<%= this.frmCampoPlantilla.FindControl("txtPosicionRelativa").ClientID %>').value == "") {
                    args.IsValid = false;
                }
            }

        }
        
    </script>

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
                <span class="titulo">Agregar Campo Plantilla</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:FormView ID="frmGrupoMensaje" runat="server" DataSourceID="dsGrupoMensaje" HorizontalAlign="Center">
                    <ItemTemplate>
                        <table class="tabla">
                            <tr>
                                <td class="columnaIzquierda">
                                    <span class="texto">Grupo Mensaje</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblNombreGrupoMensaje" runat="server" Text='<%# Bind("GMJ_NOMBRE") %>'
                                        CssClass="texto" />
                                    <asp:HiddenField ID="hdnCodigoGrupoMensaje" Value='<%# Bind("GMJ_CODIGO") %>' runat="server" />
                                    <asp:HiddenField ID="hdnTipoMensajeCodigo" runat="server" Value='<%# Bind("TIPO_MENSAJE.TMJ_CODIGO") %>' />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsGrupoMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerGrupoMensaje" TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="codigo" QueryStringField="CodigoGrupoMensaje" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:FormView ID="frmCampoPlantilla" runat="server" DataSourceID="dsCampoPlantilla"
                    DefaultMode="Insert" HorizontalAlign="Center" OnItemInserting="frmCampoPlantilla_ItemInserting">
                    <InsertItemTemplate>
                        <table class="tabla">
                            <tr>
                                <td class="columnaIzquierda">
                                    <span class="texto">Nombre</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("CMP_NOMBRE") %>' MaxLength="50"
                                        Width="220px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                                        ErrorMessage="Debe ingresar el Nombre" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="columnaIzquierda">
                                    <span class="texto">Cabecera</span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkCabecera" runat="server" Checked='<%# Bind("CMP_CABECERA") %>'
                                        onclick="CambiarSelector();" />
                                </td>
                            </tr>
                        </table>
                        <%if (((HiddenField)this.frmGrupoMensaje.FindControl("hdnTipoMensajeCodigo")).Value == TipoMensajeBL.obtenerCodigoBitmap().ToString())
                          { %>
                        <div id="divBitmap">
                            <table class="tabla">
                                <tr>
                                    <td class="columnaIzquierda">
                                        <span class="texto">Bitmap</span>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkBitmap" runat="server" Checked='<%# Bind("CMP_BITMAP") %>' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <%} %>
                        <div id="divSelector">
                            <table class="tabla">
                                <tr>
                                    <td class="columnaIzquierda">
                                        <span class="texto">Selector</span>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkSelector" runat="server" Checked='<%# Bind("CMP_SELECTOR") %>' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="tabla">
                            <tr>
                                <td class="columnaIzquierda">
                                    <span class="texto">Tipo de Dato</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drlTipoDato" runat="server" DataSourceID="dsTipoDato" DataTextField="TDT_NOMBRE"
                                        DataValueField="TDT_CODIGO" AppendDataBoundItems="True">
                                        <asp:ListItem Value="-1">Seleccionar el Tipo de Dato</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drlTipoDato"
                                        ErrorMessage="Debe ingresar el Tipo de Dato" InitialValue="-1" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                                    <asp:ObjectDataSource ID="dsTipoDato" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="obtenerTipoDato" TypeName="BusinessLayer.Mensajeria.TipoDatoBL">
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                        <%if (((HiddenField)this.frmGrupoMensaje.FindControl("hdnTipoMensajeCodigo")).Value == TipoMensajeBL.obtenerCodigoBitmap().ToString())
                          { %>
                        <table class="tabla">
                            <tr>
                                <td class="columnaIzquierda">
                                    <span class="texto">Variable</span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkVariable" runat="server" Checked='<%# Bind("CMP_VARIABLE") %>'
                                        onclick="OcultarMostrarLongitudCabecera();" />
                                </td>
                            </tr>
                        </table>
                        <%} %>
                        <%if (((HiddenField)this.frmGrupoMensaje.FindControl("hdnTipoMensajeCodigo")).Value == TipoMensajeBL.obtenerCodigoBitmap().ToString())
                          { %>
                        <div id="divVariable">
                            <table class="tabla">
                                <tr>
                                    <td class="columnaIzquierda">
                                        <span class="texto">Longitud Cabecera</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLongitudCabecera" runat="server" MaxLength="3" Width="70px" Text='<%# Bind("CMP_LONGITUD_CABECERA") %>'></asp:TextBox>
                                        &nbsp;<span class="texto">bytes</span>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidarLongitudCabecera"
                                            ErrorMessage="Debe ingresar la Longitud Cabecera" ValidationGroup="Grupo">*</asp:CustomValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <%} %>
                        <table class="tabla">
                            <tr>
                                <td class="columnaIzquierda">
                                    <span class="texto">Longitud Cuerpo</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLongitud" runat="server" Text='<%# Bind("CMP_LONGITUD") %>' MaxLength="4"
                                        Width="70px" />&nbsp;<span class="texto"> bytes</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLongitud"
                                        ErrorMessage="Debe ingresar la Longitud" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="columnaIzquierda">
                                    <span class="texto">Transaccional</span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkTransaccional" runat="server" Checked='<%# Bind("CMP_TRANSACCIONAL") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td class="columnaIzquierda">
                                    <span class="texto">Protegido por Log</span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkProtegidoLog" runat="server" Checked='<%# Bind("CMP_PROTEGIDO_LOG") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td class="columnaIzquierda">
                                    <span class="texto">Almacenado</span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkAlmacenado" runat="server" Checked='<%# Bind("CMP_ALMACENADO") %>' />
                                </td>
                            </tr>
                        </table>
                        <div id="divPosicionRelativa">
                            <table class="tabla">
                                <tr>
                                    <td class="columnaIzquierda">
                                        <span class="texto">Posición Relativa</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPosicionRelativa" runat="server" MaxLength="3" Width="70px" Text='<%# Bind("CMP_POSICION_RELATIVA") %>'></asp:TextBox>
                                        <asp:CustomValidator ID="cvlPosicionRelativa" runat="server" ClientValidationFunction="ValidarPosicionRelativa"
                                            ErrorMessage="Debe ingresar la Posición Relativa" ValidationGroup="Grupo">*</asp:CustomValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="tabla">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnAgregar" runat="server" Text="Aceptar" CommandName="Insert" ValidationGroup="Grupo" />
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" PostBackUrl="~/Mensajeria/MantenimientoCampoPlantilla/ConsultarCampoPlantilla.aspx" />
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsCampoPlantilla" runat="server" DataObjectTypeName="BusinessEntity.CAMPO_PLANTILLA"
                    InsertMethod="insertarCampoPlantilla" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerCampoPlantilla" TypeName="BusinessLayer.Mensajeria.CampoPlantillaBL"
                    OnInserted="dsGrupoMensaje_Inserted" OnInserting="dsGrupoMensaje_Inserting">
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
                <asp:ValidationSummary ID="vlsProtocolo" runat="server" HeaderText="Se han producido los siguientes errores"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Grupo" />
            </td>
        </tr>
    </table>
    <% if (((HiddenField)this.frmGrupoMensaje.FindControl("hdnTipoMensajeCodigo")).Value == TipoMensajeBL.obtenerCodigoBitmap().ToString())
       { %>

    <script language="javascript" type="text/javascript">
        OcultarMostrarLongitudCabecera();
        document.getElementById('<%= this.frmCampoPlantilla.FindControl("txtNombre").ClientID %>').focus();
    </script>

    <%} %>

    <script language="javascript" type="text/javascript">
        CambiarSelector();
    </script>

</asp:Content>
