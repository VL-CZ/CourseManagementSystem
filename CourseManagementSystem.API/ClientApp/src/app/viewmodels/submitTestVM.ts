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
   * answers to the test
   */
  public answers: SubmissionAnswerVM[] = [];
}
