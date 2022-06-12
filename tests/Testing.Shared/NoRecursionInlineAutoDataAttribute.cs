using AutoFixture.Xunit2;

namespace Testing.Shared;

public class NoRecursionInlineAutoDataAttribute : InlineAutoDataAttribute
{
    public NoRecursionInlineAutoDataAttribute(params object[]? values) :
        base(new NoRecursionAutoDataAttribute(), values)
    {
    }
}
