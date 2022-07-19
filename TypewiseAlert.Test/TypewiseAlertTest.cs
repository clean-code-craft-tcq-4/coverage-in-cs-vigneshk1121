using System;
using Xunit;

namespace TypewiseAlert.Test
{
  public class TypewiseAlertTest
  {

        [Theory]
        [InlineData(CoolingType.PASSIVE_COOLING, typeof(PassiveCooling))]
        [InlineData(CoolingType.HI_ACTIVE_COOLING, typeof(HighActiveCooling))]
        [InlineData(CoolingType.MED_ACTIVE_COOLING, typeof(MediumActiveCooling))]
        public void TestIsCoolingFactoryCreated(CoolingType type, Type classType)
        {
            // Arrange
            var factory = new Factory<ICooling, CoolingType>();
            factory.RegisterType(CoolingType.PASSIVE_COOLING, () => new PassiveCooling());
            factory.RegisterType(CoolingType.HI_ACTIVE_COOLING, () => new HighActiveCooling());
            factory.RegisterType(CoolingType.MED_ACTIVE_COOLING, () => new MediumActiveCooling());

            // Act
            var coolingType = factory[type];

            // Assert
            Assert.True(coolingType.GetType() == classType);
        }

        [Theory]
        [InlineData(AlertTarget.TO_CONTROLLER, typeof(AlertByController))]
        [InlineData(AlertTarget.TO_EMAIL, typeof(AlertByMail))]
        public void TestIsAlertTargetFactoryCreated(AlertTarget type, Type classType)
        {
            // Arrange
            var factory = new Factory<IAlertTarget, AlertTarget>();
            factory.RegisterType(AlertTarget.TO_CONTROLLER, () => new AlertByController());
            factory.RegisterType(AlertTarget.TO_EMAIL, () => new AlertByMail());

            // Act
            var alertType = factory[type];

            // Assert
            Assert.True(alertType.GetType() == classType);
        }

        [Fact]
        public void Test_If_Cooling_Factory_Is_Registered()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var value = testAlert.RegisterCoolingTypeFactory();

            // Assert
            Assert.True(value.GetType() == typeof(Factory<ICooling, CoolingType>));
        }

        [Fact]
        public void Test_If_Alert_Test_Factory_Is_Registered()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var value = testAlert.RegisterAlertTargetFactory();

