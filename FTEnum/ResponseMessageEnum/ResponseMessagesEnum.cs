using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEnum.ResponseMessageEnum
{
    public enum ResponseMessagesEnum
    {
        [Description("Record(s) saved successfully.")]
        Saved = 2,
        [Description("Record(s) deleted successfully.")]
        Deleted = 3,
        [Description("Unable to process your request, please contact admin.")]
        Error = 4,
        [Description("Record already exists.")]
        Exists = 5,
        [Description("Record(s) deleted successfully.")]
        Remove = 6,
        [Description("Record(s) updated successfully.")]
        Update = 7,
        [Description("Record(s) Retrived successfully.")]
        GetRecord = 8
    }
  
}
