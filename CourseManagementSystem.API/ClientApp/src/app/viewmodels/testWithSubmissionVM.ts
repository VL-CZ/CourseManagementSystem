import {SubmissionAnswerVM} from './testSubmissionVM';

export class TestWithSubmissionVM {
  public testId: number;
  public testTopic: string;
  public submissionId: number;
  public answers: SubmissionAnswerWithCorrectAnswerVM[];

  constructor(testId: number, testTopic: string, submissionId: number, answers: SubmissionAnswerWithCorrectAnswerVM[]) {
    this.testId = testId;
    this.testTopic = testTopic;
    this.answers = answers;
    this.submissionId = submissionId;
  }
}


export class SubmissionAnswerWithCorrectAnswerVM extends SubmissionAnswerVM {
  public correctAnswer: string;

  constructor(questionNumber: number, questionText: string, answerText: string, correctAnswer: string) {
    super(questionNumber, questionText, answerText);
    this.correctAnswer = correctAnswer;
  }
}
