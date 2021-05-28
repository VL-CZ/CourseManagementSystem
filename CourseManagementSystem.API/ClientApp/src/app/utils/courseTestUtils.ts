/**
 * class with additional methods for CourseTest related entities
 */
export class CourseTestUtils {
  /**
   * get string 'GRADED' or 'NOT GRADED' based on {@link BaseCourseTestVM.isGraded} property
   * @param isGraded is the test graded or not?
   */
  public isGradedToString(isGraded: boolean): string {
    return isGraded ? 'GRADED' : 'NOT GRADED';
  }
}
