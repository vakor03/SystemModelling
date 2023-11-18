namespace SystemModelling.ModelWIthItems.Elements;

public partial class Creator
{
    public class FluentCreateBuilder : FluentElementBuilder<FluentCreateBuilder>
    {
        public static FluentCreateBuilder New() => new();

        public FluentCreateBuilder WithStartedDelay(double startedDelay)
        {
            StartedDelay = startedDelay;
            return this;
        }

        public double StartedDelay { get; set; }

        public override Creator Build()
        {
            Creator create = new Creator
            {
                Name = Name,
                DelayGenerator = DelayGenerator,
                TNext = StartedDelay
            };

            return create;
        }
    }
}