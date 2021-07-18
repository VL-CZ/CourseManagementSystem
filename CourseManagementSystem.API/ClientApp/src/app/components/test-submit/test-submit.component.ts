import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseTestService} from '../../services/course-test.service';
import {SubmitTestVM} from '../../viewmodels/submitTestVM';
import {TestSubmissionService} from '../../services/test-submission.service';
import {ActivatedRouteTools} from '../../tools/activatedRouteTools';
import {CourseTestTools} from '../../tools/courseTestTools';
import {DateTimeFormatter} from '../../tools/datetime/dateTimeFormatter';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ObservableWrapper} from '../../tools/observableWrapper';
import {ConfirmDialogManager} from '../../tools/dialog-managers/confirmDialogManager';
import {ConfirmButtonStyle} from '../confirm-dialog/confirm-dialog.component';
import {PageNavigator} from '../../tools/pageNavigator';
import {InformationDialogManager} from '../../tools/dialog-managers/informationDialogManager';

/**
 * component for submitting a test
 */
@Component({
  selector: 'app-test-submit',
  templateUrl: './test-submit.component.html',
  styleUrls: ['./test-submit.component.css']
})
export class TestSubmitComponent implements OnInit {

  /**
   * test to submit
   */
  public testSubmission: SubmitTestVM = new SubmitTestVM();

  /**
   * id of the course that this test submission belongs to
   */
  public courseId: string;

  /**
   * formatter of date-time values
   */
  public dateTimeFormatter: DateTimeFormatter = new DateTimeFormatter();

  public courseTestUtils: CourseTestTools = new CourseTestTools();

  private readonly pageNavigator: PageNavigator;
  private testSubmitService: TestSubmissionService;
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private observableWrapper: ObservableWrapper;
  private confirmDialogManager: ConfirmDialogManager;
  private informationDialogManager: InformationDialogManager;

  constructor(route: ActivatedRoute, courseTestService: CourseTestService, testSubmissionService: TestSubmissionService, router: Router,
              bsModalService: BsModalService) {
    const testId = ActivatedRouteTools.getIdParam(route);
    this.testSubmitService = testSubmissionService;
    this.pageNavigator = new PageNavigator(router);
    this.bsModalService = bsModalService;
    this.confirmDialogManager = new ConfirmDialogManager(this.bsModalRef, this.bsModalService);
    this.informationDialogManager = new InformationDialogManager(this.bsModalRef, this.bsModalService);
    this.observableWrapper = new ObservableWrapper(this.bsModalRef, this.bsModalService);

    testSubmissionService.loadSubmission(testId).subscribe(submission => {
      this.testSubmission = submission;
      if (submission.isSubmitted) {
        this.pageNavigator.navigateToSubmissionReview(this.testSubmission.testSubmissionId);
      }
    });

    courseTestService.getCourseId(testId).subscribe(result => {
      this.courseId = result.value;
    });
  }

  ngOnInit() {
  }

  /**
   * save current answer contents
   */
  public saveAnswers(): void {
    this.observableWrapper.subscribeOrShowError(
      this.testSubmitService.saveAnswers(this.testSubmission.testSubmissionId, this.testSubmission.answers),
      () => {
        this.informationDialogManager.displayDialog('Answers have been saved.');
      });
  }

  /**
   * submit the test
   */
  public submit(): void {
    this.confirmDialogManager.displayDialog(
      'Submit the assignment',
      'Are you sure you want to submit the assignment?',
      () => {
        this.observableWrapper.subscribeOrShowError(
          // save answers
          this.testSubmitService.saveAnswers(this.testSubmission.testSubmissionId, this.testSubmission.answers),
          () => {
            // submit the test
            this.testSubmitService.submit(this.testSubmission).subscribe(
              // navigate
              () => this.pageNavigator.navigateToSubmissionReview(this.testSubmission.testSubmissionId)
            );
          });
      },
      ConfirmButtonStyle.Information
    );
  }
}
