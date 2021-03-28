import {TestQuestionVM} from './testQuestionVM';

/**
 * viewmodel representing a test
 */
export class CourseTestVM {
  /**
   * id of the test
   */
  id: number;

  /**
   * topic of the test
   */
  topic: string;

  /**
   * weight of the test (the greater, the more important)
   */
  weight: number;

  /**
   * questions contained in this test
   */
  questions: TestQuestionVM[];
}
