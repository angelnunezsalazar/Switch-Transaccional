using System;
using System.Web.UI;
using System.Web.UI.WebControls;


public enum EnumInfoType
{
    Agregar,
    Editar,
    Eliminar,
    Campo
}

 
public class GridViewTemplate : ITemplate
{

    ListItemType itemType;
    string headerName;
    string typeName;
    string fieldName;
    int longitud;
    EnumInfoType infoType;


    public GridViewTemplate(ListItemType itemType, string headerName, string typeName, EnumInfoType infoType)
    {
        this.itemType = itemType;
        this.headerName = headerName;
        this.typeName = typeName;
        this.infoType = infoType;
    }


    public GridViewTemplate(ListItemType itemType, string fieldName,int longitud, EnumInfoType infoType)
    {
        this.itemType = itemType;
        this.fieldName = fieldName;
        this.longitud = longitud;
        this.infoType = infoType;
    }

    public GridViewTemplate(ListItemType itemType, string fieldName, EnumInfoType infoType)
    {
        this.itemType = itemType;
        this.fieldName = fieldName;
        this.infoType = infoType;
    }

    public GridViewTemplate(ListItemType itemType, EnumInfoType infoType)
    {
        this.itemType = itemType;
        this.infoType = infoType;
    }


    public void InstantiateIn(System.Web.UI.Control Container)
    {
        switch (itemType)
        {
            case ListItemType.Header:
                Literal cabecera = new Literal();
                cabecera.Text = headerName+" ["+typeName+"]";
                Container.Controls.Add(cabecera);
                break;

            case ListItemType.Item:

                switch (infoType)
                {
                    case EnumInfoType.Editar:

                        ImageButton editButton = new ImageButton();
                        editButton.ID = "imgEditar";
                        editButton.ImageUrl = "~/Includes/Imagenes/iconEdit.png";
                        editButton.CommandName = "Edit";
                        Container.Controls.Add(editButton);

                        break;

                    case EnumInfoType.Eliminar:

                        ImageButton deleteButton = new ImageButton();
                        deleteButton.ID = "imgBorrar";
                        deleteButton.ImageUrl = "~/Includes/Imagenes/iconErase.png";
                        deleteButton.CommandName = "Delete";
                        deleteButton.OnClientClick = "return confirm('Esta seguro que quiere eliminar el registro?')";
                        Container.Controls.Add(deleteButton);

                        break;

                    case EnumInfoType.Campo:

                        Label lblCampo = new Label();
                        lblCampo.ID = "lbl" + fieldName;
                        lblCampo.Text = String.Empty;
                        lblCampo.DataBinding += new EventHandler(OnDataBinding);
                        Container.Controls.Add(lblCampo);
                        break;

                }
                break;
            case ListItemType.EditItem:

                switch (infoType)
                {
                    case EnumInfoType.Editar:

                        ImageButton actualizarButton = new ImageButton();
                        actualizarButton.ID = "imgActualizar";
                        actualizarButton.CommandName = "Update";
                        actualizarButton.ImageUrl = "~/Includes/Imagenes/iconSave.png";
                        Container.Controls.Add(actualizarButton);

                        ImageButton cancelarButton = new ImageButton();
                        cancelarButton.ImageUrl = "~/Includes/Imagenes/iconCancel.png";
                        cancelarButton.ID = "imgCancelar";
                        cancelarButton.CommandName = "Cancel";
                        Container.Controls.Add(cancelarButton);

                        break;

                    case EnumInfoType.Eliminar:

                        ImageButton deleteButton = new ImageButton();
                        deleteButton.ID = "imgBorrar";
                        deleteButton.ImageUrl = "~/Includes/Imagenes/iconErase.png";
                        deleteButton.CommandName = "Delete";
                        deleteButton.OnClientClick = "return confirm('Esta seguro que quiere eliminar el registro?')";
                        Container.Controls.Add(deleteButton);

                        break;

                    case EnumInfoType.Campo:

                        TextBox txtCampo = new TextBox();
                        txtCampo.ID = IdEdit(fieldName);
                        txtCampo.Text = String.Empty;
                        txtCampo.MaxLength = longitud;
                        txtCampo.Width = new Unit(150);
                        txtCampo.DataBinding += new EventHandler(OnDataBinding);
                        Container.Controls.Add(txtCampo);

                        break;

                }
                break;

            case ListItemType.Footer:

                switch (infoType)
                {
                    case EnumInfoType.Agregar:

                        ImageButton actualizarButton = new ImageButton();
                        actualizarButton.ID = "imgAgregar";
                        actualizarButton.CommandName = "Agregar";
                        actualizarButton.ImageUrl = "~/Includes/Imagenes/iconNew.png";
                        Container.Controls.Add(actualizarButton);

                        break;

                    case EnumInfoType.Campo:

                        TextBox txtCampo = new TextBox();
                        txtCampo.ID = IdFooter(fieldName);
                        txtCampo.MaxLength = longitud;
                        txtCampo.Width = new Unit(150);
                        Container.Controls.Add(txtCampo);

                        break;

                }

                break;
        }

    }

    private void OnDataBinding(object sender, EventArgs e)
    {

        object boundValue = null;
        Control ctrl = (Control)sender;
        IDataItemContainer dataItemContainer = (IDataItemContainer)ctrl.NamingContainer;
        boundValue = DataBinder.Eval(dataItemContainer.DataItem, fieldName);

        switch (itemType)
        {
            case ListItemType.Item:
                Label lblField = (Label)sender;
                lblField.Text = boundValue.ToString();

                break;
            case ListItemType.EditItem:
                TextBox txtField = (TextBox)sender;
                txtField.Text = boundValue.ToString();

                break;
        }
    }

    public static string IdFooter(string fieldName)
    {
        return "txt" + fieldName + "Footer";
    }

    public static string IdEdit(string fieldName)
    {
        return "txt" + fieldName + "Edit";
    }


}
