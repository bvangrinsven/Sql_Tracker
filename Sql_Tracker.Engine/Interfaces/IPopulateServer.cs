﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Interfaces
{
    public interface IPopulateServer
    {
        bool ShowWizard { get; set; }
        void Execute();
    }
}
