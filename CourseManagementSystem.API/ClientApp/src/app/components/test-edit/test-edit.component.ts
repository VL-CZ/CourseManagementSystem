import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseTestService} from '../../services/course-test.service';
import {ActivatedRouteUtils} from '../../utils/activatedRouteUtils';
import {AddCourseTestVM, CourseTestDetailsVM} from '../../viewmodels/courseTestVM';
import {TestQuestionVM} from '../../viewmodels/testQuestionVM';
import {TestQuestionNumberSetter} from '../../utils/testQuestionNumberSetter';
import {DateTimeBinder} from '../../utils/dateTimeBinder';
import {DateTimeFormatter} from '../../utils/dateTimeFormatter';
import {CourseTestUtils} from '../../utils/courseTestUtils';
import {ObservableWrapper} from '../../utils/observableWrapper';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ConfirmDialogManager} from '../../utils/confirmDialogManager';

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

  /**
   * id of the course that this test belongs to
   */
  public courseId: string;

  public courseTestUtils: CourseTestUtils = new CourseTestUtils();

  private router: Router;
  private courseTestService: CourseTestService;
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private observableWrapper: ObservableWrapper;
  private confirmDialogManager: ConfirmDialogManager;

  constructor(activatedRoute: ActivatedRoute, router: Router, courseTestService: CourseTestService, bsModalService: BsModalService) {
    this.router = router;
    this.testId = ActivatedRouteUtils.getIdParam(activatedRoute);
    this.courseTestService = courseTestService;
    this.bsModalService = bsModalService;
    this.observableWrapper = new ObservableWrapper(this.bsModalRef, this.bsModalService);
    this.confirmDialogManager = new ConfirmDialogManager(this.bsModalRef, this.bsModalService);

    courseTestService.getById(this.testId).subscribe(result => {
      this.testToUpdate = result;
    });

    courseTestService.getCourseId(this.testId).subscribe(result => {
      this.courseId = result.value;
    });
  }

  ngOnInit() {
  }

  /**
   * delete a question from the test
   * @param question question to delete
   */
  public deleteQuestion(question: TestQuestionVM): void {
    this.confirmDialogManager.displayDialog(
      'Delete a question',
      'Are you sure you want to delete this question?',
      () => {
        this.testToUpdate.questions = this.testToUpdate.questions.filter(q => q !== question);
        TestQuestionNumberSetter.setQuestionNumbers(this.testToUpdate.questions);
      });
  }

  /**
   * add new question to the test
   */
  public addQuestion(): void {
    this.testToUpdate.questions.push(new TestQuestionVM());
    TestQuestionNumberSetter.setQuestionNumbers(this.testToUpdate.questions);
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

    this.observableWrapper.subscribeOrShowError(
      this.courseTestService.updateTest(this.testId, updatedTest),
      () => {
        this.router.navigate(['/tests', this.testId]);
      });
  }

  /**
   * discard the changes made to this assignment
   */
  public discardChanges(): void {
    this.confirmDialogManager.displayDialog(
      'Discard changes',
      'Are you sure you want to discard these changes?',
      () => {
        this.router.navigate(['/tests', this.testId]);
      });
  }
}
