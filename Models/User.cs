using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public string? Email { get; set; }

    public int? Score { get; set; }


}