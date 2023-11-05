namespace Todo.SqlRepository.Entities;

public class Todo
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool Completed { get; set; }
}