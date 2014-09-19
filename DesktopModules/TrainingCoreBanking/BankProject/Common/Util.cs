﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace BankProject.Common
{
    public class Util
    {
        public static string getIDDate()
        {
            DateTime today = DateTime.Today;
            return today.ToString("yy") + today.DayOfYear.ToString().PadLeft(3, '0');

        }

        public static void LoadData2RadCombo<T>(RadComboBox ctl, List<T> db, string valueField, string textField, string emptyMessage)
        {
            if (ctl != null)
            {
                ctl.Text = "";
                ctl.Items.Clear();
                ctl.DataSource = db;
                ctl.DataValueField = valueField;
                ctl.DataTextField = textField;
                if (!String.IsNullOrEmpty(emptyMessage))
                {
                    ctl.EmptyMessage = emptyMessage;
                }
                ctl.DataBind();
            }
        }
        public static void LoadData2RadCombo<T>(RadComboBox ctl, List<T> db, string valueField, string textField)
        {
            LoadData2RadCombo(ctl, db, valueField, textField, null);
        }
        public static void LoadData2RadCombo(RadComboBox ctl, string emptyMessage)
        {
            ctl.Text = "";
            ctl.Items.Clear();
            if (!String.IsNullOrEmpty(emptyMessage))
            {
                ctl.EmptyMessage = emptyMessage;
            }
            ctl.DataBind();
        }

        public static void LoadData2RadCombo(RadComboBox ctl)
        {
            LoadData2RadCombo(ctl, null);
        }
    }
}