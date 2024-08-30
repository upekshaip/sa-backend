using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class User
{
    public int Id { get; set; }
    
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;
   
    [StringLength(50)] 
    public string LastName { get; set; } = string.Empty;

    [StringLength(100)] 
    public string Email { get; set; } = string.Empty;

    [StringLength(100)] 
    public string Username { get; set; } = string.Empty;
    
    [StringLength(10)] 
    public string Gender { get; set; } = string.Empty;
    
    public int Mobile { get; set; }
    public string Password { get; set; } = string.Empty;
    
    [StringLength(255)] 
    public string Token { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}
