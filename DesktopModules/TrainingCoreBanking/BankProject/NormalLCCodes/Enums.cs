using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject
{
    public enum EnumLCType
    {
        Issue,
        Amend,
        Cancel,
        Authorize,
        Reverse,
        Settlement
    }


    public enum EnumLCChargeCode
    {
        /// <summary>
        /// OPEN CHARGE FOR IMPORT LC
        /// </summary>
        OPEN,
        /// <summary>
        /// OPEN CHARGE FOR IMPORT LC (AMORT)
        /// </summary>
        OPENAMORT,
        /// <summary>
        /// AMENDMENT CHARGE FOR IMPORT LC
        /// </summary>
        AMEND,
        /// <summary>
        /// AMENDMENT CHARGE FOR IMPORT LC (AMORT)
        /// </summary>
        AMENDAMORT,
        /// <summary>
        /// CANCEL CHARGE FOR IMPORT LC
        /// </summary>
        CANCEL,
        /// <summary>
        /// CABLE CHARGE FOR IMPORT LC
        /// </summary>
        CABLE,
        /// <summary>
        /// PAYMENT CHARGE FOR IMPORT LC
        /// </summary>
        PAYMENT,
        /// <summary>
        /// ACCEPTING  CHARGE FOR IMPORT LC
        /// </summary>
        ACCEPT,
        /// <summary>
        /// OTHER CHARGE  CHARGE FOR IMPORT LC
        /// </summary>
        OTHER,
        /// <summary>
        /// HANDLING  CHARGE FOR IMPORT LC
        /// </summary>
        HANDLING,
        /// <summary>
        /// DISCREPANCY  CHARGE FOR IMPORT LC
        /// </summary>
        DISCRP,
    }

    public enum EnumICChargeCode
    {
        /// <summary>
        /// ADVISING CHARGE FOR IMPORT COLLECTION
        /// </summary>
        ADVISE,
        /// <summary>
        /// AMENDMENT  CHARGE FOR IMPORT COLLECTION
        /// </summary>
        AMEND,
        /// <summary>
        /// CANCEL  CHARGE FOR IMPORT COLLECTION
        /// </summary>
        CANCEL,
        /// <summary>
        /// PAYMENT  CHARGE FOR IMPORT COLLECTION
        /// </summary>
        PAYMENT,
        /// <summary>
        /// CABLE  CHARGE FOR IMPORT COLLECTION
        /// </summary>
        CABLE,
        /// <summary>
        /// ACCEPTING  CHARGE FOR IMPORT COLLECTION
        /// </summary>
        ACCEPT,
        /// <summary>
        /// OTHER  CHARGE FOR IMPORT COLLECTION
        /// </summary>
        OTHER,
        /// <summary>
        /// HANDLING  CHARGE FOR IMPORT COLLECTION
        /// </summary>
        HANDLING,
    }
}
