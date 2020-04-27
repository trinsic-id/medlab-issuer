using MedLab_Demo1.Data;
using System;

namespace MedLab_Demo1.Data
{
    public class CredentialModel
    {
        public string virus { get; set;} = "Covid-19";
        public string checktime { get; set;} = DateTime.Today.ToString();
        public string checkedlocation { get; set;} = "San Francisco, California";
        public string checkedby { get; set;} = "Dr. John Johnson";
        public string checkedfacility { get; set;} = "Blue Lake Hospital";
        public string diagnosismethod { get; set;} = "Blood Test";
        public string issued { get; set;} = DateTime.Today.ToString();
        public string expiry { get; set;} = "12/31/2020 12:00:00 AM";
    }
}