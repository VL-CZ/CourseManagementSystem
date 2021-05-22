import {TestQuestionVM} from './testQuestionVM';

/**
 * base viewmodel for course test
 */
abstract class BaseCourseTestVM {
  /**
   * topic of the test
   */
  public topic: string;

  /**
   * weight of the test (the greater, the more important)
   */
  public weight: number;

  /**
   * deadline of the test
   */
  public deadline: string;

  /**
   * questions contained in this test
   */
  public questions: TestQuestionVM[] = [];
}

/**
 * viewmodel for adding a course test
 */
export class AddCourseTestVM extends BaseCourseTestVM {

  /**
   * get class instance from `CourseTestDetailsVM`
   * @param courseTest test
   * @constructor
   */
  public static getFrom(courseTest: CourseTestDetailsVM): AddCourseTestVM {
    const testToAdd = new AddCourseTestVM();
    testToAdd.weight = courseTest.weight;
    testToAdd.deadline = courseTest.deadline;
    testToAdd.questions = courseTest.questions;
    testToAdd.topic = courseTest.topic;

    return testToAdd;
  }
}

/**
 * viewmodel representing details of a test
 */
export class CourseTestDetailsVM extends BaseCourseTestVM {
  /**
   * id of the test
   */
  public id: string;

  /**
   * status of the test (is it already published?)
   */
  public status: TestStatus;
}

/**
 * enum representing status of the test
 */
export enum TestStatus {
  /**
   * not published yet
   */
  New,
  /**
   * published
   */
  Published
}
