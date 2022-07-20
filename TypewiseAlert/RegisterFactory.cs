using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
  public static class RegisterFactory
    {
        // This should be handled by Dependency container -> Future Scope
        public static IFactory<ICooling, CoolingType> RegisterCoolingTypeFactory()
        {
            IFactory<ICooling, CoolingType> coolingFactory = new Factory<ICooling, CoolingType>();

            coolingFactory.RegisterType(CoolingType.PASSIVE_COOLING, () => new PassiveCooling());
            coolingFactory.RegisterType(CoolingType.HI_ACTIVE_COOLING, () => new HighActiveCooling());
            coolingFactory.RegisterType(CoolingType.MED_ACTIVE_COOLING, () => new MediumActiveCooling());

            return coolingFactory;
        }

        public static ICooling GetCoolingClassTypeByCoolingType(CoolingType coolingType)
        {
            var coolingTypeFactory = RegisterCoolingTypeFactory();
            return coolingTypeFactory[coolingType];
        }

        public static IFactory<IAlertTarget, AlertTarget> RegisterAlertTargetFactory()
        {
            IFactory<IAlertTarget, AlertTarget> alertFactory = new Factory<IAlertTarget, AlertTarget>();

            alertFactory.RegisterType(AlertTarget.TO_CONTROLLER, () => new AlertByController());
            alertFactory.RegisterType(AlertTarget.TO_EMAIL, () => new AlertByMail());

            return alertFactory;
        }

        public static IAlertTarget GetAlertTargetClassByAlertTarget(AlertTarget alertTarget)
        {
            // have a common method to create a instance
            var alertFactory = RegisterAlertTargetFactory();

            return alertFactory[alertTarget];
        }
    }
}
