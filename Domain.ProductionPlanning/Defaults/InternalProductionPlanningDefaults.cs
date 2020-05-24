namespace Domain.ProductionPlanning.Defaults
{
    internal static class InternalProductionPlanningDefaults
    {
        internal static string CantMeetLoadMessage => "Can't meet load with provided power sources.";

        internal static string CurrentAlgorithmIsNotAccurateEnoughMessage(decimal remainder)
            => $"Current algorithm isn't accurate enough or load can't be met with provided power sources, Load requirement has a remainder of {remainder}";
    }
}