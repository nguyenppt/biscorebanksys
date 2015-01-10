using BankProject.DBContext;
using BankProject.DBRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class InterestedRateAutomatic : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                LoadDataTolvLoanInterestedRate();
        }



        private void LoadDataTolvLoanInterestedRate()
        {
            NewLoanInterestedKeyRepository facade = new NewLoanInterestedKeyRepository();
            var db = facade.GetAll();
            lvLoanInterestedRate.DataSource = db.ToList();
            lvLoanInterestedRate.DataBind();
        }

        protected void lvLoanInterestedRate_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            NewLoanInterestedKeyRepository facade = new NewLoanInterestedKeyRepository();
            StoreProRepository storeFacade = new StoreProRepository();

            RadNumericTextBox monthRate = (lvLoanInterestedRate.InsertItem.FindControl("radMonthRate")) as RadNumericTextBox;
            TextBox rateDisplay = (lvLoanInterestedRate.InsertItem.FindControl("tbRateDisplay")) as TextBox;
            RadNumericTextBox rnbVNDRate = (lvLoanInterestedRate.InsertItem.FindControl("rnbVNDRate")) as RadNumericTextBox;
            RadNumericTextBox rnbUSDRate = (lvLoanInterestedRate.InsertItem.FindControl("rnbUSDRate")) as RadNumericTextBox;

            if ((Int16)monthRate.Value <= 0)
            {
                RadWindowManager1.RadAlert("Deposite Rate is required!", 340, 150, "Alert", null);
                return;
            }

            BLOANINTEREST_KEY intKey = new BLOANINTEREST_KEY();
            intKey.MonthLoanRateNo = (Int16)monthRate.Value;
            intKey.USD_InterestRate = (decimal)rnbUSDRate.Value;
            intKey.VND_InterestRate = (decimal)rnbVNDRate.Value;
            intKey.LoanInterest_Key = rateDisplay.Text;
            intKey.Id = (Int16)monthRate.Value;
            storeFacade.StoreProcessor().B_LoanInterested_Key_history_process(intKey.MonthLoanRateNo, intKey.VND_InterestRate, intKey.USD_InterestRate, this.UserId, 1);

            facade.Add(intKey);
            facade.Commit();
            LoadDataTolvLoanInterestedRate();
        }

        protected void lvLoanInterestedRate_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lvLoanInterestedRate.EditIndex = -1;
            LoadDataTolvLoanInterestedRate();
        }

        protected void lvLoanInterestedRate_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lvLoanInterestedRate.EditIndex = e.NewEditIndex;
            LoadDataTolvLoanInterestedRate();
        }

        protected void lvLoanInterestedRate_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            NewLoanInterestedKeyRepository facade = new NewLoanInterestedKeyRepository();
            StoreProRepository storeFacade = new StoreProRepository();

            RadNumericTextBox monthRate = (lvLoanInterestedRate.EditItem.FindControl("radMonthRate")) as RadNumericTextBox;
            TextBox rateDisplay = (lvLoanInterestedRate.EditItem.FindControl("tbRateDisplay")) as TextBox;
            RadNumericTextBox rnbVNDRate = (lvLoanInterestedRate.EditItem.FindControl("rnbVNDRate")) as RadNumericTextBox;
            RadNumericTextBox rnbUSDRate = (lvLoanInterestedRate.EditItem.FindControl("rnbUSDRate")) as RadNumericTextBox;
            Label lbl = (lvLoanInterestedRate.Items[e.ItemIndex].FindControl("lbID")) as Label;
            String ids = "";            
            if (lbl != null)
                ids = lbl.Text;

            BLOANINTEREST_KEY intKey = new BLOANINTEREST_KEY();
            intKey.MonthLoanRateNo = (Int16)monthRate.Value;
            intKey.USD_InterestRate = (decimal)rnbUSDRate.Value;
            intKey.VND_InterestRate = (decimal)rnbVNDRate.Value;
            intKey.LoanInterest_Key = rateDisplay.Text;
            intKey.Id = Int32.Parse(ids);

            BLOANINTEREST_KEY exits = facade.GetById(intKey.Id);
            if (exits != null)
            {
                storeFacade.StoreProcessor().B_LoanInterested_Key_history_process(intKey.MonthLoanRateNo, intKey.VND_InterestRate, intKey.USD_InterestRate, this.UserId, 2);
                facade.Update(facade.GetById(intKey.Id), intKey);
                facade.Commit();
            }
            lvLoanInterestedRate.EditIndex = -1;
            LoadDataTolvLoanInterestedRate();
        }

        protected void lvLoanInterestedRate_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            String ids = "";
            Label lbl = (lvLoanInterestedRate.Items[e.ItemIndex].FindControl("lbID")) as Label;
            if (lbl != null)
                ids = lbl.Text;

            if (!String.IsNullOrEmpty(ids))
            {
                StoreProRepository storeFacade = new StoreProRepository();
                NewLoanInterestedKeyRepository facade = new NewLoanInterestedKeyRepository();
                var itm = facade.GetById(Int16.Parse(ids));
                if (itm != null)
                {
                    storeFacade.StoreProcessor().B_LoanInterested_Key_history_process(itm.MonthLoanRateNo, itm.VND_InterestRate, itm.USD_InterestRate, this.UserId, 3);
                    facade.Delete(itm);
                    facade.Commit();
                    LoadDataTolvLoanInterestedRate();
                }

            }
        }
    }
}