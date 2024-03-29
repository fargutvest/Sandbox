﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;

namespace MTBReportParser
{
    public class Operation
    {
        private static string Operation_DateTime_ToString => ConfigurationManager.AppSettings[nameof(Operation_DateTime_ToString)];

        public DateTime DateTime { get; set; }

        public string Title { get; set; }

        public string Place { get; set; }

        public double Amount { get; set; }

        public string Currency { get; set; }

        public override string ToString()
        {
            var name = (typeof(Status).GetMember(Status.ToString())[0].GetCustomAttributes(typeof(DisplayAttribute), false)[0] as DisplayAttribute).Name;
            return $"{DateTime.ToString(Operation_DateTime_ToString, new CultureInfo("ru-RU"))} {GetDisplayName(Status)} {Amount.ToString()} {Currency} {Title} {Place}";
        }
       
        public Status Status { get; set; }

        private string GetDisplayName(Status status)
        {
            var type = typeof(Status);
            var memberInfo = type.GetMember(status.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
            return ((DisplayAttribute)attributes[0]).Name;
        }
    }
}
