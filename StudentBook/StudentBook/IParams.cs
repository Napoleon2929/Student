using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace StudentBook
{
    public interface IParams
    {
        Parametrs GetParametrs();
        void SetParametrs(Parametrs parametrs);
    }
}