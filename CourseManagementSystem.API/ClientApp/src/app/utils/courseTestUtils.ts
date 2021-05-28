import {AddCourseTestVM, BaseCourseTestVM} from '../viewmodels/courseTestVM';

/**
 * class with additional methods for {@link BaseCourseTestVM}
 */
export class CourseTestUtils {
  /**
   * get string 'GRADED' or 'NOT GRADED' based on {@link BaseCourseTestVM.isGraded} property
   * @param courseTest test to check
   */
  public isGradedToString(courseTest: BaseCourseTestVM): string {
    return courseTest.isGraded ? 'GRADED' : 'NOT GRADED';
  }
}
