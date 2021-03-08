namespace UsingConfigInjection
{
    internal class TimerSettings
    {
        public const string SectionName = "TimerSettings";

        public int TimerSeconds { get; set; }

        public string Nisse { get; set; }
    }
}