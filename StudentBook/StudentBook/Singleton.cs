using DatabaseLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentBook
{
    static class Singleton
    {
        static public Parametrs Parametrs;
        static public Quiz Quiz;
        static Singleton()
        {
            Parametrs = Parametrs.GetParametrs();
            Quiz = new Quiz();
        }
    }
}
