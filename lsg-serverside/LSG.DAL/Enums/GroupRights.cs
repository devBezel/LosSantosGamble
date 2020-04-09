using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Enums
{
    [Flags]
    public enum GroupRights
    {
        None = 0,
        Vehicle = 1 << 0,
        Offers = 1 << 1,
        Recruitment = 1 << 2,
        Orders = 1 << 3,
        Doors = 1 << 4,
        DepositWithdrawMoney = 1 << 5,
        First = 1 << 6,
        Second = 1 << 7,
        Third = 1 << 8,
        Fourth = 1 << 9,
        Fifth = 1 << 10,
        Sixth = 1 << 11,
        Seventh = 1 << 12,
        Eight = 1 << 13,
        Ninth = 1 << 14,
        AllBasic = DepositWithdrawMoney | Doors | Recruitment | Offers | Orders,
    }
}
