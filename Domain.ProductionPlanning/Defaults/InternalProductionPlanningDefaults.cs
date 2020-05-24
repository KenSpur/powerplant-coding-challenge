
namespace Domain.ProductionPlanning.Defaults
{
    internal static class InternalProductionPlanningDefaults
    {
        internal static string InvalidType => "{invalid Type}";

        internal static string CantMeetLoadMessage(decimal remainder)
            => $"Can't meet load with provided power sources, Load requirement has a remainder of {remainder}.";

        internal static string CurrentAlgorithmIsNotAccurateEnoughMessage(decimal remainder)
            => $"Current algorithm isn't accurate enough or load can't be met with provided power sources, Load requirement has a remainder of {remainder}.";
    }
}