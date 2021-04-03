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
   * questions contained in this test
   */
  public questions: TestQuestionVM[] = [];
}
