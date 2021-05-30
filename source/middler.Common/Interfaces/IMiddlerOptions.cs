﻿using System;
using System.Collections.Generic;
using doob.middler.Common.SharedModels.Enums;

namespace doob.middler.Common.Interfaces
{
    public interface IMiddlerOptions
    {
        AccessMode DefaultAccessMode { get; set; }
        List<string> DefaultHttpMethods { get; set; }
        List<string> DefaultScheme { get; set; }
        int AutoStreamDefaultMemoryThreshold { get; set; }
        Dictionary<string, Type> RegisteredActionTypes { get; }
    }
}
