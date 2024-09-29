using BarberBoss.Application.UseCases.Invoices;
using BarberBoss.Communication.Enums;
using BarberBoss.Exception.Messages;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validator.Tests.UseCases.Invoices.Register;

public class InvoiceRegisterValidatorTests
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new InvoiceValidator();
        var request = RequestRegisterInvoiceJsonBuilder.Build();

        //Act
        var actual = validator.Validate(request);

        //Assert
        actual.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        //Arrange
        var validator = new InvoiceValidator();
        var request = RequestRegisterInvoiceJsonBuilder.Build();
        request.Title = title;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.REQUIRED_TITLE));
    }

    [Fact]
    public void Error_Date_future()
    {
        //Arrange
        var validator = new InvoiceValidator();
        var request = RequestRegisterInvoiceJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVOICE_DATE_NEEDS_OLDER_THAN_CURRENT));
    }

    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        //Arrange
        var validator = new InvoiceValidator();
        var request = RequestRegisterInvoiceJsonBuilder.Build();
        request.PaymentType = (EPaymentType)700;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PAYMENT_TYPE));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-7)]
    public void Error_Amount_Invalid(decimal amount)
    {
        //Arrange
        var validator = new InvoiceValidator();
        var request = RequestRegisterInvoiceJsonBuilder.Build();
        request.Amount = amount;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVOICE_AMOUNT_GREATER_THAN_ZERO));
    }
}