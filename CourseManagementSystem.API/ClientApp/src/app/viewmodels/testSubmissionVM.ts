export class TestSubmissionVM {
  testId: number;
  testTopic: string;
  answers: SubmissionAnswerVM[];

  constructor(testId: number, testTopic: string) {
    this.testId = testId;
    this.testTopic = testTopic;
    this.answers = [];
  }
}

export class SubmissionAnswerVM {
  questionNumber: number;
  questionText: string;
  answerText: string;

  constructor(questionNumber: number) {
    this.questionNumber = questionNumber;
  }
}
