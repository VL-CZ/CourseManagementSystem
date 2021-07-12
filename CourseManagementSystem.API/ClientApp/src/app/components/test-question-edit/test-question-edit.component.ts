import {Component, Input, OnInit} from '@angular/core';
import {QuestionType, TestQuestionVM} from '../../viewmodels/testQuestionVM';
import {ArrayUtils} from '../../utils/arrayUtils';
import {AnswerChoice, QuestionWithAnswerChoices} from '../../utils/questionWithAnswerChoices';

/**
 * component that represents creating/editing a question
 */
@Component({
  selector: 'app-test-question-edit',
  templateUrl: './test-question-edit.component.html',
  styleUrls: ['./test-question-edit.component.css']
})
export class TestQuestionEditComponent implements OnInit {

  /**
   * given question
   */
  @Input()
  public question: TestQuestionVM;

  /**
   * number of all possible answers to the question
   * applied only if the question has choices of answers
   */
  public possibleAnswersCount: number;

  /**
   * the question with choices of answers
   * applied only if the question has choices of answers
   */
  public questionWithAnswerChoices: QuestionWithAnswerChoices;

  constructor() {
  }

  ngOnInit() {
    if (!this.hasTextAnswer()) {
      this.questionWithAnswerChoices = QuestionWithAnswerChoices.createFrom(this.question.questionText);
      this.possibleAnswersCount = this.questionWithAnswerChoices.answerChoices.length;
    }
  }

  /**
   * simple getter, in order to allow access to enum in template
   * @constructor
   */
  public get QuestionType() {
    return QuestionType;
  }

  /**
   * check if the question has textual answer (no choices of answers)
   */
  public hasTextAnswer(): boolean {
    return this.question.type === QuestionType.TextAnswer;
  }

  /**
   * update number of answer choices
   * applied only if the question has choices of answers
   */
  public updateChoicesCount(): void {
    ArrayUtils.resize(this.questionWithAnswerChoices.answerChoices, this.possibleAnswersCount,
      new AnswerChoice('', ''));
    this.setAnswerChoicesLetters();
    this.serializePossibleAnswers();
  }

  /**
   * serialize all possible answers to string and store it to {@link question.questionText}
   * applied only if the question has choices of answers
   */
  public serializePossibleAnswers(): void {
    this.question.questionText = this.questionWithAnswerChoices.serialize();
  }

  /**
   * set letters (A,B,C,...) to answer choices
   * applied only if the question has choices of answers
   * @private
   */
  private setAnswerChoicesLetters(): void {
    const allowedLetters = 'ABCDEFGHIJ';
    for (let i = 0; i < this.questionWithAnswerChoices.answerChoices.length; i++) {
      this.questionWithAnswerChoices.answerChoices[i].answerLetter = allowedLetters[i];
    }
  }
}
