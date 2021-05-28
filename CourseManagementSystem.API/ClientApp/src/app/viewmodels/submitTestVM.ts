import {SubmissionAnswerVM} from './testSubmissionAnswerVM';

/**
 * viewmodel for submitting a test
 */
export class SubmitTestVM {
  /**
   * id of the test submission
   */
  public testSubmissionId: string;

  /**
   * topic of the test
   */
  public testTopic: string;

  /**
   * is this test graded?
   */
  public isTestGraded: boolean;

  /**
   * check if the test has already been submitted
   */
  public isSubmitted: boolean;

  /**
   * answers to the test
   */
  public answers: SubmissionAnswerVM[] = [];
}
