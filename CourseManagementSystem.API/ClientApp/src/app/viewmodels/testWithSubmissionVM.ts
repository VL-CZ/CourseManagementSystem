import {SubmissionAnswerVM} from './testSubmissionVM';

/**
 * class representing a test with a submission
 */
export class TestWithSubmissionVM {
  /**
   * identifier of the test
   */
  public testId: number;

  /**
   * topic of the test
   */
  public testTopic: string;

  /**
   * identifier of the test submission
   */
  public submissionId: number;

  /**
   * answers in the submission
   */
  public answers: SubmissionAnswerWithCorrectAnswerVM[];

  constructor(testId: number, testTopic: string, submissionId: number, answers: SubmissionAnswerWithCorrectAnswerVM[] = []) {
    this.testId = testId;
    this.testTopic = testTopic;
    this.answers = answers;
    this.submissionId = submissionId;
  }

  /**
   * get default instance of this class
   */
  public static getDefault(): TestWithSubmissionVM {
    return new TestWithSubmissionVM(0, '', 0);
  }
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

  constructor(questionNumber: number, questionText: string, answerText: string, correctAnswer: string,
              receivedPoints: number, maximalPoints: number, comment: string) {
    super(questionNumber, questionText, answerText);
    this.correctAnswer = correctAnswer;
    this.receivedPoints = receivedPoints;
    this.maximalPoints = maximalPoints;
    this.comment = comment;
  }
}
