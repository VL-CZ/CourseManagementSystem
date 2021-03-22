import {TestWithSubmissionVM} from './testWithSubmissionVM';

export class EvaluatedTestSubmissionVM {
  public testSubmissionId: number;
  public evaluatedAnswers: EvaluatedAnswerVM[];

  constructor(testSubmissionId: number, evaluatedAnswers: EvaluatedAnswerVM[]) {
    this.testSubmissionId = testSubmissionId;
    this.evaluatedAnswers = evaluatedAnswers;
  }

  /**
   * create new evaluated test submission from test with the submission
   * @param testWithSubmission given submission with the test
   */
  public static createFrom(testWithSubmission: TestWithSubmissionVM): EvaluatedTestSubmissionVM {
    const evaluatedAnswers: EvaluatedAnswerVM[] = [];
    for (const answer of testWithSubmission.answers) {
      const evaluatedAnswer = new EvaluatedAnswerVM(answer.questionNumber, answer.receivedPoints, answer.comment);
      evaluatedAnswers.push(evaluatedAnswer);
    }

    return new EvaluatedTestSubmissionVM(testWithSubmission.submissionId, evaluatedAnswers);
  }
}

export class EvaluatedAnswerVM {
  public questionNumber: number;
  public updatedPoints: number;
  public updatedComment: string;

  constructor(answerNumber: number, updatedPoints: number, updatedComment: string) {
    this.questionNumber = answerNumber;
    this.updatedPoints = updatedPoints;
    this.updatedComment = updatedComment;
  }
}
