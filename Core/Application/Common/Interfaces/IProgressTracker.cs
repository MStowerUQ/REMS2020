﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static Rems.Application.EventManager;

namespace Rems.Application.Common.Interfaces
{
    public interface IProgressTracker
    {
        event NextItemHandler NextProgress;

        event EventHandler IncrementProgress;

        event EventHandler StopProgress;

        event ExceptionHandler TaskFailed;

        int Items { get; }

        void Run();
    }
}
