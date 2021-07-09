/**
 * viewmodel representing a question in a test
 */
export class TestQuestionVM {
  /**
   * id of the question
   */
  public id: string;

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

  /**
   * type of the question
   */
  public type: QuestionType;
}

/**
 * enum representing type of the question
 */
export enum QuestionType {
  /**
   * question with text answer
   */
  TextAnswer,

  /**
   * question with single choice of answers
   */
  SingleChoice,

  /**
   * question that has multiple choices from the answers
   */
  MultipleChoice
}
