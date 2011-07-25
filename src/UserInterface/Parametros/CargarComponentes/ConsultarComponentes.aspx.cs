using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLayer.Parametros;
using Utilidades;

namespace UserInterface.Parametros.CargarComponentes
{
    public partial class ConsultarComponentes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void dsComponente_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            List<COMPONENTE> lista = (List<COMPONENTE>)e.ReturnValue;
            if (lista.Count == 0)
            {
                COMPONENTE componente = new COMPONENTE();
                componente.COM_NOMBRE = "";
                lista.Add(componente);
            }

        }


        protected void grvComponente_DataBound(object sender, EventArgs e)
        {
            if (this.grvComponente.Rows.Count > 0)
            {
                if (this.grvComponente.Rows[0].RowState == DataControlRowState.Normal)
                {
                    if (((Label)this.grvComponente.Rows[0].FindControl("lblNombreItem")).Text == "")
                    {
                        this.grvComponente.Columns[3].Visible = false;
                        this.grvComponente.Columns[4].Visible = false;
                    }
                    else
                    {
                        this.grvComponente.Columns[3].Visible = true;
                        this.grvComponente.Columns[4].Visible = true;
                    }
                }
            }
        }

        protected void imgAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                using (FileUpload uplComponente = ((FileUpload)this.grvComponente.FooterRow.FindControl("uplComponenteFooter")))
                {
                    if (uplComponente.HasFile)
                    {
                        string fileName = uplComponente.FileName;

                        if (Path.GetExtension(fileName).ToUpper() != ".DLL")
                        {
                            this.lblMensaje.Text = "No es un archivo válido.";
                            return;
                        }

                        COMPONENTE componente = new COMPONENTE()
                        {
                            COM_NOMBRE = ((TextBox)this.grvComponente.FooterRow.FindControl("txtNombreFooter")).Text,
                            COM_ARCHIVO = Path.GetFileName(fileName)
                        };

                        EstadoOperacion resultado = ComponenteBL.insertarComponente(componente);
                        if (resultado.Estado)
                        {
                            string directorio = DllDinamica.DirectorioComponentes();

                            uplComponente.SaveAs(directorio + Path.GetFileName(fileName));
                            this.grvComponente.DataBind();
                        }
                        else
                        {
                            this.lblMensaje.Text = resultado.Mensaje;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = ex.Message;
            }


        }

        protected void grvComponente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                ModificarComponente(Convert.ToInt32(e.CommandArgument));
            }

            if (e.CommandName == "Eliminar")
            {
                EliminarComponente(Convert.ToInt32(e.CommandArgument));
            }
        }

        private void ModificarComponente(int index)
        {
            try
            {
                GridViewRow row = this.grvComponente.Rows[index];

                using (FileUpload uplComponente = ((FileUpload)row.FindControl("uplComponenteEdit")))
                {
                    string fileName = uplComponente.FileName;

                    if (Path.GetExtension(fileName).ToUpper() != ".DLL")
                    {
                        this.lblMensaje.Text = "No es un archivo válido.";
                        return;
                    }

                    COMPONENTE componente = new COMPONENTE()
                    {
                        COM_CODIGO = int.Parse(this.grvComponente.DataKeys[index].Values["COM_CODIGO"].ToString()),
                        COM_NOMBRE = string.Empty,
                        COM_ARCHIVO = Path.GetFileName(fileName)
                    };

                    EstadoOperacion resultado = ComponenteBL.modificarComponente(componente);
                    if (resultado.Estado)
                    {
                        string rutaArchivoNuevo = Path.GetFileName(fileName);
                        string rutaArchivoViejo = this.grvComponente.DataKeys[index].Values["COM_ARCHIVO"].ToString();
                        string directorio = DllDinamica.DirectorioComponentes();

                        FileInfo archivoViejo = new FileInfo(directorio + rutaArchivoViejo);
                        {
                            if (archivoViejo.Exists)
                            {
                                archivoViejo.Delete();
                            }
                        }

                        uplComponente.SaveAs(directorio + rutaArchivoNuevo);
                        grvComponente.EditIndex = -1;
                        this.grvComponente.DataBind();
                    }
                    else
                    {
                        this.lblMensaje.Text = resultado.Mensaje;
                    }
                }
            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = ex.Message;
            }
        }

        private void EliminarComponente(int index)
        {
            try
            {

                COMPONENTE componente = new COMPONENTE()
                {
                    COM_CODIGO = int.Parse(this.grvComponente.DataKeys[index].Values["COM_CODIGO"].ToString()),
                };

                EstadoOperacion resultado = ComponenteBL.eliminarComponente(componente);
                if (resultado.Estado)
                {
                    string rutaArchivoViejo = this.grvComponente.DataKeys[index].Values["COM_ARCHIVO"].ToString();
                    string directorio = ConfigurationManager.AppSettings["DirectorioComponentes"];

                    FileInfo archivoViejo = new FileInfo(directorio + rutaArchivoViejo);
                    {
                        if (archivoViejo.Exists)
                        {
                            archivoViejo.Delete();
                        }
                    }

                    this.grvComponente.DataBind();
                }
                else
                {
                    this.lblMensaje.Text = resultado.Mensaje;
                }

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = ex.Message;
            }
        }
    }
}
