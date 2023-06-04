using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.ApiClient.Contracts;
public record TaskUpdateRequest
{
    public required Guid TaskId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required bool Completed { get; init; }
    public required DateTime? DateCompleted { get; init; }
}