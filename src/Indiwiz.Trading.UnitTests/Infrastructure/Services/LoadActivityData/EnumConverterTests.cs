using CsvHelper;
using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Infrastructure.Services.LoadActivityData;
using Moq;

namespace Indiwiz.Trading.UnitTests.Infrastructure.Services.LoadActivityData;
public class EnumConverterTests
{
    public enum Example
    {
        Buy,
        Sell,
        MonthlyStatement,
        InterestFromCash
    }

    [Theory]
    [InlineData("", null)]
    [InlineData(null, null)]
    [InlineData("BUY", Example.Buy)]
    [InlineData("buy", Example.Buy)]
    [InlineData("Buy", Example.Buy)]
    [InlineData("sElL", Example.Sell)]
    public void ConvertFromString_ReturnsValueOrNull(string? value, Example? expected)
    {
        var mockReaderRow = new Mock<IReaderRow>();
        var sut = new EnumConverter<Example>();

        var actual = (Example?)sut.ConvertFromString(value, mockReaderRow.Object, new CsvHelper.Configuration.MemberMapData(this.GetType().GetMembers()[0]));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("TOP_UP", ActivityType.Topup)]
    public void ConvertFromString_UsesTextConverter(string? value, ActivityType? expected)
    {
        var mockReaderRow = new Mock<IReaderRow>();
        var sut = new EnumConverter<ActivityType>(ActivityDataModelMapper.ReplaceUnderscores);

        var actual = (ActivityType?)sut.ConvertFromString(value, mockReaderRow.Object, new CsvHelper.Configuration.MemberMapData(this.GetType().GetMembers()[0]));

        Assert.Equal(expected, actual);
    }
}
