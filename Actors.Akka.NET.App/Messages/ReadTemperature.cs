namespace Actors.AkkaNET.App.Messages
{
    public sealed class ReadTemperature
    {
        public static ReadTemperature Instance { get; } = new ReadTemperature();
        private ReadTemperature() { }
    }
}