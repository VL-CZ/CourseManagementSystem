import {Component, Input, OnInit} from '@angular/core';
import {SubmissionAnswerVM} from '../../viewmodels/testSubmissionAnswerVM';
import {QuestionType} from '../../viewmodels/testQuestionVM';
import {QuestionWithAnswerChoices} from '../../utils/questionWithAnswerChoices';

/**
 * component representing form with answer of the test to submit
 */
@Component({
  selector: 'app-test-question-answer-form',
  templateUrl: './test-question-answer-form.component.html',
  styleUrls: ['./test-question-answer-form.component.css']
})
export class TestQuestionAnswerFormComponent implements OnInit {

  /**
   * delimiter that joins selected answers in {@link answer.answerText}
   * applied only if the question has multiple choice of answers (checkboxes)
   * @private
   */
  private static readonly multipleAnswersDelimiter = ', ';

  /**
   * answer object
   */
  @Input()
  public answer: SubmissionAnswerVM;

  /**
   * question with choice of answers
   * we use this property only if the question has choice of answers
   */
  public questionWithAnswers: QuestionWithAnswerChoices;

  /**
   * dictionary of selected answers (key - answer letter, value - is it selected?)
   * we use this property only if the question has multiple choice of answers
   */
  public isSelectedAnswer: { [id: string]: boolean } = {};

  constructor() {
  }

  /**
   * simple getter, in order to allow access to enum in template
   * @constructor
   */
  public get QuestionType() {
    return QuestionType;
  }

  ngOnInit() {
    this.questionWithAnswers = QuestionWithAnswerChoices.createFrom(this.answer.questionText);

    if (this.answer.questionType === QuestionType.MultipleChoice) {
      // mark all answers as notSelected
      this.setSelectedAnswers();
    }
  }

  /**
   * toggle checkbox answer
   * @param answerLetter letter of the answer
   */
  public toggleCheckboxAnswer(answerLetter: string) {
    // toggle the value
    this.isSelectedAnswer[answerLetter] = !this.isSelectedAnswer[answerLetter];
    this.updateSelectedAnswers();
  }

  /**
   * set {@link answer.answerText} to contain all letters of the selected answers
   * applies only if the question has answer with checkboxes
   * @private
   */
  private updateSelectedAnswers() {
    const selectedAnswers = Object.entries(this.isSelectedAnswer).filter(
      ([letter, isSelected]) => isSelected
    ).map(
      ([letter, isSelected]) => letter
    );

    this.answer.answerText = selectedAnswers.join(TestQuestionAnswerFormComponent.multipleAnswersDelimiter);
  }

  /**
   * set checkbox answers as selected (we parse the {@link answer.answerText})
   * @private
   */
  private setSelectedAnswers(): void {
    const selectedAnswers = this.answer.answerText.split(TestQuestionAnswerFormComponent.multipleAnswersDelimiter);
    for (const ans of this.questionWithAnswers.answerChoices) {
      this.isSelectedAnswer[ans.answerLetter] = selectedAnswers.includes(ans.answerLetter);
    }
  }
}
