using LSG.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.WebPages;

namespace LSG.BLL.Validators
{
    public class GroupSlotValidator : IValidator<byte>
    {
        public bool IsValid(byte value)
        {
            return value <= 3 && value >= 1;
        }
    }
}
