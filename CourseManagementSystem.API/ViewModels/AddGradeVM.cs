namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel for adding new grade
    /// </summary>
    public class AddGradeVM
    {
        /// <summary>
        /// percentual value of the grade (0=0%, 1=100%, may be greater than 1 in case of bonus points)
        /// </summary>
        public double PercentualValue { get; set; }

        /// <summary>
        /// topic that this grade belongs to
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// comment to the grade provided by teacher
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// weight of the grade - impact on total grade (the higher weight, the more impact)
        /// <br/>
        /// e.g. grade with weight 2 has the same weight as two grades weighted 1
        /// </summary>
        public int Weight { get; set; }
    }
}