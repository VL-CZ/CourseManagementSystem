export class TestSubmissionInfoVM {
  public testSubmissionId: number;
  public testTopic: string;
  public testWeight: number;
  public percentualScore: number;

  constructor(testSubmissionId: number, testTopic: string, testWeight: number, percentualScore: number) {
    this.testSubmissionId = testSubmissionId;
    this.testTopic = testTopic;
    this.testWeight = testWeight;
    this.percentualScore = percentualScore;
  }
}
