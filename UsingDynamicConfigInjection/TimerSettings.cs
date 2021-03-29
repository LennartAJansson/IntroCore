namespace UsingDynamicConfigInjection
{
    internal class TimerSettings
    {
        public const string SectionName = "TimerSettings";

        public int TimerSeconds { get; set; }

        public string Message { get; set; }
    }
}