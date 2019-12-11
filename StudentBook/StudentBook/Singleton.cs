using DatabaseLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentBook
{
    static class Singleton
    {
        static public Parametrs parametrs;
        static Singleton()
        {
            parametrs = new Parametrs();
        }
    }
}
