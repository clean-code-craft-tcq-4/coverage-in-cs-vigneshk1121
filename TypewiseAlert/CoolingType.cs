using System.Linq;
using System.Text;

namespace TypewiseAlert
{
    public class PassiveCooling : ICooling
    {
        public PermissibleRange GetTemperatureLimits()
        {
            return new PermissibleRange { LowerLimit = 0, UpperLimit = 35 };
        }
    }

    public class HighActiveCooling : ICooling
    {
        public PermissibleRange GetTemperatureLimits()
        {
            return new PermissibleRange { LowerLimit = 0, UpperLimit = 45 };
        }
    }


    public class MediumActiveCooling : ICooling
    {
        public PermissibleRange GetTemperatureLimits()
        {
            return new PermissibleRange { LowerLimit = 0, UpperLimit = 40 };
        }
    }
}
