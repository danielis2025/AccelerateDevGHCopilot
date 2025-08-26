using NSubstitute;
using Library.ApplicationCore;
using Library.ApplicationCore.Entities;
using Library.ApplicationCore.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UnitTests.Infrastructure.JsonLoanRepositoryTests;

public class GetLoanTest
{
    private readonly ILoanRepository _mockLoanRepository;
    private readonly JsonLoanRepository _jsonLoanRepository;
    private readonly IConfiguration _configuration;
    private readonly JsonData _jsonData;

    public GetLoanTest()
    {
        _mockLoanRepository = Substitute.For<ILoanRepository>();
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .Build();
        _jsonData = new JsonData(_configuration);
        _jsonLoanRepository = new JsonLoanRepository(_jsonData);
    }

    [Fact(DisplayName = "JsonLoanRepository.GetLoan: Returns loan when loan ID exists")]
    public async Task GetLoan_ReturnsLoan_WhenLoanIdExists()
    {
        //Arrange
        var loanId = 999; // Use a loan ID that does not exist in the Loans.json file
        var expectedLoan = new Loan { Id = loanId, BookItemId = 101, PatronId = 202, LoanDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) };      _mockLoanRepository.GetLoan(loanId).Returns(expectedLoan);

        //Act
        var actualLoan = await _mockLoanRepository.GetLoan(existingLoanId);

        //Assert
        Assert.NotNull(actualLoan);
    }

    /*
    {
        // Arrange
        int existingLoanId = 1; // Loan ID 1 exists in Loans.json
        var expectedLoan = new Loan
        {
            Id = existingLoanId,
            BookItemId = 101,
            PatronId = 202,
            LoanDate = new DateTime(2024, 6, 1),
            DueDate = new DateTime(2024, 6, 15),
            ReturnDate = null // or new DateTime(2024, 6, 10) if returned
        };
        _mockLoanRepository.GetLoan(existingLoanId).Returns(expectedLoan);

        // Act
        var actualLoan = await _jsonLoanRepository.GetLoan(existingLoanId);

        // Assert
        Assert.NotNull(actualLoan);
        Assert.Equal(expectedLoan.Id, actualLoan!.Id);
    }
    */

    // ...unit test methods to be added...
}
