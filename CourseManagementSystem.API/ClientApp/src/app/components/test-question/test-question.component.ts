import {Component, Input, OnInit} from '@angular/core';
import {QuestionType, TestQuestionVM} from '../../viewmodels/testQuestionVM';
import {ArrayUtils} from '../../utils/arrayUtils';

@Component({
  selector: 'app-test-question',
  templateUrl: './test-question.component.html',
  styleUrls: ['./test-question.component.css']
})
export class TestQuestionComponent implements OnInit {

  @Input()
  public question: TestQuestionVM;

  public questionText = '';
  public possibleAnswersCount: number;
  public possibleAnswers: AnswerChoice[] = [];

  constructor() {
  }

  ngOnInit() {
    this.questionText = this.question.questionText;
  }

  public get QuestionType() {
    return QuestionType;
  }

  public hasTextAnswer(): boolean {
    return this.question.type === QuestionType.TextAnswer;
  }

  public updateChoicesCount(): void {
    ArrayUtils.resize(this.possibleAnswers, this.possibleAnswersCount, new AnswerChoice('', ''));
    this.setChoicesLetters();
  }

  public serializePossibleAnswers(): void {
    const serializedAnswers = this.possibleAnswers.map(choice => `${choice.answerLetter}||${choice.answerText}`).join('|||');
    this.question.questionText = `${this.questionText}|||${serializedAnswers}`;
  }

  private setChoicesLetters(): void {
    const allowedLetters = 'ABCDEFGHIJ';
    for (let i = 0; i < this.possibleAnswers.length; i++) {
      this.possibleAnswers[i].answerLetter = allowedLetters[i];
    }
  }
}

class AnswerChoice {
  constructor(public answerLetter: string, public answerText: string) {
  }
}
