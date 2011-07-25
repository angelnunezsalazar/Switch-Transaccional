using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLayer.Mensajeria;

namespace UserInterface.Mensajeria.ValoresTabla
{
    public partial class ConsultarValoresTabla : System.Web.UI.Page
    {
        public static List<string> listaDeColumnas;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (this.drlTabla.SelectedValue != "-1")
                {
                    GenerarTabla();
                }

            }
        }

        protected void drlGrupoMensaje_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GenerarTabla()
        {
            List<COLUMNA> listaColumna = ColumnaBL.obtenerColumna(int.Parse(this.drlTabla.SelectedValue));

            if (listaColumna == null || listaColumna.Count == 0)
            {
                return;
            }

            DataTable dataTable = TablaBL.ObtenerValoresTabla(this.drlTabla.SelectedItem.Text);

            listaDeColumnas = new List<string>();

            grvValoresTabla.Columns.Clear();

            TemplateField newField = new TemplateField();
            newField.FooterTemplate = new GridViewTemplate(ListItemType.Footer, EnumInfoType.Agregar);
            grvValoresTabla.Columns.Add(newField);

            foreach (COLUMNA columna in listaColumna)
            {
                listaDeColumnas.Add(columna.COL_NOMBRE);

                TemplateField ItemTmpField = new TemplateField();

                ItemTmpField.HeaderTemplate = new GridViewTemplate(ListItemType.Header,
                                                  columna.COL_NOMBRE, columna.TIPO_DATO_COLUMNA.TDC_NOMBRE,
                                                  EnumInfoType.Campo);

                ItemTmpField.ItemTemplate = new GridViewTemplate(ListItemType.Item,
                                                              columna.COL_NOMBRE,
                                                              EnumInfoType.Campo);

                ItemTmpField.EditItemTemplate = new GridViewTemplate(ListItemType.EditItem,
                                                              columna.COL_NOMBRE, columna.COL_LONGITUD,
                                                              EnumInfoType.Campo);

                ItemTmpField.FooterTemplate = new GridViewTemplate(ListItemType.Footer,
                                                              columna.COL_NOMBRE, columna.COL_LONGITUD,
                                                              EnumInfoType.Campo);

                grvValoresTabla.Columns.Add(ItemTmpField);
            }

            if (dataTable.Rows.Count == 0)
            {
                DataRow row = dataTable.NewRow();
                row[listaDeColumnas[0]] = "";
                dataTable.Rows.Add(row);
            }
            else
            {
                TemplateField EditField = new TemplateField();
                EditField.ItemTemplate = new GridViewTemplate(ListItemType.Item, EnumInfoType.Editar);
                EditField.EditItemTemplate = new GridViewTemplate(ListItemType.EditItem, EnumInfoType.Editar);
                grvValoresTabla.Columns.Add(EditField);

                TemplateField DeleteField = new TemplateField();
                DeleteField.ItemTemplate = new GridViewTemplate(ListItemType.Item, EnumInfoType.Eliminar);
                DeleteField.EditItemTemplate = new GridViewTemplate(ListItemType.EditItem, EnumInfoType.Eliminar);
                grvValoresTabla.Columns.Add(DeleteField);
            }

            grvValoresTabla.DataSource = dataTable;
            string[] dataKeys = new string[1];
            dataKeys[0] = dataTable.Columns[0].ColumnName;
            grvValoresTabla.DataKeyNames = dataKeys;
            grvValoresTabla.DataBind();
            //ViewState["IsConnectionInfoSet"] = true;
        }

        protected void grvValoresTabla_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvValoresTabla.EditIndex = e.NewEditIndex;
            grvValoresTabla.DataBind();
        }

        protected void grvValoresTabla_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvValoresTabla.EditIndex = -1;
            grvValoresTabla.DataBind();
        }

        protected void grvValoresTabla_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Agregar")
            {
                List<string> listaDeValores = new List<string>();

                foreach (var nombreColumna in listaDeColumnas)
                {
                    string valor = ((TextBox)this.grvValoresTabla.FooterRow.FindControl
                        (GridViewTemplate.IdFooter(nombreColumna))).Text;
                    listaDeValores.Add(valor);
                }

                EstadoOperacion resultado = TablaBL.insertarValoresTabla(drlTabla.SelectedItem.Text,
                    listaDeColumnas, listaDeValores);

                if (resultado.Estado)
                {
                    GenerarTabla();
                }
                else
                {
                    lblMensaje.Text = resultado.Mensaje;
                }
            }
        }

        protected void grvValoresTabla_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex;

            int id = int.Parse(this.grvValoresTabla.DataKeys[index].Value.ToString());

            EstadoOperacion resultado = TablaBL.eliminarValoresTabla(drlTabla.SelectedItem.Text, id);

            if (resultado.Estado)
            {
                grvValoresTabla.EditIndex = -1;
                GenerarTabla();
            }
            else
            {
                lblMensaje.Text = resultado.Mensaje;
            }


        }

        protected void grvValoresTabla_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = int.Parse(this.grvValoresTabla.DataKeys[e.RowIndex].Value.ToString());

            GridViewRow row = this.grvValoresTabla.Rows[e.RowIndex];

            List<string> listaDeValores = new List<string>();

            foreach (var nombreColumna in listaDeColumnas)
            {
                string valor = ((TextBox)row.FindControl
                    (GridViewTemplate.IdEdit(nombreColumna))).Text;

                listaDeValores.Add(valor);
            }

            EstadoOperacion resultado = TablaBL.modificarValoresTabla(drlTabla.SelectedItem.Text,
                listaDeColumnas, listaDeValores, id);

            if (resultado.Estado)
            {
                grvValoresTabla.EditIndex = -1;
                GenerarTabla();
            }
            else
            {
                lblMensaje.Text = resultado.Mensaje;
            }
        }
    }
}
