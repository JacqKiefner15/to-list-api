using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Models;

namespace ToDoWebApi
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDoItem> ToDos {  get; set; }
    }
}
