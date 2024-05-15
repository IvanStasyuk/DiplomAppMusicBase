using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DiplomAppMusicBase
{
    internal class Manager
    {
        static public Frame MFrame;
        public static bool IsGuest { get; set; }

        public static int IsRole { get; set; }
        public static Grid GridBackground;
        public class InstrumentsList
        {
            public string Instrument { get; set; }
            public SqlMoney PriceInstrument { get; set; }
            public string FormattedPriceInstrument
            {
                get
                {
                    return string.Format("{0} ₽", PriceInstrument);
                }
            }
        }
        public class JanrsList
        {
            public string Janr { get; set; }
            public SqlMoney PriceJanr { get; set; }
            public string FormattedPriceJanr
            {
                get
                {
                    return string.Format("{0} ₽", PriceJanr);
                }
            }
        }
        public class EffectsList
        {
            public string Effect { get; set; }
            public SqlMoney PriceEffect { get; set; }
            public string FormattedPriceEffect
            {
                get
                {
                    return string.Format("{0} ₽", PriceEffect);
                }
            }
        }
    }
}
