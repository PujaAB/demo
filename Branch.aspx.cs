
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




public partial class Branch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindcontrols();

            

        } 
    }

    
    private void bindcontrols()
    {
        lstCountry.DataSource = clsStaticDBAccess.GetSaticListBySaticTypeID(Convert.ToInt32(SaticMaster.Country));
        lstCountry.DataValueField = "SaticValue";
        lstCountry.DataTextField = "StaticName";
        lstCountry.DataBind();

        lstCountry_SelectedIndexChanged(null, null);
                     

        grdBranch.DataSource = clsBranchDBAccess.GetList();
        grdBranch.DataBind();
        txtBcode.Focus();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearSelection();

    }

    private void ClearSelection()
    {
        hdnBranchID.Value = "";
        txtBcode.Text = "";
        txtBname.Text = "";
      //  txtDivison.Text = "";
        txtCity.Text = "";
        txtPcode.Text = "";
        lstState.ClearSelection();
        lstCountry.ClearSelection();
        txtBcode.Focus();
    }

    protected void grdBranch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ClearSelection();
       // EnableDisableControls(true);

        string branchcode = Convert.ToString(e.CommandArgument.ToString());

        if (e.CommandName == "Edit_Branch")
        {
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            int intBranchID = Convert.ToInt32(grdBranch.DataKeys[row.RowIndex].Values[0].ToString());

            clsBranch objBranch = new clsBranch();
            objBranch = clsBranchDBAccess.Get(intBranchID);
            txtBcode.Text = objBranch.branch_code;
            txtBname.Text = objBranch.branch_name;
            txtCity.Text = objBranch.city;
            txtPcode.Text = objBranch.pincode.ToString();
            try
            {

                lstCountry.ClearSelection();
                lstCountry.Items.FindByValue(objBranch.Country_id.ToString()).Selected = true;
            }
            catch (Exception ex)
            { }
            lstCountry_SelectedIndexChanged(null, null);

            // lstState.ClearSelection();

            try
            {
                lstState.Items.FindByValue(objBranch.State_id.ToString()).Selected = true;
            }
            catch (Exception ex)
            { }





            hdnBranchID.Value = objBranch.branch_id.ToString();

            EnableDisableControls(true);
        }

        if (e.CommandName == "Delete_Branch")
        {
            string vendid = Convert.ToString(e.CommandArgument.ToString());

            GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            int intBranchID = Convert.ToInt32(grdBranch.DataKeys[row.RowIndex].Values[0].ToString());

            clsBranchDBAccess.Delete(intBranchID);

            bindcontrols();


        }


        if (e.CommandName == "View_Branch")
        {

            GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
           
           // string s = gv.DataKeys[row.RowIndex][0].ToString();


          //  int intRowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;
            int intBranchID = Convert.ToInt32(grdBranch.DataKeys[row.RowIndex].Values[0].ToString());

            clsBranch objBranch = new clsBranch();
            objBranch = clsBranchDBAccess.Get(intBranchID);
            txtBcode.Text = objBranch.branch_code;
            txtBname.Text = objBranch.branch_name;
            txtCity.Text = objBranch.city;
            txtPcode.Text = objBranch.pincode.ToString();
            try
            {

                lstCountry.ClearSelection();
                lstCountry.Items.FindByValue(objBranch.Country_id.ToString()).Selected = true;
            }
            catch (Exception ex)
            { }
            lstCountry_SelectedIndexChanged(null, null);

            // lstState.ClearSelection();

            try
            {
                lstState.Items.FindByValue(objBranch.State_id.ToString()).Selected = true;
            }
            catch (Exception ex)
            { }
            hdnBranchID.Value = objBranch.branch_id.ToString();
            EnableDisableControls(false);

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)

    {

        clsBranch objBranch = new clsBranch();
     
        objBranch.branch_code =  txtBcode.Text.Trim() ;
        objBranch.branch_name = txtBname.Text.Trim();
        // objBranch.Div txtDivison.Text.Trim();
        objBranch.city = txtCity.Text.Trim();
        objBranch.pincode = Convert.ToInt32(txtPcode.Text.Trim());
        objBranch.State_id = Convert.ToInt32(lstState.SelectedItem.Value);
        objBranch.Country_id = Convert.ToInt32(lstCountry.SelectedItem.Value);
   

        if (hdnBranchID.Value == "")
        {
            clsBranchDBAccess.Add(objBranch);
            ClearSelection();
            bindcontrols();
        }
        else
        {
            objBranch.branch_id = Convert.ToInt32(hdnBranchID.Value);
            clsBranchDBAccess.Update(objBranch);
            ClearSelection();
            bindcontrols();
        }

       
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ClearSelection();

       //Boolean boolFlag = true;

        EnableDisableControls(true);
       

    }

    private void EnableDisableControls(bool boolFlag)
    {
        txtBcode.Enabled = boolFlag;
        txtBname.Enabled = boolFlag;
       // txtDivison.Enabled = boolFlag;
        txtCity.Enabled = boolFlag;
        txtPcode.Enabled = boolFlag;
        lstState.Enabled = boolFlag;
        lstCountry.Enabled = boolFlag;
        btnSave.Enabled = boolFlag;
        btnReset.Enabled = boolFlag;

    }

    protected void lstCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            lstState.DataSource = clsStaticDBAccess.GetSaticListBySaticTypeIDandParentType(Convert.ToInt32(SaticMaster.State), Convert.ToInt32(lstCountry.SelectedItem.Value));
            lstState.DataValueField = "SaticValue";
            lstState.DataTextField = "StaticName";
            lstState.DataBind();
        }
        catch (Exception ex)

        {

        }
      
    }
}

