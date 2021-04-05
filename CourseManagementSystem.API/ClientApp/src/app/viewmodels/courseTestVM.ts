import {TestQuestionVM} from './testQuestionVM';

/**
 * viewmodel representing a test
 */
export class CourseTestVM {
  /**
   * id of the test
   */
  public id: number;

  /**
   * topic of the test
   */
  public topic: string;

  /**
   * weight of the test (the greater, the more important)
   */
  public weight: number;

  /**
   * status of the test (is it already published?)
   */
  public status: TestStatus;

  /**
   * deadline of the test
   */
  public deadline: Date;

  /**
   * questions contained in this test
   */
  public questions: TestQuestionVM[] = [];
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
