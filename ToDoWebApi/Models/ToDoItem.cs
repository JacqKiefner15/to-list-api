namespace ToDoWebApi.Models
{

    public enum ToDoStatus
    {
        NotStarted,
        InProgress,
        Blocked,
        Complete
    }

    public class ToDoItem
    {
        public int Id { get; set; }
        public string? Name { get; set;}
        public string? Description { get; set;}

        public ToDoStatus Status { get; set; }
        public bool IsDeleted {  get; set; }

        public ToDoItem()
        {
            IsDeleted = false;
            Status = ToDoStatus.NotStarted;
        }


    }
}
