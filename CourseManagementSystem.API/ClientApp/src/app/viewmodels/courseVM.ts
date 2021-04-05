/**
 * base viewmodel for a course
 */
abstract class BaseCourseVM {
  /**
   * name of the course
   */
  public name: string;
}

/**
 * viewmodel representing info about a course
 */
export class CourseInfoVM extends BaseCourseVM {
  /**
   * id of the course
   */
  public id: number;
}

/**
 * viewmodel for adding a course
 */
export class AddCourseVM extends BaseCourseVM{

  /**
   * id of the course admin
   */
  public adminId: string;

}
