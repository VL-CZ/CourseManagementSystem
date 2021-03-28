/**
 * viewmodel representing a test submission
 */
export class TestSubmissionVM {
  /**
   * id of the test
   */
  testId: number;

  /**
   * topic of the test
   */
  testTopic: string;

  /**
   * answers to the test
   */
  answers: SubmissionAnswerVM[];

  constructor(testId: number, testTopic: string) {
    this.testId = testId;
    this.testTopic = testTopic;
    this.answers = [];
  }
}

/**
 * viewmodel representing an answer within a test submission
 */
export class SubmissionAnswerVM {
  /**
   * number of question that this answer belongs to
   */
  questionNumber: number;

  /**
   * text provided to the question
   */
  questionText: string;

  /**
   * answer to the question
   */
  answerText: string;

  constructor(questionNumber: number, questionText: string, answerText: string) {
    this.questionNumber = questionNumber;
    this.questionText = questionText;
    this.answerText = answerText;
  }
}
