using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BankProject
{
    public class BaseNormalLCChargeTabs : System.Web.UI.UserControl
    {
        protected void CheckBoxWaiveCHarges_CheckedChanged(object sender, EventArgs args)
        {
            var checkbox = (CheckBox)sender;
            var tabs = checkbox.Parent.FindControl("TabContainerCharges");
            tabs.Visible = !checkbox.Checked;
        }

        public string LCCode
        {
            get
            {
                return this.ViewState.GetState<string>("LCCode");
            }
            set
            {
                this.ViewState.SetState<string>("LCCode", value);
            }
        }

        public EnumLCChargeCode[] LCChargeCodes
        {
            get
            {
                return this.ViewState.GetState<EnumLCChargeCode[]>("LCChargeCodes");
            }
            set
            {
                this.ViewState.SetState<EnumLCChargeCode[]>("LCChargeCodes", value);
            }
        }

        public EnumLCType LCType
        {
            get
            {
                return this.ViewState.GetState<EnumLCType>("LCType");
            }
            set
            {
                this.ViewState.SetState<EnumLCType>("LCType", value);
            }
        }

        protected virtual void LoadData()
        {

        }

        void LoadProps()
        {

        }
    }
}
