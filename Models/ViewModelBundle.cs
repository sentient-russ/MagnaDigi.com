using System.Collections;

namespace magnadigi.Models
{
    public class ViewModelBundle
    {
        public List<TaskModel> taskList { get; set; }
        public TaskModel taskModel { get; set; }
        public UserModel userModel { get; set; }

        public string? JsonTasks { get; set; }

       
    }
}
