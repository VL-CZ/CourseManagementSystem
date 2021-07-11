import {Component, Input, OnInit} from '@angular/core';
import {QuestionType, TestQuestionVM} from '../../viewmodels/testQuestionVM';
import {ArrayUtils} from '../../utils/arrayUtils';
import {PossibleAnswer, QuestionWithAnswerChoices} from '../../utils/questionWithAnswerChoices';

@Component({
  selector: 'app-test-question',
  templateUrl: './test-question.component.html',
  styleUrls: ['./test-question.component.css']
})
export class TestQuestionComponent implements OnInit {

  @Input()
  public question: TestQuestionVM;

  public possibleAnswersCount: number;
  public questionWithAnswerChoices: QuestionWithAnswerChoices;

  constructor() {
  }

  ngOnInit() {
      this.questionWithAnswerChoices = QuestionWithAnswerChoices.createFrom(this.question.questionText);
  }

  /**
   * simple getter, in order to allow access to enum in template
   * @constructor
   */
  public get QuestionType() {
    return QuestionType;
  }

  public hasTextAnswer(): boolean {
    return this.question.type === QuestionType.TextAnswer;
  }

  public updateChoicesCount(): void {
    ArrayUtils.resize(this.questionWithAnswerChoices.possibleAnswers, this.possibleAnswersCount,
      new PossibleAnswer('', ''));
    this.setChoicesLetters();
  }

  public serializePossibleAnswers(): void {
    this.question.questionText = this.questionWithAnswerChoices.serialize();
  }

  private setChoicesLetters(): void {
    const allowedLetters = 'ABCDEFGHIJ';
    for (let i = 0; i < this.questionWithAnswerChoices.possibleAnswers.length; i++) {
      this.questionWithAnswerChoices.possibleAnswers[i].answerLetter = allowedLetters[i];
    }
  }
}
