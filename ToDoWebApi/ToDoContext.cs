using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Models;

namespace ToDoWebApi
{
    public class ToDoContext : DbContext
    {

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }
        public DbSet<ToDoItem> ToDos {  get; set; }
    }
}
