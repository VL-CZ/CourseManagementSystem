using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.API.Services
{
    public interface ICourseMemberService
    {
        /// <summary>
        /// get person with selected person ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CourseMember GetMemberByID(int id);

        /// <summary>
        /// remove person with selected ID
        /// </summary>
        /// <param name="id"></param>
        void RemoveMemberById(int id);
    }
}