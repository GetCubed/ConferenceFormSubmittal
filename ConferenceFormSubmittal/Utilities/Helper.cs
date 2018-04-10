using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.Utilities
{
    static public class Helper
    {
        // returns an <i> element.
        // Need to pass a string instead of an enum for the first parameter?
        // No problem! Use the overload defined below.
        public static HtmlString GetIcon(Icons icon, int size = 2, string textColor = "black")
        {
            string element = "<i class='fa fa-";

            switch ((int)icon)
            {
                case 0:
                    element += "plus";
                    break;
                case 1:
                    element += "minus";
                    break;
                case 2:
                    element += "pencil";
                    break;
                case 3:
                    element += "eye";
                    break;
                case 4:
                    element += "flag";
                    break;
                case 5:
                    element += "paper-plane";
                    break;
                case 6:
                    element += "paperclip";
                    break;
                case 7:
                    element += "thumbs-up";
                    break;
                case 8:
                    element += "thumbs-down";
                    break;
                case 9:
                    element += "check";
                    break;
                case 10:
                    element += "ban";
                    break;
                case 11:
                    element += "arrow-left";
                    break;
                case 12:
                    element += "floppy-o"; 
                    break;
                case 13:
                    element += "filter";
                    break;
                case 14:
                    element += "file-text";
                    break;
                case 15:
                    element += "eraser";
                    break;
                case 16:
                    element += "marker";
                    break;
                default:
                    element += "question";
                    break;
            }

            element += " fa-" + size.ToString() + "x text-" + textColor + "'></i>";

            return new HtmlString(element);
        }

        // pass a string instead of an enum
        public static HtmlString GetIcon(string icon, int size = 2, string textColor = "black")
        {
            return GetIcon((Icons)Enum.Parse(typeof(Icons), icon), size, textColor);
        }


        //// given a status, returns a background element
        public static HtmlString GetBackGround(string status)
        {
            string element = "";

            switch (status)
            {
                case "Submitted":
                    element += "";
                    break;
                case "Approved":
                    element += "rgba-green-light";
                    break;
                case "Denied":
                    element += "rgba-black-light";
                    break;
                case "Draft":
                    element += "gba-yellow-light";
                    break;
                default:
                    element += "";
                    break;
            }

           return new HtmlString(element);
        }

        public static string YesNoFromBool(bool b)
        {
            return b ? "Yes" : "No";
        }
    }
}