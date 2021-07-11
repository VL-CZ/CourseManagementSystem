/**
 * viewmodel representing an answer within a test submission
 */
import {QuestionType} from './testQuestionVM';

export class SubmissionAnswerVM {
  /**
   * number of question that this answer belongs to
   */
  public questionNumber: number;

  /**
   * text provided to the question
   */
  public questionText: string;

  /**
   * answer to the question
   */
  public answerText: string;

  /**
   * type of the question
   */
  public questionType: QuestionType;
}

/**
 * viewmodel representing an answer in submission including correct answer
 */
export class SubmissionAnswerWithCorrectAnswerVM extends SubmissionAnswerVM {
  /**
   * correct answer to the question
   */
  public correctAnswer: string;

  /**
   * points received for the answer
   */
  public receivedPoints: number;

  /**
   * maximal points for the question
   */
  public maximalPoints: number;

  /**
   * comment to the answer provided by teacher
   */
  public comment: string;
}
