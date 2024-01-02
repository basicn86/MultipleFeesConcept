using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleFeesConcept.ViewModels
{
    public class ClosingDisclosureViewModel : ViewModelBase
    {
        public class CDRow
        {
            public string FeeType { get; set; } = "";
            public string? Payee { get; set; } = "";
            public string BorrowerAtClosing { get; set; } = "";
            public string BorrowerOutsideClosing { get; set; } = "";
            public string SellerAtClosing { get; set; } = "";
            public string PaidByOthers { get; set; } = "";
        }

        public ObservableCollection<CDRow> CDRows { get; }

        public ClosingDisclosureViewModel(IEnumerable<Models.Fee> fees)
        {
            CDRows = new ObservableCollection<CDRow>();

            foreach (Models.Fee fee in fees)
            {
                CDRow row = new CDRow();

                row.FeeType = fee.FeeType.name;
                row.Payee = fee.payee;
                if (fee.poc_amount > 0)
                {
                    if (fee.PocBy?.name == "borrower")
                    {
                        row.BorrowerOutsideClosing = "$" + fee?.poc_amount.ToString();
                    } else if (fee.PocBy?.name == "broker")
                    {
                        row.PaidByOthers = "$" + fee?.poc_amount.ToString();
                    } else if (fee.PocBy?.name == "seller")
                    {
                        row.SellerAtClosing = "$" + fee?.poc_amount.ToString();
                    }
                }

                if (fee?.amount - fee?.poc_amount != 0)
                {
                    row.BorrowerAtClosing = "$" + (fee.amount - fee.poc_amount).ToString();
                }

                CDRows.Add(row);
            }
        }
    }
}
