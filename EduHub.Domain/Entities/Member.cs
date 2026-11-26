namespace EduHub.Domain.Entities;

/// <summary>
/// A club member. State can only be changed through the methods of this class,
/// so an invalid Member (for example an email without '@') can never exist.
/// </summary>
public class Member
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }

    private Member(Guid id, string email)
    {
        Id = id;
        Email = email;
    }

    public static Member Create(string email)
    {
        ValidateEmail(email);
        return new Member(Guid.NewGuid(), email.Trim());
    }

    public void ChangeEmail(string email)
    {
        ValidateEmail(email);
        Email = email.Trim();
    }

    private static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email must not be empty.", nameof(email));

        if (!email.Contains('@'))
            throw new ArgumentException("Email must contain the '@' character.", nameof(email));
    }
}
