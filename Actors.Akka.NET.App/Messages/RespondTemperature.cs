using System;
using System.Collections.Generic;
using System.Text;

namespace Actors.AkkaNET.App.Messages
{
    public sealed class RespondTemperature
    {
        public RespondTemperature(double? value)
        {
            Value = value;
        }

        public double? Value { get; }
    }
}
