using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BLL.Interfaces
{
    public interface IValidator<in T>
    {
        bool IsValid(T value);
    }
}
