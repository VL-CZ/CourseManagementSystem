/**
 * class representing a test submission with information about an user
 */
export class TestSubmissionWithUserInfoVM {
  /**
   * email of the student
   */
  public studentEmail: string;

  /**
   * identifier of the test submission
   */
  public testSubmissionId: number;

  /**
   * percentual score from the test (0=0%,1=100%)
   */
  public percentualScore: number;

  constructor(studentEmail: string, testSubmissionId: number, percentualScore: number) {
    this.studentEmail = studentEmail;
    this.testSubmissionId = testSubmissionId;
    this.percentualScore = percentualScore;
  }
}
