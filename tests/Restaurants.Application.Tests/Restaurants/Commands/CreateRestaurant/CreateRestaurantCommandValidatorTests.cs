using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        CreateRestaurantCommand command = new()
        {
            Name = "Test",
            Category = "Italian",
            ContactEmail = "test@test.com",
            PostalCode = "12-345",
        };

        CreateRestaurantCommandValidator? validator = new();

        // act
        TestValidationResult<CreateRestaurantCommand>? result = validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        CreateRestaurantCommand command = new()
        {
            Name = "Te",
            Category = "Ita",
            ContactEmail = "@test.com",
            PostalCode = "12345",
        };

        CreateRestaurantCommandValidator? validator = new();

        // act
        TestValidationResult<CreateRestaurantCommand>? result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }


    [Theory()]
    [InlineData("Italian")]
    [InlineData("Mexican")]
    [InlineData("Japanese")]
    [InlineData("American")]
    [InlineData("Indian")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        CreateRestaurantCommandValidator? validator = new();

        CreateRestaurantCommand? command = new() { Category = category };

        // act
        TestValidationResult<CreateRestaurantCommand>? result = validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }

    [Theory()]
    [InlineData("10220")]
    [InlineData("102-20")]
    [InlineData("10 220")]
    [InlineData("10-2 20")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        CreateRestaurantCommandValidator? validator = new();

        CreateRestaurantCommand? command = new() { PostalCode = postalCode };

        // act
        TestValidationResult<CreateRestaurantCommand>? result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }

}