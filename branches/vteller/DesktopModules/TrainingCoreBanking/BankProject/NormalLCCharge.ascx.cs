using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankProject
{
    public partial class NormalLCCharge : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            this.LoadProps();
        }

        #region Charge Props
        [Bindable(true, BindingDirection.TwoWay)]
        public EnumLCChargeCode ChargeCode
        {
            get
            {
                return this.ViewState.GetState<EnumLCChargeCode>("ChargeCode", EnumLCChargeCode.OPEN);
            }
            set
            {
                this.ViewState["ChargeCode"] = value;
            }
        }

        public string LCCode
        {
            get
            {
                return this.ViewState.GetState<string>("LCCode");
            }
            set
            {
                this.ViewState["LCCode"] = value;
                if (this.HiddenFieldLCCode != null)
                    this.HiddenFieldLCCode.Value = value;
            }
        }

        public bool EditMode
        {
            get
            {
                return this.ViewState.GetState<bool>("EditMode");
            }
            set
            {
                this.ViewState.SetState<bool>("EditMode", value);
                if (this.FormViewChargeDetail != null)
                    this.FormViewChargeDetail.DefaultMode = value ? FormViewMode.Edit : FormViewMode.Insert;
            }
        }


        void LoadProps()
        {
            //this.tbChargeCode.Text = "ILC." + this.ChargeCode;
        }


        #endregion

        protected void rcbPartyCharged_SelectIndexChange(object sender, EventArgs e)
        {

        }

        protected void rcbChargeStatus_SelectIndexChange(object sender, EventArgs e)
        {

        }

        protected void rcbChargeAcct_SelectIndexChange(object sender, EventArgs e)
        {

        }

        protected void tbChargeAmt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}