            // Assert
            Assert.True(value.GetType() == typeof(Factory<IAlertTarget, AlertTarget>));
        }

        [Fact]
        public void Test_If_Passive_Cooling_Class_IsInvoked_If_Cooling_Type_Is_Passive()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var type = testAlert.GetCoolingClassTypeByCoolingType(CoolingType.PASSIVE_COOLING);

            // Assert
            Assert.True(type.GetType() == typeof(PassiveCooling));
        }

        [Fact]
        public void Test_If_Passive_Cooling_Class_IsInvoked_If_Cooling_Type_Is_HighActive()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var type = testAlert.GetCoolingClassTypeByCoolingType(CoolingType.HI_ACTIVE_COOLING);

            // Assert
            Assert.True(type.GetType() == typeof(HighActiveCooling));
        }

        [Fact]
        public void Test_If_Passive_Cooling_Class_IsInvoked_If_Cooling_Type_Is_Medium_Active()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var type = testAlert.GetCoolingClassTypeByCoolingType(CoolingType.MED_ACTIVE_COOLING);

            // Assert
            Assert.True(type.GetType() == typeof(MediumActiveCooling));
        }

        [Fact]
        public void Test_Temperature_Limits_For_Passive_Cooling_Type()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var permissibleRange = testAlert.GetTemperatureLimitsByCoolingType(CoolingType.PASSIVE_COOLING);

            // Assert
            Assert.True(permissibleRange.LowerLimit == 0);
            Assert.True(permissibleRange.UpperLimit == 35);
        }

        [Fact]
        public void Test_Temperature_Limits_For_Medium_Active_Cooling_Type()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var permissibleRange = testAlert.GetTemperatureLimitsByCoolingType(CoolingType.MED_ACTIVE_COOLING);

            // Assert
            Assert.True(permissibleRange.LowerLimit == 0);
            Assert.True(permissibleRange.UpperLimit == 40);
        }

        [Fact]
        public void Test_Temperature_Limits_For_High_Active_Cooling_Type()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var permissibleRange = testAlert.GetTemperatureLimitsByCoolingType(CoolingType.HI_ACTIVE_COOLING);

            // Assert
            Assert.True(permissibleRange.LowerLimit == 0);
            Assert.True(permissibleRange.UpperLimit == 45);
        }

        [Fact]
        public void Test_if_Breech_Type_IsLow_If_temperature_Is_Below_Lower_Limit()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var permissibleRange = testAlert.GetTemperatureLimitsByCoolingType(CoolingType.MED_ACTIVE_COOLING);
            var breachType = testAlert.GetBreachType(-1, permissibleRange);

            // Assert
            Assert.True(breachType == BreachType.TOO_LOW);
        }

        [Fact]
        public void Test_if_Breech_Type_IsHigh_If_temperature_Is_Above_Upper_Limit()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var permissibleRange = testAlert.GetTemperatureLimitsByCoolingType(CoolingType.MED_ACTIVE_COOLING);
            var breachType = testAlert.GetBreachType(60, permissibleRange);

            // Assert
            Assert.True(breachType == BreachType.TOO_HIGH);
        }

        [Fact]
        public void Test_if_Breech_Type_Is_Normal_If_temperature_Is_Within_Range()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var permissibleRange = testAlert.GetTemperatureLimitsByCoolingType(CoolingType.MED_ACTIVE_COOLING);
            var breachType = testAlert.GetBreachType(38, permissibleRange);

            // Assert
            Assert.True(breachType == BreachType.NORMAL);
        }

        [Fact]
        public void Test_If_Alert_By_Controller_Class_is_Invoked_If_Type__Is_Alert_By_Controller()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var type = testAlert.GetAlertTargetClassByAlertTarget(AlertTarget.TO_CONTROLLER);

            // Assert
            Assert.True(type.GetType() == typeof(AlertByController));
        }

        [Fact]
        public void Test_If_Alert_By_email_Class_is_Invoked_If_Type__Is_Alert_By_Email()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var type = testAlert.GetAlertTargetClassByAlertTarget(AlertTarget.TO_EMAIL);

            // Assert
            Assert.True(type.GetType() == typeof(AlertByMail));
        }

        [Fact]
        public void Test_If_Alert_By_Controller_Is_triggered_If_Alert_Type_Is_Controller()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var isAlertSent = testAlert.AlertTargetSystem(AlertTarget.TO_CONTROLLER, BreachType.NORMAL);

            // Assert
            Assert.True(isAlertSent);
        }

        [Fact]
        public void Test_If_Alert_Is_Triggered_Is_triggered_If_Breach_Type_Is_Normal()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var isAlertSent = testAlert.AlertTargetSystem(AlertTarget.TO_EMAIL, BreachType.NORMAL);

            // Assert
            Assert.False(isAlertSent);
        }

        [Fact]
        public void Test_If_Alert_Is_Triggered_Is_triggered_If_Breach_Type_Is_Low()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var isAlertSent = testAlert.AlertTargetSystem(AlertTarget.TO_EMAIL, BreachType.TOO_LOW);

            // Assert
            Assert.True(isAlertSent);
        }

        [Fact]
        public void Test_If_Alert_Is_Triggered_Is_triggered_If_Breach_Type_Is_High()
        {
            // Arrange
            var testAlert = new TypewiseAlert();

            // Act
            var isAlertSent = testAlert.AlertTargetSystem(AlertTarget.TO_EMAIL, BreachType.TOO_HIGH);

            // Assert
            Assert.True(isAlertSent);
        }

        // Integration Tests
        [Fact]
        public void Test_If_Alert_Is_Triggered_If_Temperature_Is_Above_Range()
        {
            // Arrange
            var testAlert = new TypewiseAlert();
            BatteryCharacter batteryCharacter = new BatteryCharacter { CoolingType = CoolingType.HI_ACTIVE_COOLING };

            // Act
            var result = testAlert.CheckAndAlert(AlertTarget.TO_EMAIL, batteryCharacter, 60);

            // Assert
            Assert.True(result);
        }
    }
}
