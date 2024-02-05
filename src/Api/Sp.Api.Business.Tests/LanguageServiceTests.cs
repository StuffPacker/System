using FluentAssertions;
using Microsoft.Extensions.Options;
using Sp.Api.Business.Feature.Language;

namespace Sp.Api.Business.Tests;

[TestClass]
public class LanguageServiceTests
{
    [Ignore]
    [TestMethod]
    public void ShouldFindLanguage()
    {
        var target = GetTarget();
        var result = target.GetLanguage("Detta är en text på svenska");
        result.Name.Should().BeEquivalentTo("Swedish");
        result.Iso6391Name.Should().BeEquivalentTo("sv");

        var result2 = target.GetLanguage("Ce document est rédigé en Français.");
        result2.Name.Should().BeEquivalentTo("French");
        result2.Iso6391Name.Should().BeEquivalentTo("fr");
    }

    private ILanguageService GetTarget()
    {
        var key = string.Empty;
        var uri = string.Empty;
        var option = new LanguageServiceOptions
        {
            Key = key,
            Uri = uri
        };

        return new LanguageService(Options.Create(option));
    }
}