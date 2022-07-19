using System;

namespace TypewiseAlert
{
    public interface IAlertTarget
    {
        bool SendAlert(BreachType breachType);
    }

    public class AlertByController : IAlertTarget
    {
        public bool SendAlert(BreachType breachType)
        {
            Console.WriteLine("{} : {}\n" + breachType);
            return true;
        }
    }

    public class AlertByMail : IAlertTarget
    {
        public bool SendAlert(BreachType breachType)
        {
            string recepient = "a.b@c.com";
            switch (breachType)
            {
                case BreachType.TOO_LOW:
                case BreachType.TOO_HIGH:
                    Console.WriteLine("To: {}\n" + recepient);
                    Console.WriteLine("Hi, the temperature is\n" + breachType);
                    return true;
                default:
                    return false;
            }
        }
    }
}
