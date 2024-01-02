
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public partial class Message
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Text { get; set; } = null!;

    public bool IsRemoved { get; set; } = false;

    public bool IsRead { get; set; } = false;
    public bool Starred { get; set; } = false;
}
