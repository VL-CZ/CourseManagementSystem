import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseTestService} from '../course-test.service';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';
import {AddCourseTestVM, CourseTestDetailsVM} from '../viewmodels/courseTestVM';
import {TestQuestionVM} from '../viewmodels/testQuestionVM';
import {TestQuestionNumberSetter} from '../utils/testQuestionNumberSetter';
import {DateTimeBinder} from '../utils/dateTimeBinder';
import {DateTimeFormatter} from '../utils/dateTimeFormatter';

/**
 * component for editing a test
 */
@Component({
  selector: 'app-test-edit',
  templateUrl: './test-edit.component.html',
  styleUrls: ['./test-edit.component.css']
})
export class TestEditComponent implements OnInit {

  /**
   * test that we edit
   */
  public testToUpdate: CourseTestDetailsVM = new CourseTestDetailsVM();

  /**
   * id of the test
   */
  public testId: string;

  /**
   * class for binding date-time values
   */
  public dateTimeBinder = new DateTimeBinder();

  /**
   * formatter of date-time
   */
  public dateTimeFormatter = new DateTimeFormatter();

  private router: Router;
  private courseTestService: CourseTestService;

  constructor(activatedRoute: ActivatedRoute, router: Router, courseTestService: CourseTestService) {
    this.router = router;
    this.testId = ActivatedRouteUtils.getIdParam(activatedRoute);
    this.courseTestService = courseTestService;

    courseTestService.getById(this.testId).subscribe(result => {
      this.testToUpdate = result;
    });
  }

  ngOnInit() {
  }

  /**
   * delete a question from the test
   * @param question question to delete
   */
  public deleteQuestion(question: TestQuestionVM): void {
    this.testToUpdate.questions = this.testToUpdate.questions.filter(q => q !== question);
  }

  /**
   * add new question to the test
   */
  public addQuestion(): void {
    this.testToUpdate.questions.push(new TestQuestionVM());
  }

  /**
   * save changes to the test
   */
  public saveChanges(): void {
    if (!this.dateTimeBinder.isEmpty()) {
      this.testToUpdate.deadline = this.dateTimeBinder.toString();
    }
    const updatedTest = AddCourseTestVM.getFrom(this.testToUpdate);
    TestQuestionNumberSetter.setQuestionNumbers(updatedTest.questions);

    this.courseTestService.updateTest(this.testId, updatedTest).subscribe(() => {
      this.router.navigate(['/tests', this.testId]);
    });
  }
}
