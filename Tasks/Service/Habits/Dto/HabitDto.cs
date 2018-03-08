using Tasks.Models.DomainModels;

namespace Tasks.Service.Habits.Dto
{
    public class HabitDto
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public TimeFrame TimeFrame { get; set; }
    }
}