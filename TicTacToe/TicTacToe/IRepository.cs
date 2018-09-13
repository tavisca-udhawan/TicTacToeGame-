using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    interface IRepository
    {
        bool Add(User user);
        string getTokken(int id);
    }
}
