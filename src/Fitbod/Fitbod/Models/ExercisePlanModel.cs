using MessagePack;

namespace Fitbod.Models
{
    public class ExercisePlanModel
    {
        [Key]
        public int Id { get; set; } = 0;
        public int MyProperty { get; set; }
    }
}
