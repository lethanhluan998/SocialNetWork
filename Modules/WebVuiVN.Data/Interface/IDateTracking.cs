using System;
using System.Collections.Generic;
using System.Text;
namespace WebVuiVN.Data.Interface
{
    public interface IDateTracking
    {
        DateTime DateCreated { set; get; }
        DateTime DateModified { set; get; }
    }
}