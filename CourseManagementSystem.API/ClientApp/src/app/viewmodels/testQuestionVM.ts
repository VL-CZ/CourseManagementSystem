/**
 * viewmodel representing a question in a test
 */
export class TestQuestionVM {
  /**
   * id of the question
   */
  id: number;

  /**
   * question number
   */
  number: number;

  /**
   * text to the question
   */
  questionText: string;

  /**
   * correct answer to this question
   */
  correctAnswer: string;

  /**
   * maximal points for this question
   */
  points: number;
}
