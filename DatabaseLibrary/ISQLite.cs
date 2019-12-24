using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary
{
    //интерфейс для получения пути бд
    public interface ISQLite
    {
        string GetDatabasePath(string sqliteDatabaseName);
        bool IsCorrect { get; set; }
    }
}
