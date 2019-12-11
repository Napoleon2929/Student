using DatabaseLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentBook
{
    static class Singleton
    {
        static public StudentDBEntity studentDB;
        static public Parametrs parametrs;
        static Singleton()
        {
            studentDB = new StudentDBEntity("task.db");
            parametrs = new Parametrs();
        }
    }
}
