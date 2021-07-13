import {Component, Input, OnInit} from '@angular/core';
import {QuestionType} from '../../viewmodels/testQuestionVM';
import {QuestionWithAnswerChoices} from '../../utils/questionWithAnswerChoices';

/**
 * component used for textual representation of the question
 */
@Component({
  selector: 'app-test-question-text',
  templateUrl: './test-question-text.component.html',
  styleUrls: ['./test-question-text.component.css']
})
export class TestQuestionTextComponent implements OnInit {

  /**
   * text of the question
   */
  @Input()
  public questionText: string;

  /**
   * type of the question
   */
  @Input()
  public questionType: QuestionType;

  /**
   * question together with choices of the answers
   * applied only if the question has choices of answers
   */
  public questionWithAnswers: QuestionWithAnswerChoices;

  constructor() {
  }

  ngOnInit() {
    this.questionWithAnswers = QuestionWithAnswerChoices.createFrom(this.questionText);
  }

  /**
   * simple getter, in order to allow access to enum in template
   * @constructor
   */
  public get QuestionType() {
    return QuestionType;
  }
}
