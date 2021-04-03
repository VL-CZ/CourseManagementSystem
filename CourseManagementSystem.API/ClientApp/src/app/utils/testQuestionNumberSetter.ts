import {TestQuestionVM} from '../viewmodels/testQuestionVM';

/**
 * class responsible for numbering test questions
 */
export class TestQuestionNumberSetter{
  /**
   * add numbers to questions
   * @param questions
   * @private
   */
  public static setQuestionNumbers(questions: TestQuestionVM[]): void {
    for (let index = 0; index < questions.length; index++) {
      questions[index].number = index + 1;
    }
  }
}
