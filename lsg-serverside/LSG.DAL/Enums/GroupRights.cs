using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Enums
{
    [Flags]
    public enum GroupRights
    {
        None = 0,
        DepositWithdrawMoney = 1 << 0,
        Recruitment = 1 << 1,
        Orders = 1 << 2,
        Doors = 1 << 3,
        Chat = 1 << 4,
        Offers = 1 << 5,
        First = 1 << 6,
        Second = 1 << 7,
        Third = 1 << 8,
        Fourth = 1 << 9,
        Fifth = 1 << 10,
        Sixth = 1 << 11,
        Seventh = 1 << 12,
        Eight = 1 << 13,
        Ninth = 1 << 14,
        Panel = 1 << 15,
        AllBasic = DepositWithdrawMoney | Doors | Recruitment | Chat | Offers | Orders | Panel,
    }
}
