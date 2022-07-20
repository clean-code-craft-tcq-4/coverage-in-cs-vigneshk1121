using System;

namespace TypewiseAlert
{
    public class TypewiseAlert
    {
        public bool CheckAndAlert(AlertTarget alertTarget, BatteryCharacter batteryChar, double temperatureInCelcius)
        {
            var permissibleRange = GetTemperatureLimitsByCoolingType(batteryChar.CoolingType);
            var breachType = GetBreachType(temperatureInCelcius, permissibleRange);

            return AlertTargetSystem(alertTarget, breachType);
        }

        public PermissibleRange GetTemperatureLimitsByCoolingType(CoolingType coolingType)
        {
            return RegisterFactory.GetCoolingClassTypeByCoolingType(coolingType).GetTemperatureLimits();
        }

        public BreachType GetBreachType(double temperatureInCelcius, PermissibleRange permissibleRange)
        {
            if (temperatureInCelcius < permissibleRange.LowerLimit)
            {
                return BreachType.TOO_LOW;
            }
            if (temperatureInCelcius > permissibleRange.UpperLimit)
            {
                return BreachType.TOO_HIGH;
            }
            return BreachType.NORMAL;
        }

        public bool AlertTargetSystem(AlertTarget alertTarget, BreachType breachType)
        {
            return RegisterFactory.GetAlertTargetClassByAlertTarget(alertTarget).SendAlert(breachType);
        }        
    }
}
