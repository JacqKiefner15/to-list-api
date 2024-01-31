namespace ToDoWebApi.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string? Name { get; set;}
        public string? Description { get; set;}
        public string? Status { get; set;}
        public bool IsDeleted {  get; set; }

        public ToDoItem()
        {
            IsDeleted = false;
        }


    }
}
