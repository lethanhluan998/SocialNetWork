using System;
using System.Collections.Generic;
using System.Text;
using WebVuiVN.Data.Enums;

namespace WebVuiVN.Data.Interface
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}
