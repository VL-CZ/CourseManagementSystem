export class TestSubmissionWithUserInfoVM {
  public studentEmail: string;
  public testSubmissionId: number;
  public percentualScore: number;

  constructor(studentEmail: string, testSubmissionId: number, percentualScore: number) {
    this.studentEmail = studentEmail;
    this.testSubmissionId = testSubmissionId;
    this.percentualScore = percentualScore;
  }
}
