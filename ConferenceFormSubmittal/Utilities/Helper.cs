using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.Utilities
{
    static public class Helper
    {
        public static string StatusIcon(string status)
        {
            string icon = "<i class='fa fa-";

            switch (status)
            {
                case "Submitted":
                    icon += "paper-plane";
                    break;
                case "Draft":
                    icon += "paperclip";
                    break;
                case "Approved":
                    icon += "thumbs-up";
                    break;
                case "Denied":
                    icon += "thunbs-up";
                    break;
                default:
                    icon += "question";
                    break;
            }

            return icon + " fa-2x'></i>";
        }

        public static string YesNoFromBool(bool b)
        {
            return b ? "Yes" : "No";
        }
    }
}