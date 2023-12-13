using MudBlazor.Utilities;
using System.ComponentModel.DataAnnotations;

namespace WorkFlow.Client.Model;

public class Membre
{
    public string Id { get; set; }
    [Required]
    public string Role { get; set; }

    public string Manager { get; set; }
    //public MudColor Couleur { get; set; }
}
