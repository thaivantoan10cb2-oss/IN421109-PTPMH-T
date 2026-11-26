using EduHub.Domain.Entities;

namespace EduHub.Tests;

public class MemberTests
{
    [Fact]
    public void Create_WithValidEmail_ReturnsMember()
    {
        var m = Member.Create("abc@example.com");

        Assert.NotEqual(Guid.Empty, m.Id);
        Assert.Equal("abc@example.com", m.Email);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc")]
    public void Create_WithInvalidEmail_ThrowsArgumentException(string email)
    {
        Assert.Throws<ArgumentException>(() => Member.Create(email));
    }

    [Fact]
    public void ChangeEmail_WithInvalidEmail_KeepsOldEmail()
    {
        var m = Member.Create("abc@example.com");

        Assert.Throws<ArgumentException>(() => m.ChangeEmail("abc"));

        Assert.Equal("abc@example.com", m.Email);
    }
}
