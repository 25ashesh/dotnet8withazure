using Xunit;
using FrontLineCleaners.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace FrontLineCleaners.Application.Tests.Commands;

public class CreateCleanerCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        //arrange
        var command = new CreateCleanerCommand()
        {
            Name = "Test",
            Category = "Home",
            ContactEmail = "absolutehomecleaner@info.co.uk",
            PostalCode = "123-890",
            Description = "Absolutely the best cleaner service!"
        };

        var validator = new CreateCleanerCommandValidator();

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
    {
        //arrange
        var command = new CreateCleanerCommand()
        {
            Name = "Te",
            Category = "Hom",
            ContactEmail = "absolutehomecleanerinfo.co.uk",
            PostalCode = "123890"
        };

        var validator = new CreateCleanerCommandValidator();

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldHaveValidationErrorFor(x=>x.Name);
        result.ShouldHaveValidationErrorFor(x => x.Category);
        result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Theory()]
    [InlineData("Complete")]
    [InlineData("Home")]
    [InlineData("Business")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        //arrange
        var validator = new CreateCleanerCommandValidator();
        var command = new CreateCleanerCommand() { Category = category};

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldNotHaveValidationErrorFor(x=>x.Category);
    }

    [Theory()]
    [InlineData("10220")]
    [InlineData("102-20")]
    [InlineData("10 220")]
    [InlineData("10-2 20")]
    [InlineData("100-22-0")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        //arrange
        var validator = new CreateCleanerCommandValidator();
        var command = new CreateCleanerCommand() { PostalCode = postalCode };

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldHaveValidationErrorFor(x => x.PostalCode);
    }
}
