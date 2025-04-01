﻿using AutoMapper;
using FluentValidation;
using TestTemplate16.Application.Foos.Commands;

namespace TestTemplate16.Api.Models;

public record CreateFooRequest(string Text);

public class CreateFooRequestValidator : AbstractValidator<CreateFooRequest>
{
    public CreateFooRequestValidator()
    {
        RuleFor(x => x.Text).MinimumLength(5);
    }
}

public class CreateFooRequestProfile : Profile
{
    public CreateFooRequestProfile()
    {
        CreateMap<CreateFooRequest, CreateFooCommand>();
    }
}
