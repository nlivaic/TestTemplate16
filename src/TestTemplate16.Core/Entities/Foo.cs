using System;
using TestTemplate16.Common.Base;
using TestTemplate16.Common.Exceptions;

namespace TestTemplate16.Core.Entities;

public class Foo : BaseVersionedEntity<Guid>
{
    public Foo(string text)
    {
        Validate(text);
        Text = text;
    }

    private Foo()
    {
    }

    public string Text { get; set; }

    private static void Validate(string text)
    {
        if (text.Length < 5)
        {
            throw new BusinessException($"Foo text must be at least 5 characters.");
        }
    }
}
