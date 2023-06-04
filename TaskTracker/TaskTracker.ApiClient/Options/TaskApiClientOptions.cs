using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.ApiClient.Options;
public class TaskApiClientOptions
{
    [Required]
    public string BaseUrl { get; set; } = default!;
}
