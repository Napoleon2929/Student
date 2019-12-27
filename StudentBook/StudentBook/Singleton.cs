using DatabaseLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentBook
{
    static class Singleton
    {
        static public Parametrs Parametrs { get; set; } = Parametrs.GetParametrs();
        static public Quiz Quiz { get; set; } = new Quiz();
        static public bool IsUpdate { get; set; } = false;
        static public StudentDBEntity StudentDB { get; set; }// = new StudentDBEntity("task.db");
       /* static Singleton()
        {
            IsUpdate = false;
            StudentDB = new StudentDBEntity("task.db");
            Parametrs = Parametrs.GetParametrs();
            Quiz = new Quiz();
        }*/
    }
}
