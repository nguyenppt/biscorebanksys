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
    public partial class RateExchange :  DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadDataTolvRateExchange();
        }



        private void LoadDataTolvRateExchange()
        {
            ExchangeRatesRepository facade = new ExchangeRatesRepository();
            var db = facade.GetAll();
            lvRateExchange.DataSource = db.ToList();
            lvRateExchange.DataBind();
        }

        protected void lvRateExchange_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            ExchangeRatesRepository facade = new ExchangeRatesRepository();
            StoreProRepository storeFacade = new StoreProRepository();

            TextBox tbcurrency = (lvRateExchange.InsertItem.FindControl("tbCurrency")) as TextBox;
            RadNumericTextBox rnbVNDRate = (lvRateExchange.InsertItem.FindControl("rnbVNDRate")) as RadNumericTextBox;


            B_ExchangeRates exchangeRate = new B_ExchangeRates();

            exchangeRate.Rate = (decimal)rnbVNDRate.Value;
            exchangeRate.Currency = tbcurrency.Text;
            storeFacade.StoreProcessor().B_ExchangeRate_history_process(exchangeRate.Currency, exchangeRate.Rate, this.UserId, 1);

            facade.Add(exchangeRate);
            facade.Commit();
            LoadDataTolvRateExchange();
        }

        protected void lvRateExchange_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lvRateExchange.EditIndex = -1;
            LoadDataTolvRateExchange();
        }

        protected void lvRateExchange_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lvRateExchange.EditIndex = e.NewEditIndex;
            LoadDataTolvRateExchange();
        }

        protected void lvRateExchange_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            ExchangeRatesRepository facade = new ExchangeRatesRepository();
            StoreProRepository storeFacade = new StoreProRepository();

            TextBox tbcurrency = (lvRateExchange.EditItem.FindControl("tbCurrency")) as TextBox;
            RadNumericTextBox rnbVNDRate = (lvRateExchange.EditItem.FindControl("rnbVNDRate")) as RadNumericTextBox;
            Label lbl = (lvRateExchange.Items[e.ItemIndex].FindControl("lbID")) as Label;
            String ids = "";
            if (lbl != null)
                ids = lbl.Text;

            B_ExchangeRates exchangeRate = new B_ExchangeRates();
            exchangeRate.Rate = (decimal)rnbVNDRate.Value;
            exchangeRate.Currency = tbcurrency.Text;
            exchangeRate.Id = Int32.Parse(ids);

            B_ExchangeRates exits = facade.GetById(exchangeRate.Id);
            if (exits != null)
            {
                storeFacade.StoreProcessor().B_ExchangeRate_history_process(exchangeRate.Currency, exchangeRate.Rate, this.UserId, 2);
                facade.Update(facade.GetById(exchangeRate.Id), exchangeRate);
                facade.Commit();
            }
            lvRateExchange.EditIndex = -1;
            LoadDataTolvRateExchange();
        }

        protected void lvRateExchange_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            String ids = "";
            Label lbl = (lvRateExchange.Items[e.ItemIndex].FindControl("lbID")) as Label;
            if (lbl != null)
                ids = lbl.Text;

            if (!String.IsNullOrEmpty(ids))
            {
                StoreProRepository storeFacade = new StoreProRepository();
                ExchangeRatesRepository facade = new ExchangeRatesRepository();
                var itm = facade.GetById(Int16.Parse(ids));
                if (itm != null)
                {
                    storeFacade.StoreProcessor().B_ExchangeRate_history_process(itm.Currency, itm.Rate, this.UserId, 3);
                    facade.Delete(itm);
                    facade.Commit();
                    LoadDataTolvRateExchange();
                }

            }
        }
    }
}