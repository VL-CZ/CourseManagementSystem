/**
 * viewmodel representing info about a test submission
 */
export class TestSubmissionInfoVM {
  /**
   * id of the test submission
   */
  public testSubmissionId: number;

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

  constructor(testSubmissionId: number, testTopic: string, testWeight: number, percentualScore: number) {
    this.testSubmissionId = testSubmissionId;
    this.testTopic = testTopic;
    this.testWeight = testWeight;
    this.percentualScore = percentualScore;
  }
}
