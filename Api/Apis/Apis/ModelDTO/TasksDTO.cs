namespace Apis.ModelDTO
{
    public class TasksDTO
    {
        public string Id { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Timeframe { get; set; }

        public string Priority { get; set; } = null!;

        public bool Done { get; set; }
        public static Apis.Model.Task TaskConverter(TasksDTO taskdto)
        {
            Apis.Model.Task task = new Apis.Model.Task();
            task.Id = taskdto.Id;
            task.UserId = taskdto.UserId;
            task.Description = taskdto.Description;
            task.Timeframe = taskdto.Timeframe;
            task.Priority = taskdto.Priority;
            task.Done = taskdto.Done;
            return task;
        }

    }
}
