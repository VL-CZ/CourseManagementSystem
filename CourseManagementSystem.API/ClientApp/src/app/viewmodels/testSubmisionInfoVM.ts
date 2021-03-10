export class TestSubmissionInfoVM {
  testSubmissionId: number;
  testTopic: string;

  constructor(testSubmissionId: number, testTopic: string) {
    this.testSubmissionId = testSubmissionId;
    this.testTopic = testTopic;
  }
}
