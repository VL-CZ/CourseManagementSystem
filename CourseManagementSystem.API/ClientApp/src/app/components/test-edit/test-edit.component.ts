import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseTestService} from '../../services/course-test.service';
import {ActivatedRouteTools} from '../../tools/activatedRouteTools';
import {AddCourseTestVM, CourseTestDetailsVM} from '../../viewmodels/courseTestVM';
import {TestQuestionVM} from '../../viewmodels/testQuestionVM';
import {TestQuestionNumberSetter} from '../../tools/testQuestionNumberSetter';
import {DateTimeBinder} from '../../tools/datetime/dateTimeBinder';
import {DateTimeFormatter} from '../../tools/datetime/dateTimeFormatter';
import {CourseTestTools} from '../../tools/courseTestTools';
import {ObservableWrapper} from '../../tools/observableWrapper';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ConfirmDialogManager} from '../../tools/dialog-managers/confirmDialogManager';
import {PageNavigator} from '../../tools/pageNavigator';

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
   * check if we are editing the deadline of the test
   */
  public editingDeadline = false;

  /**
   * id of the course that this test belongs to
   */
  public courseId: string;

  public courseTestUtils: CourseTestTools = new CourseTestTools();

  private readonly pageNavigator: PageNavigator;
  private courseTestService: CourseTestService;
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private observableWrapper: ObservableWrapper;
  private confirmDialogManager: ConfirmDialogManager;

  constructor(activatedRoute: ActivatedRoute, router: Router, courseTestService: CourseTestService, bsModalService: BsModalService) {
    this.pageNavigator = new PageNavigator(router);
    this.testId = ActivatedRouteTools.getIdParam(activatedRoute);
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
    if (!this.dateTimeBinder.isEmpty() && this.editingDeadline) {
      this.testToUpdate.deadline = this.dateTimeBinder.toString();
    }
    const updatedTest = AddCourseTestVM.getFrom(this.testToUpdate);
    TestQuestionNumberSetter.setQuestionNumbers(updatedTest.questions);

    this.observableWrapper.subscribeOrShowError(
      this.courseTestService.updateTest(this.testId, updatedTest),
      () => {
        this.pageNavigator.navigateToAssignmentDetail(this.testId);
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
        this.pageNavigator.navigateToAssignmentDetail(this.testId);
      });
  }

  /**
   * start editation of deadline property
   */
  public startEditingDeadline(): void {
    this.editingDeadline = true;
  }

  /**
   * cancel editation of deadline property
   */
  public cancelEditingDeadline(): void {
    this.editingDeadline = false;
  }
}
