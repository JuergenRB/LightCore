﻿namespace PeterBucher.AutoFunc.TestTypes
{
    public class NullLogger : ILogger
    {
        public void Log(string message)
        {
            // do nothing.
        }
    }
}