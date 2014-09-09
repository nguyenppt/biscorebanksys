using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace BankProject.DataProvider
{
    public static class SQLData
    {
        private static SqlDataProvider sqldata = new SqlDataProvider();

        public static DataTable CreateGenerateDatas(string fieldType, int viewType = 217)
        {
            var dtTemp = new DataTable();
            dtTemp.Clear();
            dtTemp.Columns.Add("Id", typeof (string));
            dtTemp.Columns.Add("Description", typeof (string));
            dtTemp.Columns.Add("Name", typeof (string));

            var drow = dtTemp.NewRow();
            switch (fieldType)
            {
                case "TabCharge_ChargeAcct":
                    drow["Description"] = "03.000068528.1 - TGTT USD-CTY TNHH";
                    drow["Id"] = "03.000068528.1";
                    drow["Name"] = "TGTT USD-CTY TNHH";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "03.000045632.1 - TGTT USD-CTY TNHH TOAN CAU";
                    drow["Name"] = "TGTT USD-CTY TNHH TOAN CAU";
                    drow["Id"] = "03.000045632.1";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "03.000078945.1 - TGTT USD-CTY TNHH LONG AN";
                    drow["Name"] = "TGTT USD-CTY TNHH LONG AN";
                    drow["Id"] = "03.000078945.1";
                    dtTemp.Rows.Add(drow);
                    break;

                case "TabMT103_OrderingCustAcc":
                    drow = dtTemp.NewRow();
                    drow["Description"] = "KHOAN PTRA USD TRONG NV TT";
                    drow["Id"] = "USD1147300022232";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "KHOAN PTRA EUR TRONG NV TT";
                    drow["Id"] = "EUR1147300011131";
                    dtTemp.Rows.Add(drow);
                    break;

                case "TabAccountTransfer_TransactionType":
                    drow = dtTemp.NewRow();
                    drow["Description"] = "advance transfer_commodity";
                    drow["Id"] = "OTC1";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "after transfer_commodity";
                    drow["Id"] = "OTC2";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "advance transfer_services";
                    drow["Id"] = "OTS1";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "after transfer_services";
                    drow["Id"] = "OTS2";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "transfer_individual";
                    drow["Id"] = "OTP1";
                    dtTemp.Rows.Add(drow);
                    break;

                case "TabAccountTransfer_CreditAccount":
                    drow = dtTemp.NewRow();
                    drow["Description"] = "BANK OF AMERICA, N.A.";
                    drow["Id"] = "010000035160";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "CITIBANK N.A.";
                    drow["Id"] = "020000035160";
                    dtTemp.Rows.Add(drow);
                    break;

                case "TabAccountTransfer_DebitAcctNo":
                    drow = dtTemp.NewRow();
                    drow["Description"] = "KHOAN PTRA USD TRONG NV TT";
                    drow["Id"] = "USD1147300022232";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "KHOAN PTRA EUR TRONG NV TT";
                    drow["Id"] = "EUR1147300011131";
                    dtTemp.Rows.Add(drow);
                    break;

                case "DocumetaryCollection_TabMain_CollectionType":
                    drow = dtTemp.NewRow();
                    drow["Description"] = "Document against Acceptance";
                    drow["Id"] = "DA";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "Document against Payment";
                    drow["Id"] = "DP";
                    dtTemp.Rows.Add(drow);
                    break;

                case "DocumetaryCleanCollection_TabMain_CollectionType":
                    drow = dtTemp.NewRow();
                    drow["Description"] = "Clean Collection";
                    drow["Id"] = "CC";
                    dtTemp.Rows.Add(drow);

                    break;

                case "DocumetaryCollection_TabMain_Commodity":
                    drow = dtTemp.NewRow();
                    drow["Description"] = "Sắt thép";
                    drow["Id"] = "SATH";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "Cà phê";
                    drow["Id"] = "CAFE";
                    dtTemp.Rows.Add(drow);
                    break;

                case "DocumetaryCollection_TabMain_DocsCode":
                    drow = dtTemp.NewRow();
                    drow["Description"] = "INV - COMMERCIAL";
                    drow["Id"] = "INV";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "BL - BILL OF LADING";
                    drow["Id"] = "BL";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "PL - PACKING LIST";
                    drow["Id"] = "PL";
                    dtTemp.Rows.Add(drow);
                    break;

                case "SwiftCode":
                    drow = dtTemp.NewRow();
                    drow["Description"] = "ASIA COMMERCIAL BANK";
                    drow["Id"] = "ASCBVNVX";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "CITIBANK N.A.";
                    drow["Id"] = "CITIVNVX";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "HSBC BANK (VIETNAM) LTD.";
                    drow["Id"] = "HSBCVNVX";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "CITIBANK N.A.";
                    drow["Id"] = "CITIUS33";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "BANK OF AMERICA, N.A.";
                    drow["Id"] = "BOFAUS6H";
                    dtTemp.Rows.Add(drow);
                    break;

                case "DocumetaryCollection_TabMain_ChargeAcc":

                    drow = dtTemp.NewRow();
                    drow["Description"] = "CTY TNHH SONG HONG";
                    drow["Id"] = "03.000237869.4";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "CTY TNHH PHAT TRIEN PHAN MEM ABC";
                    drow["Id"] = "03.000237870.4";
                    dtTemp.Rows.Add(drow);
                    break;

                case "ChargeCode_Register":
                case "ChargeCode_Amendments":
                case "ChargeCode_Cancel":
                case "ChargeCode_Acception":
                case "ChargeCode_Payment":
                    return B_BCHARGECODE_GetByViewType(viewType);

                case "PartyCharged":
                    drow = dtTemp.NewRow();
                    drow["Description"] = "Beneficiary";
                    drow["Id"] = "B";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "Correspondent Charges for the Openner";
                    drow["Id"] = "AC";
                    dtTemp.Rows.Add(drow);
                    break;

                case "PartyCharged_IssueLC":
                    drow = dtTemp.NewRow();
                    drow["Description"] = "Openner";
                    drow["Id"] = "A";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "Correspondent Charges for the Openner";
                    drow["Id"] = "AC";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "Beneficiary";
                    drow["Id"] = "B";
                    dtTemp.Rows.Add(drow);

                    drow = dtTemp.NewRow();
                    drow["Description"] = "Correspondent Charges for the Beneficiary";
                    drow["Id"] = "BC";
                    dtTemp.Rows.Add(drow);
                    break;
            }

            return dtTemp;
        }

        public static DataSet B_BLCTYPES_GetByLCType(string LCType)
        {
            return sqldata.ndkExecuteDataset("B_BLCTYPES_GetByLCType", LCType);
        }

        public static DataSet B_BOperation_GetAll()
        {
            return sqldata.ndkExecuteDataset("B_BOperation_GetAll");
        }

        public static DataSet B_BNORMAILLCMT700_GetByNormalLCCode(string normalLcCode)
        {
            return sqldata.ndkExecuteDataset("B_BNORMAILLCMT700_GetByNormalLCCode", normalLcCode);
        }

        public static void B_BNORMAILLCMT700_Insert(string NormalLCCode, string RevevingBank,
                                                    string BounceOfTotal, string FormOfDocumentaryCredit,
                                                    string DateOfIssue,
                                                    string Date31D, string PlaceOfExpiry31D, string ApplicantBank51,
                                                    string Applicant50, string DocumentaryCusNo,
                                                    string DocumentaryNameAddress,
                                                    string CurrencyCode32B, string Amount32B,
                                                    string PercentCreditAmount39A1,
                                                    string PercentCreditAmount39A2, string AdditionalAmountComment,
                                                    string AvailableRule40E, string AvailableWith41A,
                                                    string AvailableWithNameAddress, string C42, string D42,
                                                    string DraweeNameAndAddress,
                                                    string MaximumCreditAmount39B, string MixedPaymentDetails,
                                                    string AvailableWithBy,

                                                    string DeferredPaymentDetails,
                                                    string PatialShipment,
                                                    string Transhipment, string Placeoftakingincharge,
                                                    string Portofloading, string PortofDischarge
                                                    , string Placeoffinalindistination, string LatesDateofShipment,
                                                    string ShipmentPeriod, string DescrpofGoods
                                                    , string DocsRequired, string OrderDocs, string AdditionalConditions,
                                                    string Charges, string PeriodforPresentation,
                                                    string ConfimationInstructions, string NegotgBank,
                                                    string SendertoReceiverInfomation
            )
        {
            sqldata.ndkExecuteNonQuery("B_BNORMAILLCMT700_Insert", NormalLCCode, RevevingBank, BounceOfTotal,
                                       FormOfDocumentaryCredit, DateOfIssue, Date31D, PlaceOfExpiry31D,
                                       ApplicantBank51, Applicant50, DocumentaryCusNo, DocumentaryNameAddress,
                                       CurrencyCode32B,
                                       Amount32B, PercentCreditAmount39A1,
                                       PercentCreditAmount39A2, AdditionalAmountComment, AvailableRule40E,
                                       AvailableWith41A,
                                       AvailableWithNameAddress, C42, D42, DraweeNameAndAddress, MaximumCreditAmount39B,
                                       MixedPaymentDetails,
                                       AvailableWithBy, DeferredPaymentDetails,
                                       PatialShipment,
                                       Transhipment, Placeoftakingincharge, Portofloading, PortofDischarge
                                       , Placeoffinalindistination, LatesDateofShipment, ShipmentPeriod, DescrpofGoods
                                       , DocsRequired, OrderDocs, AdditionalConditions,
                                       Charges, PeriodforPresentation, ConfimationInstructions, NegotgBank,
                                       SendertoReceiverInfomation);

        }

        public static DataSet B_BNORMAILLCMT740_GetByNormalLCCode(string normalLcCode)
        {
            return sqldata.ndkExecuteDataset("B_BNORMAILLCMT740_GetByNormalLCCode", normalLcCode);
        }

        public static void B_BNORMAILLCMT740_Insert(string NormalLCCode, string Generate, string ReceivingBank,
                                                    string DocumentaryCreditNumber,
                                                    string Date31D, string PlaceOfExpiry, string Beneficial,
                                                    string BeneficialNameAndAddress, string CreditMoneyType32,
                                                    string CreditAmount32, string AvailableWith41A,
                                                    string AvailableNameAndAddress, string Draffy42C, string Drawee42D
                                                    , string BankChanges, string SenderToReceiverIinformation,
                                                    string NameAddress)
        {
            sqldata.ndkExecuteNonQuery("B_BNORMAILLCMT740_Insert", NormalLCCode, Generate, ReceivingBank
                                       , DocumentaryCreditNumber, Date31D, PlaceOfExpiry, Beneficial,
                                       BeneficialNameAndAddress, CreditMoneyType32, CreditAmount32,
                                       AvailableWith41A, AvailableNameAndAddress, Draffy42C, Drawee42D, BankChanges,
                                       SenderToReceiverIinformation, NameAddress);
        }

        public static void B_BOVERSEASTRANSFER_Insert(string OverseasTransferCode
                                                      , string TransactionType
                                                      , string ProductLine
                                                      , string CountryCode
                                                      , string CommoditySer
                                                      , string OtherInfo
                                                      , string OtherBy
                                                      , string DebitRef
                                                      , string DebitAcctNo
                                                      , string DebitCurrency
                                                      , double DebitAmount
                                                      , string DebitDate
                                                      , double AmountDebited
                                                      , string TPKT
                                                      , string CreditAccount
                                                      , string CreditCurrency
                                                      , double TreasuryRate
                                                      , double CreditAmount
                                                      , string CreditDate
                                                      , string ProcessingDate
                                                      , double AmountCredited
                                                      , string VATSend
                                                      , string AddRemarks
                                                      , string curentUserId
                                                      , string txtOtherBy2, string txtOtherBy3, string txtOtherBy4,
                                                      string txtOtherBy5)
        {
            sqldata.ndkExecuteNonQuery("B_BOVERSEASTRANSFER_Insert", OverseasTransferCode
                                       , TransactionType
                                       , ProductLine
                                       , CountryCode
                                       , CommoditySer
                                       , OtherInfo
                                       , OtherBy
                                       , DebitRef
                                       , DebitAcctNo
                                       , DebitCurrency
                                       , DebitAmount
                                       , DebitDate
                                       , AmountDebited
                                       , TPKT
                                       , CreditAccount
                                       , CreditCurrency
                                       , TreasuryRate
                                       , CreditAmount
                                       , CreditDate
                                       , ProcessingDate
                                       , AmountCredited
                                       , VATSend
                                       , AddRemarks
                                       , curentUserId
                                       , txtOtherBy2
                                       , txtOtherBy3
                                       , txtOtherBy4
                                       , txtOtherBy5);
        }

        public static DataSet B_BOVERSEASTRANSFER_GetByOverseasTransferCode(string code)
        {
            return sqldata.ndkExecuteDataset("B_BOVERSEASTRANSFER_GetByOverseasTransferCode", code);
        }

        public static DataSet B_BBENEFICIARYBANK_GetAll()
        {
            return sqldata.ndkExecuteDataset("B_BBENEFICIARYBANK_GetAll");
        }

        public static DataSet B_BBENEFICIARYBANK_GetById(string id)
        {
            return sqldata.ndkExecuteDataset("B_BBENEFICIARYBANK_GetById", id);
        }

        public static void B_BOVERSEASTRANSFERMT103_Insert(string OverseasTransferCode
                                                           , string PendingMT
                                                           , string SenderReference
                                                           , string TimeIndication
                                                           , string BankOperationCode
                                                           , string InstructionCode
                                                           , string ValueDate
                                                           , string Currency
                                                           , double InterBankSettleAmount
                                                           , double InstancedAmount
                                                           , string OrderingCustAcc
                                                           , string OrderingInstitution
                                                           , string SenderCorrespondent
                                                           , string ReceiverCorrespondent
                                                           , string ReceiverCorrBankAct
                                                           , string IntermediaryInstruction
                                                           , string IntermediaryBankAcct
                                                           , string AccountWithInstitution
                                                           , string AccountWithBankAcct
                                                           , string RemittanceInformation
                                                           , string DetailOfCharges
                                                           , double SenderCharges
                                                           , double ReceiverCharges
                                                           , string SenderToReceiveInfo
                                                           , string curentUserId
                                                           , string BeneficiaryCustomer1
                                                           , string BeneficiaryCustomer2
                                                           , string BeneficiaryCustomer3
                                                           , string AccountType
                                                           , string AccountWithBankAcct2
                                                           , string BeneficiaryCustomer4
                                                           , string BeneficiaryCustomer5
                                                           , string IntermediaryType
                                                           , string IntermediaryInstruction1
                                                           , string IntermediaryInstruction2
            , string OrderingCustAccName, string OrderingCustAccAddr1, string OrderingCustAccAddr2, string OrderingCustAccAddr3)
        {
            sqldata.ndkExecuteNonQuery("B_BOVERSEASTRANSFERMT103_Insert", OverseasTransferCode
                                       , PendingMT
                                       , SenderReference
                                       , TimeIndication
                                       , BankOperationCode
                                       , InstructionCode
                                       , ValueDate
                                       , Currency
                                       , InterBankSettleAmount
                                       , InstancedAmount
                                       , OrderingCustAcc
                                       , OrderingInstitution
                                       , SenderCorrespondent
                                       , ReceiverCorrespondent
                                       , ReceiverCorrBankAct
                                       , IntermediaryInstruction
                                       , IntermediaryBankAcct
                                       , AccountWithInstitution
                                       , AccountWithBankAcct
                                       , RemittanceInformation
                                       , DetailOfCharges
                                       , SenderCharges
                                       , ReceiverCharges
                                       , SenderToReceiveInfo
                                       , curentUserId
                                       , BeneficiaryCustomer1
                                       , BeneficiaryCustomer2
                                       , BeneficiaryCustomer3
                                       , AccountType
                                       , AccountWithBankAcct2
                                       , BeneficiaryCustomer4
                                       , BeneficiaryCustomer5
                                       , IntermediaryType
                                       , IntermediaryInstruction1
                                       , IntermediaryInstruction2
                                       , OrderingCustAccName
                                       , OrderingCustAccAddr1
                                       , OrderingCustAccAddr2
                                       , OrderingCustAccAddr3);
        }

        public static void B_BOVERSEASTRANSFERCHARGECOMMISSION_Insert(string OverseasTransferCode
                                                                      , string ChargeAcct
                                                                      , string DisplayChargesCom
                                                                      , string CommissionCode
                                                                      , string CommissionType
                                                                      , double CommissionAmount
                                                                      , string CommissionFor
                                                                      , string ChargeCode
                                                                      , string ChargeType
                                                                      , double ChargeAmount
                                                                      , string ChargeFor
                                                                      , string DetailOfCharges
                                                                      , string VATNo
                                                                      , string AddRemarks1
                                                                      , string AddRemarks2
                                                                      , string ProfitCenteCust
                                                                      , double TotalChargeAmount
                                                                      , double TotalTaxAmount
                                                                      , string curentUserId
                                                                      , string CommissionCurrency
                                                                      , string ChargeCurrency)
        {
            sqldata.ndkExecuteNonQuery("B_BOVERSEASTRANSFERCHARGECOMMISSION_Insert", OverseasTransferCode
                                       , ChargeAcct
                                       , DisplayChargesCom
                                       , CommissionCode
                                       , CommissionType
                                       , CommissionAmount
                                       , CommissionFor
                                       , ChargeCode
                                       , ChargeType
                                       , ChargeAmount
                                       , ChargeFor
                                       , DetailOfCharges
                                       , VATNo
                                       , AddRemarks1
                                       , AddRemarks2
                                       , ProfitCenteCust
                                       , TotalChargeAmount
                                       , TotalTaxAmount
                                       , curentUserId
                                       , CommissionCurrency
                                       , ChargeCurrency);
        }

        public static DataTable B_BOVERSEASTRANSFER_GetByStatus(string status)
        {
            return sqldata.ndkExecuteDataset("B_BOVERSEASTRANSFER_GetByStatus", status).Tables[0];
        }

        public static DataTable B_BOVERSEASTRANSFER_GetByReview(string curentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BOVERSEASTRANSFER_GetByReview", curentUserId).Tables[0];
        }

        public static void B_BOVERSEASTRANSFER_UpdateStatus(string OverseasTransferCode, string status,
                                                            string authorizedBy)
        {
            sqldata.ndkExecuteNonQuery("B_BOVERSEASTRANSFER_UpdateStatus", OverseasTransferCode, status, authorizedBy);
        }

        public static void B_BDOCUMETARYCOLLECTION_Insert(string DocCollectCode
                                                          , string CollectionType
                                                          , string RemittingBankNo
                                                          , string RemittingBankAddr
                                                          , string RemittingBankAcct
                                                          , string RemittingBankRef
                                                          , string DraweeCusNo
                                                          , string DraweeAddr1
                                                          , string DraweeAddr2
                                                          , string DraweeAddr3
                                                          , string ReimbDraweeAcct
                                                          , string DrawerCusNo
                                                          , string DrawerAddr
                                                          , string Currency
                                                          , string Amount
                                                          , string DocsReceivedDate
                                                          , string MaturityDate
                                                          , string Tenor
                                                          , string Days
                                                          , string TracerDate
                                                          , string ReminderDays
                                                          , string Commodity
                                                          , string DocsCode1
                                                          , string NoOfOriginals1
                                                          , string NoOfCopies1
                                                          , string DocsCode2
                                                          , string NoOfOriginals2
                                                          , string NoOfCopies2
                                                          , string OtherDocs
                                                          , string InstructionToCus
                                                          , string CurrentUserId
                                                          , string DrawerAddr1, string DrawerAddr2, string Remarks,
                                                          string CancelDate
                                                          , string ContingentExpiryDate, string DrawerCusName,
                                                          string DraweeCusName
                                                          , string DraweeType, string DrawerType, string AccountOfficer,
                                                          string ExpressNo, string InvoiceNo,
                                                          string CancelRemark, string RemittingBankAddr2,
                                                          string RemittingBankAddr3, string comeFromUrl
                                                          , string AcceptedDate, string AcceptRemarks
            )
        {
            sqldata.ndkExecuteNonQuery("B_BDOCUMETARYCOLLECTION_Insert", DocCollectCode
                                       , CollectionType
                                       , RemittingBankNo
                                       , RemittingBankAddr
                                       , RemittingBankAcct
                                       , RemittingBankRef
                                       , DraweeCusNo
                                       , DraweeAddr1
                                       , DraweeAddr2
                                       , DraweeAddr3
                                       , ReimbDraweeAcct
                                       , DrawerCusNo
                                       , DrawerAddr
                                       , Currency
                                       , Amount
                                       , DocsReceivedDate
                                       , MaturityDate
                                       , Tenor
                                       , Days
                                       , TracerDate
                                       , ReminderDays
                                       , Commodity
                                       , DocsCode1
                                       , NoOfOriginals1
                                       , NoOfCopies1
                                       , DocsCode2
                                       , NoOfOriginals2
                                       , NoOfCopies2
                                       , OtherDocs
                                       , InstructionToCus
                                       , CurrentUserId
                                       , DrawerAddr1
                                       , DrawerAddr2
                                       , Remarks
                                       , CancelDate
                                       , ContingentExpiryDate
                                       , DrawerCusName
                                       , DraweeCusName
                                       , DraweeType
                                       , DrawerType
                                       , AccountOfficer
                                       , ExpressNo
                                       , InvoiceNo
                                       , CancelRemark
                                       , RemittingBankAddr2
                                       , RemittingBankAddr3
                                       , comeFromUrl
                                       , AcceptedDate
                                       , AcceptRemarks);
        }

        public static DataSet B_BDOCUMETARYCOLLECTION_GetByDocCollectCode(string DocCollectCode, int ViewType)
        {
            return sqldata.ndkExecuteDataset("B_BDOCUMETARYCOLLECTION_GetByDocCollectCode", DocCollectCode, ViewType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MaCode">MaCode trong table BMACODE</param>
        /// <param name="refix">TF/FT/LD/</param>
        /// <param name="flat">/ or -</param>
        /// <returns></returns>
        public static string B_BMACODE_GetNewID(string MaCode, string refix, string flat = "-")
        {
            return
                sqldata.ndkExecuteDataset("B_BMACODE_GetNewID", MaCode, refix, flat).Tables[0].Rows[0]["Code"].ToString();
        }


        public static DataSet B_BOVERSEASTRANSFER_Report(string MaCode)
        {
            return sqldata.ndkExecuteDataset("B_BOVERSEASTRANSFER_Report", MaCode);
        }

        public static void B_BDOCUMETARYCOLLECTIONCHARGES_Insert(string NormalLCCode, string WaiveCharges,
                                                                 string Chargecode, string ChargeAcct,
                                                                 string ChargePeriod, string ChargeCcy
                                                                 , string ExchRate, string ChargeAmt,
                                                                 string PartyCharged, string OmortCharges,
                                                                 string AmtInLocalCCY, string AmtDRfromAcct,
                                                                 string ChargeStatus
                                                                 , string ChargeRemarks, string VATNo, string TaxCode,
                                                                 string TaxCcy, string TaxAmt, string TaxinLCCYAmt,
                                                                 string TaxDate, string Rowchages, int ViewType)
        {
            sqldata.ndkExecuteNonQuery("B_BDOCUMETARYCOLLECTIONCHARGES_Insert", NormalLCCode, WaiveCharges, Chargecode,
                                       ChargeAcct, ChargePeriod, ChargeCcy
                                       , ExchRate, ChargeAmt, PartyCharged, OmortCharges, AmtInLocalCCY, AmtDRfromAcct,
                                       ChargeStatus
                                       , ChargeRemarks, VATNo, TaxCode, TaxCcy, TaxAmt, TaxinLCCYAmt, TaxDate, Rowchages,
                                       ViewType);
        }

        public static DataSet B_BDOCUMETARYCOLLECTION_GetbyStatus(string key, string currentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BDOCUMETARYCOLLECTION_GetbyStatus", key, currentUserId);
        }

        public static void B_BDOCUMETARYCOLLECTION_UpdateStatus(string DocCollectCode, string status,
                                                                string authorizedBy, string comeFromUrl)
        {
            sqldata.ndkExecuteNonQuery("B_BDOCUMETARYCOLLECTION_UpdateStatus", DocCollectCode, status, authorizedBy,
                                       comeFromUrl);
        }

        public static DataSet B_BDOCUMETARYCOLLECTION_GetByAmendment(string DocCollectCode, string Drawee,
                                                                     string DraweeAddr, string Drawer,
                                                                     string DrawerAddrr, string Amend_Status,
                                                                     string currentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BDOCUMETARYCOLLECTION_GetByAmendment", DocCollectCode, Drawee,
                                             DraweeAddr, Drawer, DrawerAddrr, Amend_Status, currentUserId);
        }

        public static DataSet B_BDOCUMETARYCOLLECTION_GetByCancel(string DocCollectCode, string Drawee,
                                                                  string DraweeAddr, string Drawer, string DrawerAddrr,
                                                                  string Amend_Status, string currentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BDOCUMETARYCOLLECTION_GetByCancel", DocCollectCode, Drawee, DraweeAddr,
                                             Drawer, DrawerAddrr, Amend_Status, currentUserId);
        }

        public static DataSet B_INCOMINGCOLLECTIONPAYMENT_GetByPaymentCode(string CollectionPaymentCode)
        {
            return sqldata.ndkExecuteDataset("B_INCOMINGCOLLECTIONPAYMENT_GetByPaymentCode", CollectionPaymentCode);
        }

        public static void B_INCOMINGCOLLECTIONPAYMENT_Insert(string CollectionPaymentCode
                                                              , string DrawType
                                                              , string Currency
                                                              , double DrawingAmount
                                                              , string ValueDate
                                                              , string DrFromAccount
                                                              , double ExchRate
                                                              , string AmtDrFrAcctCcy
                                                              , double AmtDrFromAcct
                                                              , string PaymentMethod
                                                              , string NostroAcct
                                                              , double AmtCredited
                                                              , string PaymentRemarks1
                                                              , string PaymentRemarks2
                                                              , string PaymentRemarks3
                                                              , int CurrentUserId
                                                              , string PresentorCusNo, string CountryCode)
        {
            sqldata.ndkExecuteNonQuery("B_INCOMINGCOLLECTIONPAYMENT_Insert", CollectionPaymentCode
                                       , DrawType
                                       , Currency
                                       , DrawingAmount
                                       , ValueDate
                                       , DrFromAccount
                                       , ExchRate
                                       , AmtDrFrAcctCcy
                                       , AmtDrFromAcct
                                       , PaymentMethod
                                       , NostroAcct
                                       , AmtCredited
                                       , PaymentRemarks1
                                       , PaymentRemarks2
                                       , PaymentRemarks3
                                       , CurrentUserId
                                       , PresentorCusNo
                                       , CountryCode);
        }

        public static DataSet B_INCOMINGCOLLECTIONPAYMENT_GetByReview(string CurrentUserId)
        {
            return sqldata.ndkExecuteDataset("B_INCOMINGCOLLECTIONPAYMENT_GetByReview", CurrentUserId);
        }

        public static void B_INCOMINGCOLLECTIONPAYMENT_UpdateStatus(string CollectionPaymentCode, string status,
                                                                    string authorizedBy)
        {
            sqldata.ndkExecuteNonQuery("B_INCOMINGCOLLECTIONPAYMENT_UpdateStatus", CollectionPaymentCode, status,
                                       authorizedBy);
        }

        // tab charge
        public static void B_INCOMINGCOLLECTIONPAYMENTCHARGES_Insert(string CollectionPaymentCode, string WaiveCharges,
                                                                     string Chargecode, string ChargeAcct,
                                                                     string ChargePeriod, string ChargeCcy
                                                                     , string ExchRate, string ChargeAmt,
                                                                     string PartyCharged, string OmortCharges,
                                                                     string AmtInLocalCCY, string AmtDRfromAcct,
                                                                     string ChargeStatus
                                                                     , string ChargeRemarks, string VATNo,
                                                                     string TaxCode, string TaxCcy, string TaxAmt,
                                                                     string TaxinLCCYAmt, string TaxDate,
                                                                     string Rowchages, string ChargeAmtFCY)
        {
            sqldata.ndkExecuteNonQuery("B_INCOMINGCOLLECTIONPAYMENTCHARGES_Insert", CollectionPaymentCode, WaiveCharges,
                                       Chargecode, ChargeAcct, ChargePeriod, ChargeCcy
                                       , ExchRate, ChargeAmt, PartyCharged, OmortCharges, AmtInLocalCCY, AmtDRfromAcct,
                                       ChargeStatus
                                       , ChargeRemarks, VATNo, TaxCode, TaxCcy, TaxAmt, TaxinLCCYAmt, TaxDate, Rowchages,
                                       ChargeAmtFCY);
        }

        // end tab charge

        public static void B_BDOCUMETARYCOLLECTIONMT410_Insert(string DocCollectCode
                                                               , string GeneralMT410_1
                                                               , string GeneralMT410_2
                                                               , string SendingBankTRN
                                                               , string RelatedReference
                                                               , string Currency
                                                               , string Amount)
        {
            sqldata.ndkExecuteNonQuery("B_BDOCUMETARYCOLLECTIONMT410_Insert", DocCollectCode
                                       , GeneralMT410_1
                                       , GeneralMT410_2
                                       , SendingBankTRN
                                       , RelatedReference
                                       , Currency
                                       , Amount);
        }

        public static DataSet B_BDOCUMETARYCOLLECTION_Report(string code)
        {
            return sqldata.ndkExecuteDataset("B_BDOCUMETARYCOLLECTION_Report", code);
        }

        public static DataSet B_BSWIFTCODE_GetAll()
        {
            return sqldata.ndkExecuteDataset("B_BSWIFTCODE_GetAll");
        }

        public static DataSet B_BSWIFTCODE_GetByCurrency(string Currency)
        {
            return sqldata.ndkExecuteDataset("B_BSWIFTCODE_GetByCurrency", Currency);
        }

        public static void B_INCOMINGCOLLECTIONPAYMENTMT202_Insert(string CollectionPaymentCode
                                                                   , string TransactionReferenceNumber
                                                                   , string RelatedReference
                                                                   , string ValueDate
                                                                   , string Currency
                                                                   , string Amount
                                                                   , string OrderingInstitution
                                                                   //, string SenderCorrespondent
                                                                   //, string ReceiverCorrespondent
                                                                   , string IntermediaryBank
                                                                   , string AccountWithInstitution
                                                                   , string BeneficiaryBank
                                                                   , string SenderToReceiverInformation
                                                                   , string SenderCorrespondent1
                                                                   , string SenderCorrespondent2
                                                                   , string ReceiverCorrespondent1
                                                                   , string ReceiverCorrespondent2
                                                                   , string IntermediaryBankType
                                                                   , string IntermediaryBankName
                                                                   , string IntermediaryBankAddr1
                                                                   , string IntermediaryBankAddr2
                                                                   , string IntermediaryBankAddr3
                                                                   , string AccountWithInstitutionType
                                                                   , string AccountWithInstitutionName
                                                                   , string AccountWithInstitutionAddr1
                                                                   , string AccountWithInstitutionAddr2
                                                                   , string AccountWithInstitutionAddr3
                                                                   , string BeneficiaryBankType
                                                                   , string BeneficiaryBankName
                                                                   , string BeneficiaryBankAddr1
                                                                   , string BeneficiaryBankAddr2
                                                                   , string BeneficiaryBankAddr3)
        {
            sqldata.ndkExecuteNonQuery("B_INCOMINGCOLLECTIONPAYMENTMT202_Insert", CollectionPaymentCode
                                       , TransactionReferenceNumber
                                       , RelatedReference
                                       , ValueDate
                                       , Currency
                                       , Amount
                                       , OrderingInstitution
                                       //, SenderCorrespondent
                                       //, ReceiverCorrespondent
                                       , IntermediaryBank
                                       , AccountWithInstitution
                                       , BeneficiaryBank
                                       , SenderToReceiverInformation
                                       , SenderCorrespondent1
                                       , SenderCorrespondent2
                                       , ReceiverCorrespondent1
                                       , ReceiverCorrespondent2
                                       , IntermediaryBankType
                                       , IntermediaryBankName
                                       , IntermediaryBankAddr1
                                       , IntermediaryBankAddr2
                                       , IntermediaryBankAddr3
                                       , AccountWithInstitutionType
                                       , AccountWithInstitutionName
                                       , AccountWithInstitutionAddr1
                                       , AccountWithInstitutionAddr2
                                       , AccountWithInstitutionAddr3
                                       , BeneficiaryBankType
                                       , BeneficiaryBankName
                                       , BeneficiaryBankAddr1
                                       , BeneficiaryBankAddr2
                                       , BeneficiaryBankAddr3
                );
        }

        public static DataSet B_BINCOMINGCOLLECTIONPAYMENTMT202_Report(string code)
        {
            return sqldata.ndkExecuteDataset("B_BINCOMINGCOLLECTIONPAYMENTMT202_Report", code);
        }

        public static DataTable B_BAUTHORISE_GetByReview(string curentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BAUTHORISE_GetByReview", curentUserId).Tables[0];
        }

        public static void B_BINCOMINGCOLLECTIONPAYMENTMT400_Insert(string CollectionPaymentCode
                                                                    , string GeneralMT400
                                                                    , string SendingBankTRN
                                                                    , string RelatedReference
                                                                    , string AmountCollected
                                                                    , string ValueDate
                                                                    , string Currency
                                                                    , string Amount
                                                                    //,string SenderCorrespondent
                                                                    //,string ReceiverCorrespondent
                                                                    , string DetailOfCharges1
                                                                    , string DetailOfCharges2
                                                                    , string SenderCorrespondent1
                                                                    , string SenderCorrespondent2
                                                                    , string ReceiverCorrespondent1
                                                                    , string ReceiverCorrespondent2

                                                                    , string ReceiverCorrespondentType
                                                                    , string ReceiverCorrespondentNo
                                                                    , string ReceiverCorrespondentName
                                                                    , string ReceiverCorrespondentAddr1
                                                                    , string ReceiverCorrespondentAddr2
                                                                    , string ReceiverCorrespondentAddr3

                                                                    , string SenderCorrespondentType
                                                                    , string SenderCorrespondentNo
                                                                    , string SenderCorrespondentName
                                                                    , string SenderCorrespondentAddr1
                                                                    , string SenderCorrespondentAddr2
                                                                    , string SenderCorrespondentAddr3


            )
        {
            sqldata.ndkExecuteNonQuery("B_BINCOMINGCOLLECTIONPAYMENTMT400_Insert", CollectionPaymentCode
                                       , GeneralMT400
                                       , SendingBankTRN
                                       , RelatedReference
                                       , AmountCollected
                                       , ValueDate
                                       , Currency
                                       , Amount
                                       //,SenderCorrespondent
                                       //,ReceiverCorrespondent
                                       , DetailOfCharges1
                                       , DetailOfCharges2
                                       , SenderCorrespondent1
                                       , SenderCorrespondent2
                                       , ReceiverCorrespondent1
                                       , ReceiverCorrespondent2

                                       , ReceiverCorrespondentType
                                       , ReceiverCorrespondentNo
                                       , ReceiverCorrespondentName
                                       , ReceiverCorrespondentAddr1
                                       , ReceiverCorrespondentAddr2
                                       , ReceiverCorrespondentAddr3

                                       , SenderCorrespondentType
                                       , SenderCorrespondentNo
                                       , SenderCorrespondentName
                                       , SenderCorrespondentAddr1
                                       , SenderCorrespondentAddr2
                                       , SenderCorrespondentAddr3


                );
        }

        public static DataSet B_BINCOMINGCOLLECTIONPAYMENTMT400_Report(string code)
        {
            return sqldata.ndkExecuteDataset("B_BINCOMINGCOLLECTIONPAYMENTMT400_Report", code);
        }

        public static DataSet B_BINCOMINGCOLLECTIONPAYMENT_HOADONVAT(string code, string UserNameLogin)
        {
            return sqldata.ndkExecuteDataset("B_BINCOMINGCOLLECTIONPAYMENT_HOADONVAT", code, UserNameLogin);
        }

        public static DataSet B_BOVERSEASTRANSFER_PHIEUCHUYENKHOAN(string code, string UserNameLogin)
        {
            return sqldata.ndkExecuteDataset("B_BOVERSEASTRANSFER_PHIEUCHUYENKHOAN", code, UserNameLogin);
        }

        public static DataSet BCOLLATERALRIGHT_GetByCustomer(string customerid)
        {
            return sqldata.ndkExecuteDataset("BCOLLATERALRIGHT_GetByCustomer", customerid);
        }

        public static DataSet B_CUSTOMER_LIMIT_SUB_GetByCustomer(string customerid)
        {
            return sqldata.ndkExecuteDataset("B_CUSTOMER_LIMIT_SUB_GetByCustomer", customerid);
        }

        public static DataSet B_BACCOUNTOFFICER_GetAll()
        {
            return sqldata.ndkExecuteDataset("B_BACCOUNTOFFICER_GetAll");
        }

        public static DataSet BINTEREST_TERM_GetAll()
        {
            return sqldata.ndkExecuteDataset("BINTEREST_TERM_GetAll");
        }

        public static DataSet B_BCURRENCY_GetAll()
        {
            return sqldata.ndkExecuteDataset("B_BCURRENCY_GetAll");
        }

        public static DataSet B_BCOUNTRY_GetAll()
        {
            return sqldata.ndkExecuteDataset("B_BCOUNTRY_GetAll");
        }

        public static DataSet B_BDRFROMACCOUNT_GetAll()
        {
            return sqldata.ndkExecuteDataset("B_BDRFROMACCOUNT_GetAll");
        }

        public static DataSet B_BCRFROMACCOUNT_GetAll(string customerName2)
        {
            return sqldata.ndkExecuteDataset("B_BCRFROMACCOUNT_GetAll", customerName2);
        }

        public static DataSet B_BBANKING_GetAll()
        {
            return sqldata.ndkExecuteDataset("B_BBANKING_GetAll");
        }


        public static DataSet B_BEXPORT_DOCUMETARYCOLLECTION_GetByDocCollectCode(string code)
        {
            return sqldata.ndkExecuteDataset("B_BEXPORT_DOCUMETARYCOLLECTION_GetByDocCollectCode", code);
        }

        public static void B_BEXPORT_DOCUMETARYCOLLECTIONCHARGES_Insert(string NormalLCCode, string WaiveCharges,
                                                                        string Chargecode, string ChargeAcct,
                                                                        string ChargePeriod, string ChargeCcy
                                                                        , string ExchRate, string ChargeAmt,
                                                                        string PartyCharged, string OmortCharges,
                                                                        string AmtInLocalCCY, string AmtDRfromAcct,
                                                                        string ChargeStatus
                                                                        , string ChargeRemarks, string VATNo,
                                                                        string TaxCode, string TaxCcy, string TaxAmt,
                                                                        string TaxinLCCYAmt, string TaxDate,
                                                                        string Rowchages)
        {
            sqldata.ndkExecuteNonQuery("B_BEXPORT_DOCUMETARYCOLLECTIONCHARGES_Insert", NormalLCCode, WaiveCharges,
                                       Chargecode, ChargeAcct, ChargePeriod, ChargeCcy
                                       , ExchRate, ChargeAmt, PartyCharged, OmortCharges, AmtInLocalCCY, AmtDRfromAcct,
                                       ChargeStatus
                                       , ChargeRemarks, VATNo, TaxCode, TaxCcy, TaxAmt, TaxinLCCYAmt, TaxDate, Rowchages);
        }

        public static void B_BEXPORT_DOCUMETARYCOLLECTION_Insert(string DocCollectCode
                                                                 , string DrawerCusNo
                                                                 , string DrawerCusName
                                                                 , string DrawerAddr1
                                                                 , string DrawerAddr2
                                                                 , string DrawerAddr3
                                                                 , string DrawerRefNo
                                                                 , string CollectingBankNo
                                                                 , string CollectingBankName
                                                                 , string CollectingBankAddr1
                                                                 , string CollectingBankAddr2
                                                                 , string CollectingBankAddr3
                                                                 , string CollectingBankAcct
                                                                 , string DraweeCusNo
                                                                 , string DraweeCusName
                                                                 , string DraweeAddr1
                                                                 , string DraweeAddr2
                                                                 , string DraweeAddr3
                                                                 , string NostroCusNo
                                                                 , string Currency
                                                                 , string Amount
                                                                 , string DocsReceivedDate
                                                                 , string MaturityDate
                                                                 , string Tenor
                                                                 , string Days
                                                                 , string TracerDate
                                                                 , string ReminderDays
                                                                 , string Commodity
                                                                 , string DocsCode1
                                                                 , string NoOfOriginals1
                                                                 , string NoOfCopies1
                                                                 , string DocsCode2
                                                                 , string NoOfOriginals2
                                                                 , string NoOfCopies2
                                                                 , string DocsCode3
                                                                 , string NoOfOriginals3
                                                                 , string NoOfCopies3
                                                                 , string OtherDocs
                                                                 , string Remarks
                                                                 , string CurrentUserId
                                                                 , string CollectionType
                                                                 , string CancelDate
                                                                 , string ContingentExpiryDate
                                                                 , string CancelRemark
                                                                 , string screenType
                                                                 )
        {
            sqldata.ndkExecuteNonQuery("B_BEXPORT_DOCUMETARYCOLLECTION_Insert", DocCollectCode
                                       , DrawerCusNo
                                       , DrawerCusName
                                       , DrawerAddr1
                                       , DrawerAddr2
                                       , DrawerAddr3
                                       , DrawerRefNo
                                       , CollectingBankNo
                                       , CollectingBankName
                                       , CollectingBankAddr1
                                       , CollectingBankAddr2
                                       , CollectingBankAddr3
                                       , CollectingBankAcct
                                       , DraweeCusNo
                                       , DraweeCusName
                                       , DraweeAddr1
                                       , DraweeAddr2
                                       , DraweeAddr3
                                       , NostroCusNo
                                       , Currency
                                       , Amount
                                       , DocsReceivedDate
                                       , MaturityDate
                                       , Tenor
                                       , Days
                                       , TracerDate
                                       , ReminderDays
                                       , Commodity
                                       , DocsCode1
                                       , NoOfOriginals1
                                       , NoOfCopies1
                                       , DocsCode2
                                       , NoOfOriginals2
                                       , NoOfCopies2
                                       , DocsCode3
                                       , NoOfOriginals3
                                       , NoOfCopies3
                                       , OtherDocs
                                       , Remarks
                                       , CurrentUserId
                                       , CollectionType
                                       , CancelDate
                                       , ContingentExpiryDate
                                       , CancelRemark
                                       , screenType);
        }

        public static DataSet B_BEXPORT_DOCUMETARYCOLLECTION_GetbyStatus(string status, string currentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BEXPORT_DOCUMETARYCOLLECTION_GetbyStatus", status, currentUserId);
        }

        public static void B_BEXPORT_DOCUMETARYCOLLECTION_UpdateStatus(string DocCollectCode, string status,
                                                                       string authorizedBy, string screenType)
        {
            sqldata.ndkExecuteNonQuery("B_BEXPORT_DOCUMETARYCOLLECTION_UpdateStatus", DocCollectCode, status,
                                       authorizedBy, screenType);
        }

        public static DataSet B_BDRFROMACCOUNT_GetByCurrency(string name, string currencyCode)
        {
            return sqldata.ndkExecuteDataset("B_BDRFROMACCOUNT_GetByCurrency", name, currencyCode);
        }

        public static DataTable B_BDRFROMACCOUNT_GetById(string Id)
        {
            return sqldata.ndkExecuteDataset("B_BDRFROMACCOUNT_GetById", Id).Tables[0];
        }

        public static DataTable B_BDRFROMACCOUNT_GetByName(string Id)
        {
            return sqldata.ndkExecuteDataset("B_BDRFROMACCOUNT_GetByName", Id).Tables[0];
        }


        public static DataSet B_BEXPORT_DOCUMETARYCOLLECTION_GetByAmendment(string DocCollectCode, string Drawee,
                                                                            string DraweeAddr, string Drawer,
                                                                            string DrawerAddrr, string Amend_Status,
                                                                            string currentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BEXPORT_DOCUMETARYCOLLECTION_GetByAmendment", DocCollectCode, Drawee,
                                             DraweeAddr, Drawer, DrawerAddrr, Amend_Status, currentUserId);
        }

        public static DataSet B_BINCOMINGCOLLECTIONPAYMENTPHIEUNHAPNGOAIBANG_Report(string code, string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("B_BINCOMINGCOLLECTIONPAYMENTPHIEUNHAPNGOAIBANG_Report", code,
                                             currentuserlogin);
        }

        public static DataSet B_BRPODCATEGORY_GetAll_IdOver200()
        {
            return sqldata.ndkExecuteDataset("B_BRPODCATEGORY_GetAll_IdOver200");
        }

        public static DataSet B_BRPODCATEGORY_GetSubAll_IdOver200(string catId)
        {
            return sqldata.ndkExecuteDataset("B_BRPODCATEGORY_GetSubAll_IdOver200", catId);
        }

        public static DataSet B_BLOANGROUP_GetAll()
        {
            return sqldata.ndkExecuteDataset("B_BLOANGROUP_GetAll");
        }

        public static DataSet B_BLOANPURPOSE_GetAll()
        {
            return sqldata.ndkExecuteDataset("B_BLOANPURPOSE_GetAll");
        }

        public static DataSet B_BDOCUMETARYCOLLECTION_PHIEUNHAPNGOAIBANG_Report(string code, string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("B_BDOCUMETARYCOLLECTION_PHIEUNHAPNGOAIBANG_Report", code,
                                             currentuserlogin);
        }

        public static DataSet P_BEXPORTDOCUMETARYCOLLECTION_PHIEUNHAPNGOAIBANG1_Report(string code, string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("P_BEXPORTDOCUMETARYCOLLECTION_PHIEUNHAPNGOAIBANG1_Report", code,
                                             currentuserlogin);
        }
        public static DataSet P_BEXPORTDOCUMETARYCOLLECTION_PHIEUNHAPNGOAIBANG2_Report(string code, string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("P_BEXPORTDOCUMETARYCOLLECTION_PHIEUNHAPNGOAIBANG2_Report", code,
                                             currentuserlogin);
        }
        public static DataSet P_BEXPORTDOCUMETARYCOLLECTION_PHIEUXUATNGOAIBANG1_Report(string code, string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("P_BEXPORTDOCUMETARYCOLLECTION_PHIEUXUATNGOAIBANG1_Report", code,
                                             currentuserlogin);
        }
        public static DataSet P_BEXPORTDOCUMETARYCOLLECTION_AMEND_PHIEUXUATNGOAIBANG_Report(string code, string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("P_BEXPORTDOCUMETARYCOLLECTION_AMEND_PHIEUXUATNGOAIBANG_Report", code,
                                             currentuserlogin);
        }
        public static DataSet P_BEXPORTDOCUMETARYCOLLECTION_AMEND_PHIEUNHAPNGOAIBANG_Report(string code, string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("P_BEXPORTDOCUMETARYCOLLECTION_AMEND_PHIEUNHAPNGOAIBANG_Report", code,
                                             currentuserlogin);
        }
        public static DataSet P_BEXPORTDOCUMETARYCOLLECTION_CANCEL_PHIEUXUATNGOAIBANG_Report(string code,
                                                                                      string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("B_DOCUMENTARYCOLLECTIONCANCEL_PHIEUXUATNGOAIBANG_REPORT", code,
                                             currentuserlogin);
        }
        public static DataTable B_BBANKSWIFTCODE_GetByCode(string Code)
        {
            return sqldata.ndkExecuteDataset("B_BBANKSWIFTCODE_GetByCode", Code).Tables[0];
        }

        public static DataSet B_BDOCUMETARYCOLLECTIONMT410_Report(string Code)
        {
            return sqldata.ndkExecuteDataset("B_BDOCUMETARYCOLLECTIONMT410_Report", Code);
        }

        public static DataSet B_BDOCUMETARYCOLLECTION_VAT_Report(string code, string currentuserlogin, int ViewType)
        {
            return sqldata.ndkExecuteDataset("B_BDOCUMETARYCOLLECTION_VAT_Report", code,
                                             currentuserlogin, ViewType);
        }

        // IncomingCollectionAmendment
        public static DataSet B_INCOMINGCOLLECTIONAMENDMENT_PHIEUXUATNGOAIBANG_REPORT(string code,
                                                                                      string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("B_INCOMINGCOLLECTIONAMENDMENT_PHIEUXUATNGOAIBANG_REPORT", code,
                                             currentuserlogin);
        }

        public static DataSet B_INCOMINGCOLLECTIONAMENDMENT_VAT_Report(string code, string currentuserlogin,
                                                                       int ViewType)
        {
            return sqldata.ndkExecuteDataset("B_INCOMINGCOLLECTIONAMENDMENT_VAT_Report", code,
                                             currentuserlogin, ViewType);
        }

        public static DataSet B_INCOMINGCOLLECTIONAMENDMENT_PHIEUNHAPNGOAIBANG_Report(string code,
                                                                                      string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("B_INCOMINGCOLLECTIONAMENDMENT_PHIEUNHAPNGOAIBANG_Report", code,
                                             currentuserlogin);
        }

        public static DataSet B_INCOMINGCOLLECTIONAMENDMENT_MT410_Report(string Code)
        {
            return sqldata.ndkExecuteDataset("B_INCOMINGCOLLECTIONAMENDMENT_MT410_Report", Code);
        }

        public static DataSet B_BCUSTOMERS_GetCompany()
        {
            return sqldata.ndkExecuteDataset("B_BCUSTOMERS_GetCompany");
        }

        //  Documentary Collection Cancel
        public static DataSet B_DOCUMENTARYCOLLECTIONCANCEL_PHIEUXUATNGOAIBANG_REPORT(string code,
                                                                                      string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("B_DOCUMENTARYCOLLECTIONCANCEL_PHIEUXUATNGOAIBANG_REPORT", code,
                                             currentuserlogin);
        }

        public static DataSet B_DOCUMENTARYCOLLECTIONCANCEL_VAT_REPORT(string code, string currentuserlogin,
                                                                       int ViewType)
        {
            return sqldata.ndkExecuteDataset("B_DOCUMENTARYCOLLECTIONCANCEL_VAT_REPORT", code,
                                             currentuserlogin, ViewType);
        }

        //  Incoming Collection Acception
        public static DataSet B_INCOMINGCOLLECTIONACCEPTION_MT410_Report(string Code)
        {
            return sqldata.ndkExecuteDataset("B_INCOMINGCOLLECTIONACCEPTION_MT410_Report", Code);
        }

        public static DataSet B_INCOMINGCOLLECTIONACCEPTION_VAT_REPORT(string code, string currentuserlogin,
                                                                       int ViewType)
        {
            return sqldata.ndkExecuteDataset("B_INCOMINGCOLLECTIONACCEPTION_VAT_REPORT", code,
                                             currentuserlogin, ViewType);
        }

        public static DataSet B_INCOMINGCOLLECTIONACCEPTION_PHIEUNHAPNGOAIBANG_Report(string code,
                                                                                      string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("B_INCOMINGCOLLECTIONACCEPTION_PHIEUNHAPNGOAIBANG_Report", code,
                                             currentuserlogin);
        }

        // Overseas Funds Transfer
        public static DataSet B_BOVERSEASTRANSFER_VAT_REPORT(string code, string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("B_BOVERSEASTRANSFER_VAT_REPORT", code,
                                             currentuserlogin);
        }

        public static DataSet B_INCOMINGCOLLECTIONPAYMENT_VAT_B_Report(string code, string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("B_INCOMINGCOLLECTIONPAYMENT_VAT_B_Report", code,
                                             currentuserlogin);
        }

        public static DataSet B_INCOMINGCOLLECTIONPAYMENT_PHIEUCHUYENKHOAN_Report(string code, string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("B_INCOMINGCOLLECTIONPAYMENT_PHIEUCHUYENKHOAN_Report", code,
                                             currentuserlogin);
        }

        public static DataSet B_BCOMMODITY_GetByTransactionType(string TransactionType)
        {
            return sqldata.ndkExecuteDataset("B_BCOMMODITY_GetByTransactionType", TransactionType);
        }

        public static DataTable B_BCHARGECODE_GetByViewType(int viewType)
        {
            return sqldata.ndkExecuteDataset("B_BCHARGECODE_GetByViewType", viewType).Tables[0];
        }

        public static DataTable B_INCOMINGCOLLECTIONPAYMENT_CheckAvailableAmt(string OrginalCode, string Code)
        {
            return
                sqldata.ndkExecuteDataset("B_INCOMINGCOLLECTIONPAYMENT_CheckAvailableAmt", OrginalCode, Code).Tables[0];
        }

        public static DataSet B_PROVISIONTRANSFER_DC_GetByLCNo(string code, string TransactionType)
        {
            return sqldata.ndkExecuteDataset("B_PROVISIONTRANSFER_DC_GetByLCNo", code, TransactionType);
        }

        public static DataTable B_BDRFROMACCOUNT_GetByIdAndCurrency(string Id, string currency)
        {
            return sqldata.ndkExecuteDataset("B_BDRFROMACCOUNT_GetByIdAndCurrency", Id, currency).Tables[0];
        }

        public static DataTable B_BCRFROMACCOUNT_GetById(string Id)
        {
            return sqldata.ndkExecuteDataset("B_BCRFROMACCOUNT_GetById", Id).Tables[0];
        }

        // Foreign Exchange
        public static void B_BFOREIGNEXCHANGE_Insert(string Code
                                                     , string TransactionType
                                                     , string FTNo
                                                     , string DealType
                                                     , string Counterparty
                                                     , string DealDate
                                                     , string ValueDate
                                                     , string ExchangeType
                                                     , string BuyCurrency
                                                     , double BuyAmount
                                                     , string SellCurrency
                                                     , double SellAmount
                                                     , double Rate
                                                     , string CustomerReceiving
                                                     , string CustomerPaying
                                                     , string AccountOfficer
                                                     , int CurrentUserId)
        {
            sqldata.ndkExecuteNonQuery("B_BFOREIGNEXCHANGE_Insert", Code
                                       , TransactionType
                                       , FTNo
                                       , DealType
                                       , Counterparty
                                       , DealDate
                                       , ValueDate
                                       , ExchangeType
                                       , BuyCurrency
                                       , BuyAmount
                                       , SellCurrency
                                       , SellAmount
                                       , Rate
                                       , CustomerReceiving
                                       , CustomerPaying
                                       , AccountOfficer
                                       , CurrentUserId);
        }

        public static void B_BFOREIGNEXCHANGE_UpdateStatus(string CommandName, string Code, int CurrentUserId)
        {
            sqldata.ndkExecuteNonQuery("B_BFOREIGNEXCHANGE_UpdateStatus", CommandName, Code, CurrentUserId);
        }

        public static DataSet B_BFOREIGNEXCHANGE_GetByCode(string Code)
        {
            return sqldata.ndkExecuteDataset("B_BFOREIGNEXCHANGE_GetByCode", Code);
        }

        public static DataTable B_BFOREIGNEXCHANGE_CheckCustomerReceivingAC(string CustomerReceivingCode,
                                                                            string BuyCurrency, string CounterpartyId)
        {
            return
                sqldata.ndkExecuteDataset("B_BFOREIGNEXCHANGE_CheckCustomerReceivingAC", CustomerReceivingCode,
                                          BuyCurrency, CounterpartyId).Tables[0];
        }

        public static DataTable B_BFOREIGNEXCHANGE_CheckCustomerPayingAC(string CustomerPayingCode,
                                                                         string SellCurrency, string CounterpartyName)
        {
            return
                sqldata.ndkExecuteDataset("B_BFOREIGNEXCHANGE_CheckCustomerPayingAC", CustomerPayingCode,
                                          SellCurrency, CounterpartyName).Tables[0];
        }

        public static DataSet B_BFOREIGNEXCHANGE_GetByPreview(int CurrentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BFOREIGNEXCHANGE_GetByPreview", CurrentUserId);
        }

        public static DataTable B_BFOREIGNEXCHANGE_CheckFTNo(string FTNo, string TransactionType)
        {
            return sqldata.ndkExecuteDataset("B_BFOREIGNEXCHANGE_CheckFTNo", FTNo, TransactionType).Tables[0];
        }

        public static DataSet PROVISIONTRANSFER_DC_GetByPreview(string RefNo, string LCNo, string Status, int UserID)
        {
            return sqldata.ndkExecuteDataset("PROVISIONTRANSFER_DC_GetByPreview", RefNo, LCNo, Status, UserID);
        }

        public static DataTable B_BSWIFTCODE_GetByCode(string Code)
        {
            return sqldata.ndkExecuteDataset("B_BSWIFTCODE_GetByCode", Code).Tables[0];
        }

        public static DataTable B_BDOCUMETARYCOLLECTION_GetByCode(string Code)
        {
            return sqldata.ndkExecuteDataset("B_BDOCUMETARYCOLLECTION_GetByCode", Code).Tables[0];
        }

        public static void B_BFREETEXTMESSAGE_Insert(string Id
                                                     , string WaiveCharge
                                                     , string TFNo
                                                     , string CableType
                                                     , string ReviverDesc
                                                     , string ReviverCode
                                                     , string RelatedReference
                                                     , string Narrative
                                                     , Int32 CurrentUserId)
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }
            sqldata.ndkExecuteNonQuery("B_BFREETEXTMESSAGE_Insert", Id
                                       , WaiveCharge
                                       , TFNo
                                       , CableType
                                       , ReviverDesc
                                       , ReviverCode
                                       , RelatedReference
                                       , Narrative
                                       , CurrentUserId);
        }

        public static DataTable B_BFREETEXTMESSAGE_GetById(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }
            return sqldata.ndkExecuteDataset("B_BFREETEXTMESSAGE_GetById", Id).Tables[0];
        }

        public static void B_BFREETEXTMESSAGE_UpdateSatus(string id, string status, int CurrentUserId)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = "0";
            }
            sqldata.ndkExecuteNonQuery("B_BFREETEXTMESSAGE_UpdateSatus", id, status, CurrentUserId);
        }

        public static DataSet B_BFREETEXTMESSAGE_GetByStatus(int CurrentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BFREETEXTMESSAGE_GetByStatus", CurrentUserId);
        }

        public static DataSet B_BFREETEXTMESSAGE_Report(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }
            return sqldata.ndkExecuteDataset("B_BFREETEXTMESSAGE_Report", Id);
        }

        public static DataSet B_BDOCUMETARYCOLLECTION_GetByEnquiry(string LCCode, string TransactionType,
                                                                   string CountryCode, string Commodity, string CusId,
                                                                   string CusName, int currentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BDOCUMETARYCOLLECTION_GetByEnquiry", LCCode, TransactionType,
                                             CountryCode, Commodity, CusId, CusName, currentUserId);
        }

        // =========================================================================================

        public static DataSet B_BIMPORT_NORMAILLC_GetByNormalLCCode(string code, int tabId)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_GetByNormalLCCode", code, tabId);
        }

        public static DataSet B_BCUSTOMER_INFO_GetByStatus(string status)
        {
            return sqldata.ndkExecuteDataset("B_BCUSTOMER_INFO_GetByStatus", status);
        }

        public static void B_BIMPORT_NORMAILLC_Insert(string NormalLCCode
                                                      , string LCType
                                                      , string ApplicantId
                                                      , string ApplicantName
                                                      , string ApplicantAddr1
                                                      , string ApplicantAddr2
                                                      , string ApplicantAddr3
                                                      , string Currency
                                                      , double Amount
                                                      , double Amount_Old
                                                      , double CrTolerance
                                                      , double DrTolerance
                                                      , DateTime? IssuingDate
                                                      , DateTime? ExpiryDate
                                                      , string ExpiryPlace
                                                      , DateTime? ContingentExpiry
                                                      , string AccountOfficer
                                                      , string ContactNo
                                                      , string BeneficiaryType
                                                      , string BeneficiaryNo
                                                      , string BeneficiaryName
                                                      , string BeneficiaryAddr1
                                                      , string BeneficiaryAddr2
                                                      , string BeneficiaryAddr3
                                                      , string AdviseBankType
                                                      , string AdviseBankNo
                                                      , string AdviseBankName
                                                      , string AdviseBankAddr1
                                                      , string AdviseBankAddr2
                                                      , string AdviseBankAddr3
                                                      , string ReimbBankType
                                                      , string ReimbBankNo
                                                      , string ReimbBankName
                                                      , string ReimbBankAddr1
                                                      , string ReimbBankAddr2
                                                      , string ReimbBankAddr3
                                                      , string AdviseThruType
                                                      , string AdviseThruNo
                                                      , string AdviseThruName
                                                      , string AdviseThruAddr1
                                                      , string AdviseThruAddr2
                                                      , string AdviseThruAddr3
                                                      , string AvailWithType
                                                      , string AvailWithNo
                                                      , string AvailWithName
                                                      , string AvailWithAddr1
                                                      , string AvailWithAddr2
                                                      , string AvailWithAddr3
                                                      , string Commodity
                                                      , double Prov
                                                      , double LCAmountSecured
                                                      , double LCAmountUnSecured
                                                      , double LoanPrincipal
                                                      , int CurrentUserId
                                                      , int TabId
                                                      , DateTime? CancelLCDate
                                                      , DateTime? ContingentExpiryDate
                                                      , string CancelRemark
            )
        {
            //DateTime? xCancelLCDate = null, xContingentExpiryDate = null;
            //if (CancelLCDate != null)
            //{
            //    xCancelLCDate = DateTime.Parse(CancelLCDate);
            //}
            //if (ContingentExpiryDate != null)
            //{
            //    xContingentExpiryDate = DateTime.Parse(ContingentExpiryDate);
            //}
            sqldata.ndkExecuteNonQuery("B_BIMPORT_NORMAILLC_Insert", NormalLCCode
                                       , LCType
                                       , ApplicantId
                                       , ApplicantName
                                       , ApplicantAddr1
                                       , ApplicantAddr2
                                       , ApplicantAddr3
                                       , Currency
                                       , Amount
                                       , Amount_Old
                                       , CrTolerance
                                       , DrTolerance
                                       , IssuingDate
                                       , ExpiryDate
                                       , ExpiryPlace
                                       , ContingentExpiry
                                       , AccountOfficer
                                       , ContactNo
                                       , BeneficiaryType
                                       , BeneficiaryNo
                                       , BeneficiaryName
                                       , BeneficiaryAddr1
                                       , BeneficiaryAddr2
                                       , BeneficiaryAddr3
                                       , AdviseBankType
                                       , AdviseBankNo
                                       , AdviseBankName
                                       , AdviseBankAddr1
                                       , AdviseBankAddr2
                                       , AdviseBankAddr3
                                       , ReimbBankType
                                       , ReimbBankNo
                                       , ReimbBankName
                                       , ReimbBankAddr1
                                       , ReimbBankAddr2
                                       , ReimbBankAddr3
                                       , AdviseThruType
                                       , AdviseThruNo
                                       , AdviseThruName
                                       , AdviseThruAddr1
                                       , AdviseThruAddr2
                                       , AdviseThruAddr3
                                       , AvailWithType
                                       , AvailWithNo
                                       , AvailWithName
                                       , AvailWithAddr1
                                       , AvailWithAddr2
                                       , AvailWithAddr3
                                       , Commodity
                                       , Prov
                                       , LCAmountSecured
                                       , LCAmountUnSecured
                                       , LoanPrincipal
                                       , CurrentUserId
                                       , TabId
                                       , CancelLCDate
                                       , ContingentExpiryDate
                                       , CancelRemark
                );
        }



        public static void B_BIMPORT_NORMAILLC_MT700_Insert(string NormalLCCode
                                                            , string ReceivingBank
                                                            , string SequenceOfTotal
                                                            , string FormDocumentaryCredit
                                                            , string DocumentaryCreditNumber
                                                            , DateTime? DateOfIssue
                                                            , DateTime? DateExpiry
                                                            , string PlaceOfExpiry
                                                            , string ApplicationRule
                                                            , string ApplicantType
                                                            , string ApplicantNo
                                                            , string ApplicantName
                                                            , string ApplicantAddr1
                                                            , string ApplicantAddr2
                                                            , string ApplicantAddr3
                                                            , string Currency
                                                            , double Amount
                                                            , double PercentageCredit
                                                            , double AmountTolerance
                                                            , string MaximumCreditAmount
                                                            , string AvailableWithType
                                                            , string AvailableWithNo
                                                            , string AvailableWithName
                                                            , string AvailableWithAddr1
                                                            , string AvailableWithAddr2
                                                            , string AvailableWithAddr3
                                                            , string Available_By
                                                            , string DraweeType
                                                            , string DraweeNo
                                                            , string DraweeName
                                                            , string DraweeAddr1
                                                            , string DraweeAddr2
                                                            , string DraweeAddr3
                                                            , string MixedPaymentDetails
                                                            , string DeferredPaymentDetails
                                                            , string PatialShipment
                                                            , string Transhipment
                                                            , string PlaceOfTakingInCharge
                                                            , string PortOfLoading
                                                            , string PortOfDischarge
                                                            , string PlaceOfFinalInDistination
                                                            , DateTime? LatesDateOfShipment
                                                            , string ShipmentPeriod
                                                            , string DescrpGoodsBervices
                                                            , string DocsRequired
                                                            , string AdditionalConditions
                                                            , string Charges
                                                            , string PeriodForPresentation
                                                            , string ConfimationInstructions
                                                            , string InstrToPaygAccptgNegotgBank
                                                            , string SenderReceiverInfomation

                                                            , string BeneficiaryType
                                                            , string BeneficiaryNo
                                                            , string BeneficiaryName
                                                            , string BeneficiaryAddr1
                                                            , string BeneficiaryAddr2
                                                            , string BeneficiaryAddr3

                                                            , string AdviseThroughBankType
                                                            , string AdviseThroughBankNo
                                                            , string AdviseThroughBankName
                                                            , string AdviseThroughBankAddr1
                                                            , string AdviseThroughBankAddr2
                                                            , string AdviseThroughBankAddr3

                                                            , string AdviseThruType
                                                            , string AdviseThruNo
                                                            , string AdviseThruName
                                                            , string AdviseThruAddr1
                                                            , string AdviseThruAddr2
                                                            , string AdviseThruAddr3

                                                            , string AdditionalAmountsCovered1
                                                            , string AdditionalAmountsCovered2
                                                            , string DraftsAt1
                                                            , string DraftsAt2
                                                            , string MixedPaymentDetails1
                                                            , string MixedPaymentDetails2
                                                            , string DeferredPaymentDetails1
                                                            , string DeferredPaymentDetails2
                                                            , string ShipmentPeriod1
                                                            , string ShipmentPeriod2
            , string MixedPaymentDetails3
            , string MixedPaymentDetails4
            , string DeferredPaymentDetails3
            , string DeferredPaymentDetails4
            , string ShipmentPeriod3
            , string ShipmentPeriod4
            , string ShipmentPeriod5
            , string ShipmentPeriod6
            )
        {
            sqldata.ndkExecuteNonQuery("B_BIMPORT_NORMAILLC_MT700_Insert", NormalLCCode
                                       , ReceivingBank
                                       , SequenceOfTotal
                                       , FormDocumentaryCredit
                                       , DocumentaryCreditNumber
                                       , DateOfIssue
                                       , DateExpiry
                                       , PlaceOfExpiry
                                       , ApplicationRule
                                       , ApplicantType
                                       , ApplicantNo
                                       , ApplicantName
                                       , ApplicantAddr1
                                       , ApplicantAddr2
                                       , ApplicantAddr3
                                       , Currency
                                       , Amount
                                       , PercentageCredit
                                       , AmountTolerance
                                       , MaximumCreditAmount
                                       , AvailableWithType
                                       , AvailableWithNo
                                       , AvailableWithName
                                       , AvailableWithAddr1
                                       , AvailableWithAddr2
                                       , AvailableWithAddr3
                                       , Available_By
                                       , DraweeType
                                       , DraweeNo
                                       , DraweeName
                                       , DraweeAddr1
                                       , DraweeAddr2
                                       , DraweeAddr3
                                       , MixedPaymentDetails
                                       , DeferredPaymentDetails
                                       , PatialShipment
                                       , Transhipment
                                       , PlaceOfTakingInCharge
                                       , PortOfLoading
                                       , PortOfDischarge
                                       , PlaceOfFinalInDistination
                                       , LatesDateOfShipment
                                       , ShipmentPeriod
                                       , DescrpGoodsBervices
                                       , DocsRequired
                                       , AdditionalConditions
                                       , Charges
                                       , PeriodForPresentation
                                       , ConfimationInstructions
                                       , InstrToPaygAccptgNegotgBank
                                       , SenderReceiverInfomation

                                       , BeneficiaryType
                                       , BeneficiaryNo
                                       , BeneficiaryName
                                       , BeneficiaryAddr1
                                       , BeneficiaryAddr2
                                       , BeneficiaryAddr3

                                       , AdviseThroughBankType
                                       , AdviseThroughBankNo
                                       , AdviseThroughBankName
                                       , AdviseThroughBankAddr1
                                       , AdviseThroughBankAddr2
                                       , AdviseThroughBankAddr3

                                       , AdviseThruType
                                       , AdviseThruNo
                                       , AdviseThruName
                                       , AdviseThruAddr1
                                       , AdviseThruAddr2
                                       , AdviseThruAddr3
                                       , AdditionalAmountsCovered1
                                       , AdditionalAmountsCovered2
                                       , DraftsAt1
                                       , DraftsAt2
                                       , MixedPaymentDetails1
                                       , MixedPaymentDetails2
                                       , DeferredPaymentDetails1
                                       , DeferredPaymentDetails2
                                       , ShipmentPeriod1
                                       , ShipmentPeriod2

                                       , MixedPaymentDetails3
                                       , MixedPaymentDetails4

                                       , DeferredPaymentDetails3
                                       , DeferredPaymentDetails4

                                       , ShipmentPeriod3
                                       , ShipmentPeriod4
                                       , ShipmentPeriod5
                                       , ShipmentPeriod6);
        }

        public static void B_BIMPORT_NORMAILLC_MT740_Insert(string NormalLCCode
                                                            , string GenerateMT740
                                                            , string ReceivingBank
                                                            , string DocumentaryCreditNumber
                                                            , DateTime? DateExpiry
                                                            , string PlaceExpiry
                                                            , string BeneficiaryType
                                                            , string BeneficiaryNo
                                                            , string BeneficiaryName
                                                            , string BeneficiaryAddr1
                                                            , string BeneficiaryAddr2
                                                            , string BeneficiaryAddr3
                                                            , double CreditAmount
                                                            , string CreditCurrency
                                                            , string AvailableWithType
                                                            , string AvailableWithNo
                                                            , string AvailableWithName
                                                            , string AvailableWithAddr1
                                                            , string AvailableWithAddr2
                                                            , string AvailableWithAddr3
                                                            , string DraweeNo
                                                            , string DraweeName
                                                            , string DraweeAddr1
                                                            , string DraweeAddr2
                                                            , string DraweeAddr3
                                                            , string ReimbursingBankChanges,
                                                            string ApplicableRule,
                                                            double PercentageCreditAmountTolerance1,
                                                            double PercentageCreditAmountTolerance2,
                                                            string SenderToReceiverInformation1,
                                                            string SenderToReceiverInformation2,
                                                            string SenderToReceiverInformation3,
                                                            string SenderToReceiverInformation4,
                                                            string DraftsAt1,
                                                            string DraftsAt2
            )
        {
            sqldata.ndkExecuteNonQuery("B_BIMPORT_NORMAILLC_MT740_Insert", NormalLCCode
                                       , GenerateMT740
                                       , ReceivingBank
                                       , DocumentaryCreditNumber
                                       , DateExpiry
                                       , PlaceExpiry
                                       , BeneficiaryType
                                       , BeneficiaryNo
                                       , BeneficiaryName
                                       , BeneficiaryAddr1
                                       , BeneficiaryAddr2
                                       , BeneficiaryAddr3
                                       , CreditAmount
                                       , CreditCurrency
                                       , AvailableWithType
                                       , AvailableWithNo
                                       , AvailableWithName
                                       , AvailableWithAddr1
                                       , AvailableWithAddr2
                                       , AvailableWithAddr3
                                       , DraweeNo
                                       , DraweeName
                                       , DraweeAddr1
                                       , DraweeAddr2
                                       , DraweeAddr3
                                       , ReimbursingBankChanges
                                       , ApplicableRule
                                       , PercentageCreditAmountTolerance1
                                       , PercentageCreditAmountTolerance2
                                       , SenderToReceiverInformation1
                                       , SenderToReceiverInformation2
                                       , SenderToReceiverInformation3
                                       , SenderToReceiverInformation4
                                       , DraftsAt1
                                       , DraftsAt2);
        }


        public static void B_BIMPORT_NORMAILLC_CHARGE_Insert(string NormalLCCode
                                                             , string WaiveCharges
                                                             , string Chargecode
                                                             , string ChargeAcct
                                                             , string ChargePeriod
                                                             , string ChargeCcy
                                                             , double ChargeAmt
                                                             , string PartyCharged
                                                             , string OmortCharges
                                                             , string ChargeStatus
                                                             , string ChargeRemarks
                                                             , string VATNo
                                                             , string TaxCode
                                                             , double TaxAmt
                                                             , int Rowchages
                                                             , int TabId)
        {
            sqldata.ndkExecuteNonQuery("B_BIMPORT_NORMAILLC_CHARGE_Insert", NormalLCCode
                                       , WaiveCharges
                                       , Chargecode
                                       , ChargeAcct
                                       , ChargePeriod
                                       , ChargeCcy
                                       , ChargeAmt
                                       , PartyCharged
                                       , OmortCharges
                                       , ChargeStatus
                                       , ChargeRemarks
                                       , VATNo
                                       , TaxCode
                                       , TaxAmt
                                       , Rowchages
                                       , TabId);
        }

        public static DataSet B_BIMPORT_NORMAILLC_GetbyStatus(string uiType, int currentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_GetbyStatus", uiType, currentUserId);
        }

        public static DataSet B_BIMPORT_NORMAILLC_MT700_Report(string Code)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_MT700_Report", Code);
        }

        public static DataSet B_BIMPORT_NORMAILLC_MT740_Report(string Code)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_MT740_Report", Code);
        }

        public static DataSet B_BIMPORT_NORMAILLC_VAT_Report(string Code, string UserNameLogin, int TabId)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_VAT_Report", Code, UserNameLogin, TabId);
        }

        public static DataSet B_BIMPORT_NORMAILLC_PHIEUNHAPNGOAIBANG_Report(string Code, string UserNameLogin, int TabId)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_PHIEUNHAPNGOAIBANG_Report", Code, UserNameLogin, TabId);
        }

        public static void B_BIMPORT_NORMAILLC_UpdateStatus(string code, string status, string authorizedBy, int tabId)
        {
            sqldata.ndkExecuteNonQuery("B_BIMPORT_NORMAILLC_UpdateStatus", code, status, authorizedBy, tabId);
        }

        public static DataSet B_BIMPORT_NORMAILLC_GetByEnquiry(string code, string ApplicantId, string ApplicantName,
                                                               int currentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_GetByEnquiry", code, ApplicantId, ApplicantName,
                                             currentUserId);
        }

        public static DataSet B_PROVISIONTRANSFER_DC_PHIEUCHUYENKHOAN_REPORT(string code, string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("B_PROVISIONTRANSFER_DC_PHIEUCHUYENKHOAN_REPORT", code,
                                             currentuserlogin);
        }

        public static DataSet B_BIMPORT_NORMAILLC_AMEND_PHIEUXUATNGOAIBANG_REPORT(string code, string userlogin)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_AMEND_PHIEUXUATNGOAIBANG_REPORT", code, userlogin);
        }

        public static DataSet B_BIMPORT_NORMAILLC_AMEND_PHIEUNHAPNGOAIBANG_REPORT(string code, string userlogin)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_AMEND_PHIEUNHAPNGOAIBANG_REPORT", code, userlogin);
        }

        public static DataSet B_BIMPORT_NORMAILLC_AMEND_VAT_REPORT(string code, string userloginname, int TabId)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_AMEND_VAT_REPORT", code, userloginname, TabId);
        }

        public static DataTable B_BFOREIGNEXCHANGE_GetByCreditAccount(string Code, string Currency, string CustomerName,
                                                                      string CallFrom)
        {
            return sqldata.ndkExecuteDataset("B_BFOREIGNEXCHANGE_GetByCreditAccount", Code, Currency, CustomerName,
                                             CallFrom).Tables[0];
        }

        public static DataTable B_BFOREIGNEXCHANGE_GetByDebitAccount(string Code, string Currency, string CustomerName,
                                                                     string CallFrom)
        {
            return sqldata.ndkExecuteDataset("B_BFOREIGNEXCHANGE_GetByDebitAccount", Code, Currency, CustomerName,
                                             CallFrom).Tables[0];
        }

        public static DataSet B_BIMPORT_NORMAILLC_CANCEL_PHIEUXUATNGOAIBANG_REPORT(string code,
                                                                                   string currentuserlogin)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_CANCEL_PHIEUXUATNGOAIBANG_REPORT", code,
                                             currentuserlogin);
        }

        public static DataSet B_BIMPORT_NORMAILLC_CANCEL_VAT_REPORT(string code, string currentuserlogin,
                                                                    int ViewType)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_CANCEL_VAT_REPORT", code,
                                             currentuserlogin, ViewType);
        }

        public static int B_BFOREIGNEXCHANGE_ValidationLCNoExst_PROVISIONTRANSFER_DC(string lcNo)
        {
            return
                Convert.ToInt16(
                    sqldata.ndkExecuteDataset("B_BFOREIGNEXCHANGE_ValidationLCNoExst_PROVISIONTRANSFER_DC", lcNo).Tables
                        [0].Rows[0]["ProvitionTransferID"].ToString());
        }

        public static DataSet B_BCUSTOMERS_OnlyBusiness()
        {
            return sqldata.ndkExecuteDataset("B_BCUSTOMERS_OnlyBusiness");
        }


        public static DataSet B_BBANKSWIFTCODE_GetByType(string Type)
        {
            return sqldata.ndkExecuteDataset("B_BBANKSWIFTCODE_GetByType", Type);
        }

        public static DataTable B_TRANSACTIONREFERENCENUMBER_CheckByType(string Code, string type)
        {
            return sqldata.ndkExecuteDataset("B_TRANSACTIONREFERENCENUMBER_CheckByType", Code, type).Tables[0];
        }

        public static DataSet B_BDRAWTYPE_GetAll()
        {
            return sqldata.ndkExecuteDataset("B_BDRAWTYPE_GetAll");
        }

        public static void B_BIMPORT_DOCUMENTPROCESSING_Insert(string DocumentType
                                                               , string LCCode
                                                               , string DrawType
                                                               , string PresentorNo
                                                               , string PresentorName
                                                               , string PresentorRefNo
                                                               , string Currency
                                                               , double? Amount
                                                               , DateTime? BookingDate
                                                               , DateTime? DocsReceivedDate
                                                               , string DocsCode1
                                                               , double? NoOfOriginals1
                                                               , double? NoOfCopies1
                                                               , string DocsCode2
                                                               , double? NoOfOriginals2
                                                               , double? NoOfCopies2
                                                               , string DocsCode3
                                                               , double? NoOfOriginals3
                                                               , double? NoOfCopies3
                                                               , string OtherDocs1
                                                               , string OtherDocs2
                                                               , string OtherDocs3
                                                               , DateTime? TraceDate
                                                               , DateTime? DocsReceivedDate_Supplemental
                                                               , string PresentorRefNo_Supplemental
                                                               , string Docs_Supplemental1
                                                               , string Docs_Supplemental2
                                                               , string Docs_Supplemental3
                                                               , int CurrentUserId
                                                               , int ViewType
                                                               , string Discrepancies
                                                               , string DisposalOfDocs)
        {
            sqldata.ndkExecuteNonQuery("B_BIMPORT_DOCUMENTPROCESSING_Insert", DocumentType
                                       , LCCode
                                       , DrawType
                                       , PresentorNo
                                       , PresentorName
                                       , PresentorRefNo
                                       , Currency
                                       , Amount
                                       , BookingDate
                                       , DocsReceivedDate
                                       , DocsCode1
                                       , NoOfOriginals1
                                       , NoOfCopies1
                                       , DocsCode2
                                       , NoOfOriginals2
                                       , NoOfCopies2
                                       , DocsCode3
                                       , NoOfOriginals3
                                       , NoOfCopies3
                                       , OtherDocs1
                                       , OtherDocs2
                                       , OtherDocs3
                                       , TraceDate
                                       , DocsReceivedDate_Supplemental
                                       , PresentorRefNo_Supplemental
                                       , Docs_Supplemental1
                                       , Docs_Supplemental2
                                       , Docs_Supplemental3
                                       , CurrentUserId
                                       , ViewType
                                       , Discrepancies
                                       , DisposalOfDocs);
        }

        public static DataSet B_BIMPORT_DOCUMENTPROCESSING_GetByCode(string Code, int ViewType, int CurrentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_DOCUMENTPROCESSING_GetByCode", Code, ViewType, CurrentUserId);
        }

        public static DataTable B_BIMPORT_NORMAILLC_GetOne(string Code)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_GetOne", Code).Tables[0];
        }

        public static void B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(string code, string Status, int ViewType,
                                                                     int CurrentUserId)
        {
            sqldata.ndkExecuteNonQuery("B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus", code, Status, ViewType,
                                       CurrentUserId);
        }

        public static DataSet B_BIMPORT_DOCUMENTPROCESSING_GetByReview(int ViewType, int CurrentUserId)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_DOCUMENTPROCESSING_GetByReview", ViewType, CurrentUserId);
        }

        public static void B_BIMPORT_DOCUMENTPROCESSING_CHARGE_Insert(string LCCode
                                                                      , string WaiveCharges
                                                                      , string Chargecode
                                                                      , string ChargeAcct
                                                                      , string ChargePeriod
                                                                      , string ChargeCcy
                                                                      , double ChargeAmt
                                                                      , string PartyCharged
                                                                      , string OmortCharges
                                                                      , string ChargeStatus
                                                                      , string ChargeRemarks
                                                                      , string VATNo
                                                                      , string TaxCode
                                                                      , double TaxAmt
                                                                      , int Rowchages
                                                                      , int TabId)
        {
            sqldata.ndkExecuteNonQuery("B_BIMPORT_DOCUMENTPROCESSING_CHARGE_Insert", LCCode
                                       , WaiveCharges
                                       , Chargecode
                                       , ChargeAcct
                                       , ChargePeriod
                                       , ChargeCcy
                                       , ChargeAmt
                                       , PartyCharged
                                       , OmortCharges
                                       , ChargeStatus
                                       , ChargeRemarks
                                       , VATNo
                                       , TaxCode
                                       , TaxAmt
                                       , Rowchages
                                       , TabId);
        }


        public static void B_BIMPORT_DOCUMENTPROCESSING_MT734_Insert(string LCCode
                                                                    , string PresentorNo
                                                                    , string PresentorName
                                                                    , string PresentorAddr1
                                                                    , string PresentorAddr2
                                                                    , string PresentorAddr3
                                                                    , string SenderTRN
                                                                    , string PresentingBankRef
                                                                    , DateTime? DateUtilization
                                                                    , double? AmountUtilization
                                                                    , string Currency
                                                                    , string ChargesClaimed
                                                                    , string TotalAmountClaimed
                                                                    , string AccountWithBankNo
                                                                    , string AccountWithBanKName
                                                                    , string SendertoReceiverInfomation
                                                                    , string Discrepancies
                                                                    , string DisposalOfDocs)
        {
            sqldata.ndkExecuteNonQuery("B_BIMPORT_DOCUMENTPROCESSING_MT734_Insert", LCCode
                                                                                , PresentorNo
                                                                                , PresentorName
                                                                                , PresentorAddr1
                                                                                , PresentorAddr2
                                                                                , PresentorAddr3
                                                                                , SenderTRN
                                                                                , PresentingBankRef
                                                                                , DateUtilization
                                                                                , AmountUtilization
                                                                                , Currency
                                                                                , ChargesClaimed
                                                                                , TotalAmountClaimed
                                                                                , AccountWithBankNo
                                                                                , AccountWithBanKName
                                                                                , SendertoReceiverInfomation
                                                                                , Discrepancies
                                                                                , DisposalOfDocs);
        }

        public static DataTable B_BDRFROMACCOUNT_GetByNameWithoutVND(string CusName)
        {
            return sqldata.ndkExecuteDataset("B_BDRFROMACCOUNT_GetByNameWithoutVND", CusName).Tables[0];
        }

        public static DataSet B_BINTERNALBANKPAYMENTACCOUNT_GetAll()
        {
            return sqldata.ndkExecuteDataset("B_BINTERNALBANKPAYMENTACCOUNT_GetAll");
        }

        public static void B_BIMPORT_NORMAILLC_MT747_Insert(string NormalLCCode
                                                            , string GenerateMT747
                                                            , string ReceivingBank
                                                            , string DocumentaryCreditNumber
                                                            , string ReimbBankType
                                                            , string ReimbBankNo
                                                            , string ReimbBankName
                                                            , string ReimbBankAddr1
                                                            , string ReimbBankAddr2
                                                            , string ReimbBankAddr3
                                                            , DateTime? DateOriginalAuthorization
                                                            , DateTime? DateOfExpiry
                                                            , string Currency
                                                            , double? Amount
                                                            , double? PercentageCreditAmountTolerance1
                                                            , double? PercentageCreditAmountTolerance2
                                                            , string MaximumCreditAmount
                                                            , string AdditionalCovered1
                                                            , string AdditionalCovered2
                                                            , string AdditionalCovered3
                                                            , string AdditionalCovered4
                                                            , string SenderToReceiverInformation1
                                                            , string SenderToReceiverInformation2
                                                            , string SenderToReceiverInformation3
                                                            , string SenderToReceiverInformation4)
        {
            sqldata.ndkExecuteNonQuery("B_BIMPORT_NORMAILLC_MT747_Insert",  NormalLCCode 
                                                                        ,  GenerateMT747  
                                                                        ,  ReceivingBank   
                                                                        ,  DocumentaryCreditNumber 
                                                                        ,  ReimbBankType  
                                                                        ,  ReimbBankNo   
                                                                        ,  ReimbBankName   
                                                                        ,  ReimbBankAddr1   
                                                                        ,  ReimbBankAddr2   
                                                                        ,  ReimbBankAddr3   
                                                                        ,  DateOriginalAuthorization  
                                                                        ,  DateOfExpiry  
                                                                        ,  Currency 
                                                                        ,  Amount  
                                                                        ,  PercentageCreditAmountTolerance1  
                                                                        ,  PercentageCreditAmountTolerance2  
                                                                        ,  MaximumCreditAmount   
                                                                        ,  AdditionalCovered1
                                                                        , AdditionalCovered2
                                                                        , AdditionalCovered3
                                                                        , AdditionalCovered4 
                                                                        ,  SenderToReceiverInformation1   
                                                                        ,  SenderToReceiverInformation2   
                                                                        ,  SenderToReceiverInformation3   
                                                                        ,  SenderToReceiverInformation4   );
        }

        public static DataSet B_BIMPORT_NORMAILLC_MT707_Insert(string NormalLCCode
                                                                        , string ReceivingBank
                                                                        , string SenderReference
                                                                        , string ReceiverReference
                                                                        , string ReferenceToPreAdvice
                                                                        , string IssuingBankType
                                                                        , string IssuingBankNo
                                                                        , string IssuingBankName
                                                                        , string IssuingBankAddr1
                                                                        , string IssuingBankAddr2
                                                                        , string IssuingBankAddr3
                                                                        , DateTime? DateOfIssue
                                                                        , string ApplicableRule
                                                                        , DateTime? DateOfAmendment
                                                                        , string BeneficiaryType
                                                                        , string BeneficiaryNo
                                                                        , string BeneficiaryName
                                                                        , string BeneficiaryAddr1
                                                                        , string BeneficiaryAddr2
                                                                        , string BeneficiaryAddr3
                                                                        , DateTime? NewDateOfExpiry
                                                                        , double? PercentageCreditAmountTolerance1
                                                                        , double? PercentageCreditAmountTolerance2
                                                                        , string MaximumCreditAmount
                                                                        , string AdditionalAmountsCovered1
                                                                        , string AdditionalAmountsCovered2
                                                                        , string PlaceOfTakingInCharge
                                                                        , string PlaceOfFinalInDistination
                                                                        , DateTime? LatesDateOfShipment
                                                                        , string ShipmentPeriod1
                                                                        , string ShipmentPeriod2
                                                                        , string ShipmentPeriod3
                                                                        , string ShipmentPeriod4
                                                                        , string PortOfLoading
                                                                        , string PortOfDischarge
                                                                        , string Narrative
                                                                        , string SenderReceiverInfomation1
                                                                        , string SenderReceiverInfomation2
                                                                        , string SenderReceiverInfomation3
                                                                        , string SenderReceiverInfomation4)
        {
            return sqldata.ndkExecuteDataset("B_BIMPORT_NORMAILLC_MT707_Insert", NormalLCCode
                                                                                , ReceivingBank
                                                                                , SenderReference
                                                                                , ReceiverReference
                                                                                , ReferenceToPreAdvice
                                                                                , IssuingBankType
                                                                                , IssuingBankNo
                                                                                , IssuingBankName
                                                                                , IssuingBankAddr1
                                                                                , IssuingBankAddr2
                                                                                , IssuingBankAddr3
                                                                                , DateOfIssue
                                                                                , ApplicableRule
                                                                                , DateOfAmendment
                                                                                , BeneficiaryType
                                                                                , BeneficiaryNo
                                                                                , BeneficiaryName
                                                                                , BeneficiaryAddr1
                                                                                , BeneficiaryAddr2
                                                                                , BeneficiaryAddr3
                                                                                , NewDateOfExpiry
                                                                                , PercentageCreditAmountTolerance1
                                                                                , PercentageCreditAmountTolerance2
                                                                                , MaximumCreditAmount
                                                                                , AdditionalAmountsCovered1
                                                                                , AdditionalAmountsCovered2
                                                                                , PlaceOfTakingInCharge
                                                                                , PlaceOfFinalInDistination
                                                                                , LatesDateOfShipment
                                                                                , ShipmentPeriod1
                                                                                , ShipmentPeriod2
                                                                                , ShipmentPeriod3
                                                                                , ShipmentPeriod4
                                                                                , PortOfLoading
                                                                                , PortOfDischarge
                                                                                , Narrative
                                                                                , SenderReceiverInfomation1
                                                                                , SenderReceiverInfomation2
                                                                                , SenderReceiverInfomation3
                                                                                , SenderReceiverInfomation4);
        }
    }
}