import {SubmissionAnswerVM} from './testSubmissionAnswerVM';

/**
 * viewmodel for submitting a test
 */
export class SubmitTestVM {
  /**
   * id of the test
   */
  public testId: string;

  /**
   * topic of the test
   */
  public testTopic: string;

  /**
   * answers to the test
   */
  public answers: SubmissionAnswerVM[] = [];
}
