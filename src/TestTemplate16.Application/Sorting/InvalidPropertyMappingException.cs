using System;

namespace TestTemplate16.Application.Sorting;

public class InvalidPropertyMappingException : Exception
{
    public InvalidPropertyMappingException(string message)
        : base(message)
    {
    }
}
