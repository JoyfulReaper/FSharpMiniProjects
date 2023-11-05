namespace Todo.SqlRepository.Entities;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class TodoContext : DbContext
{
    public string DbPath { get; }
    public DbSet<Todo> Todos { get; set; }

    public TodoContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "todo.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}