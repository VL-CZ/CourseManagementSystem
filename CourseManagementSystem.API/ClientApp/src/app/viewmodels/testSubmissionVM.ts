export class TestSubmissionVM {
  testId: number;
  answers: SubmissionAnswerVM[];
}

export class SubmissionAnswerVM {
  questionNumber: number;
  text: string;

  constructor(questionNumber: number) {
    this.questionNumber = questionNumber;
  }
}
