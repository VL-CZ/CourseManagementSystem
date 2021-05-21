import {SubmissionAnswerWithCorrectAnswerVM} from './testSubmissionAnswerVM';

/**
 * base viewmodel for a test submission
 */
abstract class BaseTestSubmissionVM {
  /**
   * id of the test submission
   */
  public testSubmissionId: string;

  /**
   * datetime when this test was submitted
   */
  public submittedDateTime: string;

  /**
   * has this test submission already been reviewed?
   */
  public isReviewed: boolean;
}

/**
 * viewmodel representing info about a test submission
 */
export class TestSubmissionInfoVM extends BaseTestSubmissionVM {

  /**
   * topic of the test
   */
  public testTopic: string;

  /**
   * weight of the test (e.g. impact on total grade)
   */
  public testWeight: number;

  /**
   * score from the test in percents (0=0%,1=100%)
   */
  public percentualScore: number;
}

/**
 * class representing a test submission with information about an user
 */
export class TestSubmissionWithUserInfoVM extends BaseTestSubmissionVM {
  /**
   * email of the student
   */
  public studentEmail: string;

  /**
   * percentual score from the test (0=0%,1=100%)
   */
  public percentualScore: number;
}

/**
 * class representing a test with a submission
 */
export class TestWithSubmissionVM extends BaseTestSubmissionVM {
  /**
   * identifier of the test
   */
  public testId: number;

  /**
   * topic of the test
   */
  public testTopic: string;

  /**
   * answers in the submission
   */
  public answers: SubmissionAnswerWithCorrectAnswerVM[] = [];
}
