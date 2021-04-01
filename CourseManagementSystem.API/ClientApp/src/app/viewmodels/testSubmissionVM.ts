/**
 * viewmodel representing a test submission
 */
export class TestSubmissionVM {
  /**
   * id of the test
   */
  public testId: number;

  /**
   * topic of the test
   */
  public testTopic: string;

  /**
   * answers to the test
   */
  public answers: SubmissionAnswerVM[];

  constructor(testId: number, testTopic: string) {
    this.testId = testId;
    this.testTopic = testTopic;
    this.answers = [];
  }

  /**
   * get default instance of this class
   */
  public static getDefault(): TestSubmissionVM {
    return new TestSubmissionVM(0, '');
  }
}

/**
 * viewmodel representing an answer within a test submission
 */
export class SubmissionAnswerVM {
  /**
   * number of question that this answer belongs to
   */
  public questionNumber: number;

  /**
   * text provided to the question
   */
  public questionText: string;

  /**
   * answer to the question
   */
  public answerText: string;

  constructor(questionNumber: number, questionText: string, answerText: string) {
    this.questionNumber = questionNumber;
    this.questionText = questionText;
    this.answerText = answerText;
  }
}
