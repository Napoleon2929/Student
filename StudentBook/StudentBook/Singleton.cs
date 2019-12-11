using DatabaseLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentBook
{
    static class Singleton
    {
        static public Parametrs parametrs;
        static public Quiz quiz;
        static Singleton()
        {
            parametrs = new Parametrs();
            quiz = new Quiz();
        }
    }
}
