/**
 * viewmodel representing a question in a test
 */
export class TestQuestionVM {
  /**
   * id of the question
   */
  public id: number;

  /**
   * question number
   */
  public number: number;

  /**
   * text to the question
   */
  public questionText: string;

  /**
   * correct answer to this question
   */
  public correctAnswer: string;

  /**
   * maximal points for this question
   */
  public points: number;
}
