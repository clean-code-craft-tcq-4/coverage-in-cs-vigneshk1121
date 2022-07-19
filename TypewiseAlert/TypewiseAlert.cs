using System;

namespace TypewiseAlert
{
    public class TypewiseAlert
    {
        public bool CheckAndAlert(AlertTarget alertTarget, BatteryCharacter batteryChar, double temperatureInC)
        {

            var permissibleRange = GetTemperatureLimitsByCoolingType(batteryChar.CoolingType);
            var breachType = GetBreachType(temperatureInC, permissibleRange);

            return AlertTargetSystem(alertTarget, breachType);
        }

        public PermissibleRange GetTemperatureLimitsByCoolingType(CoolingType coolingType)
        {
            var type = GetCoolingClassTypeByCoolingType(coolingType);

            return type.GetTemperatureLimits();
        }

        public IFactory<ICooling, CoolingType> RegisterCoolingTypeFactory()
        {
            IFactory<ICooling, CoolingType > coolingFactory = new Factory<ICooling, CoolingType>();

            coolingFactory.RegisterType(CoolingType.PASSIVE_COOLING, ()=> new PassiveCooling());
            coolingFactory.RegisterType(CoolingType.HI_ACTIVE_COOLING, () => new HighActiveCooling());
            coolingFactory.RegisterType(CoolingType.MED_ACTIVE_COOLING, () => new MediumActiveCooling());

            return coolingFactory;
        }

        public ICooling GetCoolingClassTypeByCoolingType(CoolingType coolingType)
        {
            var coolingTypeFactory = RegisterCoolingTypeFactory();
            return coolingTypeFactory[coolingType];
        }

        public IFactory<IAlertTarget, AlertTarget> RegisterAlertTargetFactory()
        {
            IFactory<IAlertTarget, AlertTarget> alertFactory = new Factory<IAlertTarget, AlertTarget>();

            alertFactory.RegisterType(AlertTarget.TO_CONTROLLER, () => new AlertByController());
            alertFactory.RegisterType(AlertTarget.TO_EMAIL, () => new AlertByMail());

            return alertFactory;
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

        public IAlertTarget GetAlertTargetClassByAlertTarget(AlertTarget alertTarget)
        {
            var alertFactory = RegisterAlertTargetFactory();

            return alertFactory[alertTarget];
        }

        public bool AlertTargetSystem(AlertTarget alertTarget, BreachType breachType)
        {
            var type = GetAlertTargetClassByAlertTarget(alertTarget);

            return type.SendAlert(breachType);
        }
    }
}
