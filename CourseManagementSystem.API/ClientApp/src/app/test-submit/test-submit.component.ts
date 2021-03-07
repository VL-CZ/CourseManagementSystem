import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseTestService} from '../course-test.service';
import {CourseTestVM} from '../viewmodels/courseTestVM';
import {TestSubmitService} from '../test-submit.service';
import {SubmissionAnswerVM, TestSubmissionVM} from '../viewmodels/testSubmissionVM';
import {TestQuestion} from '../viewmodels/testQuestion';

@Component({
  selector: 'app-test-submit',
  templateUrl: './test-submit.component.html',
  styleUrls: ['./test-submit.component.css']
})
export class TestSubmitComponent implements OnInit {

  private testSubmitService: TestSubmitService;

  public test: CourseTestVM;
  public testSubmission: TestSubmissionVM;

  constructor(route: ActivatedRoute, courseTestService: CourseTestService, testSubmitService: TestSubmitService) {
    const testId = route.snapshot.paramMap.get('id');
    this.testSubmitService = testSubmitService;

    this.testSubmission = new TestSubmissionVM();

    courseTestService.getById(testId).subscribe(test => {
      this.test = test;
      this.initializeAnswers();
    });
  }

  ngOnInit() {
  }

  public submit() {
    console.log(JSON.stringify(this.testSubmission));
    // this.testSubmitService.submit(this.test.id).subscribe(() => {
    //
    // });
  }

  /**
   * get question with selected question number
   * @param questionNumber
   */
  public getQuestionByItsNumber(questionNumber: number): TestQuestion {
    return this.test.questions.find(q => q.number === questionNumber);
  }

  /**
   * create empty answers with corresponding `questionNumber`
   * @private
   */
  private initializeAnswers() {
    const answers: SubmissionAnswerVM[] = [];
    for (const question of this.test.questions) {
      answers.push(new SubmissionAnswerVM(question.number));
    }
    this.testSubmission.answers = answers;
  }
}
