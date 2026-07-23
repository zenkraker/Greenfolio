using Ardalis.GuardClauses;
using FluentAssertions;
using Greenfolio.API.Core.GreenGraph;
using Xunit;

namespace Greenfolio.API.UnitTests;

public class OrganizationTests
{
  [Fact]
  public void Constructor_sets_required_fields_and_defaults_to_auto_status()
  {
    var org = new Organization("Technical University of Munich", "DE", OrgType.University);

    org.Name.Should().Be("Technical University of Munich");
    org.Country.Should().Be("DE");
    org.OrgType.Should().Be(OrgType.University);
    org.Status.Should().Be(OrgStatus.Auto);
    org.NameVariants.Should().BeEmpty();
  }

  [Fact]
  public void Constructor_rejects_empty_name()
  {
    var act = () => new Organization("", "DE", OrgType.University);
    act.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void AddNameVariant_appends_without_duplicating()
  {
    var org = new Organization("TUM", "DE", OrgType.University);

    org.AddNameVariant("Technische Universität München");
    org.AddNameVariant("Technische Universität München");

    org.NameVariants.Should().ContainSingle().Which.Should().Be("Technische Universität München");
  }

  [Theory]
  [InlineData(nameof(Organization.MarkClaimed), OrgStatus.Claimed)]
  [InlineData(nameof(Organization.Suppress), OrgStatus.Suppressed)]
  [InlineData(nameof(Organization.Remove), OrgStatus.Removed)]
  public void Status_transition_methods_set_the_expected_status(string methodName, OrgStatus expected)
  {
    var org = new Organization("TUM", "DE", OrgType.University);

    typeof(Organization).GetMethod(methodName)!.Invoke(org, null);

    org.Status.Should().Be(expected);
  }

  [Fact]
  public void LinkRor_rejects_empty_id()
  {
    var org = new Organization("TUM", "DE", OrgType.University);
    var act = () => org.LinkRor("");
    act.Should().Throw<ArgumentException>();
  }
}
