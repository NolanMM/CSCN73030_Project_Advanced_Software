﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.Database_Services.Table_Interface
{
    public interface Input_Tables_Template
    {
        bool Test_Connection_To_Table();
        void Insert();
        void Delete();
        void Create();
        Task<List<object>?> ReadAllAsync();
        Task UpdateAsync();
    }
}
