using AutoFixture;
using AutoFixture.Xunit2;

namespace Testing.Shared;

public class NoRecursionAutoDataAttribute : AutoDataAttribute
{
    public NoRecursionAutoDataAttribute() : base(
        () =>
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());

            return fixture;
        }
    )
    {
    }
}
