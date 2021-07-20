import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {TestSubmissionService} from '../../services/test-submission.service';
import {ActivatedRouteTools} from '../../tools/activatedRouteTools';
import {TestWithSubmissionVM} from '../../viewmodels/testSubmissionVM';
import {ArrayTools} from '../../tools/arrayTools';
import {PercentCalculator} from '../../tools/percent-tools/percentCalculator';
import {RoleAuthService} from '../../services/role-auth.service';
import {EvaluatedAnswerVM, EvaluatedTestSubmissionVM} from '../../viewmodels/evaluatedTestSubmissionVM';
import {PageNavigator} from '../../tools/pageNavigator';
import {SubmissionAnswerWithCorrectAnswerVM} from '../../viewmodels/testSubmissionAnswerVM';
import {DateTimeFormatter} from '../../tools/datetime/dateTimeFormatter';
import {CourseTestTools} from '../../tools/courseTestTools';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ObservableWrapper} from '../../tools/observableWrapper';
import {ConfirmDialogManager} from '../../tools/dialog-managers/confirmDialogManager';

/**
 * component representing detail of the submitted test solution
 */
@Component({
  selector: 'app-test-submission-review',
  templateUrl: './test-submission-review.component.html',
  styleUrls: ['./test-submission-review.component.css']
})
export class TestSubmissionReviewComponent implements OnInit {
  /**
   * submitted test solution
   */
  public submission: TestWithSubmissionVM = new TestWithSubmissionVM();

  /**
   * test solution with updates (manually evaluated answers)
   */
  public evaluatedTestSubmission: EvaluatedTestSubmissionVM = EvaluatedTestSubmissionVM.getDefault();

  /**
   * formatter of date-time
   */
  public dateTimeFormatter: DateTimeFormatter = new DateTimeFormatter();

  /**
   * is the current user admin of the course that contains this test?
   */
  public isAdmin: boolean;

  /**
   * id of the course that this test submission belongs to
   */
  public courseId: string;

  /**
   * are we editing the submission?
   */
  public editing: boolean;

  public courseTestUtils: CourseTestTools = new CourseTestTools();

  private testSubmissionService: TestSubmissionService;
  private readonly pageNavigator: PageNavigator;
  private readonly activatedRoute: ActivatedRoute;
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private observableWrapper: ObservableWrapper;
  private confirmDialogManager: ConfirmDialogManager;

  constructor(activatedRoute: ActivatedRoute, testSubmissionService: TestSubmissionService,
              roleAuthService: RoleAuthService, router: Router, bsModalService: BsModalService) {
    this.testSubmissionService = testSubmissionService;
    this.pageNavigator = new PageNavigator(router);
    this.activatedRoute = activatedRoute;
    this.bsModalService = bsModalService;
    this.observableWrapper = new ObservableWrapper(this.bsModalRef, this.bsModalService);
    this.confirmDialogManager = new ConfirmDialogManager(this.bsModalRef, this.bsModalService);
    this.editing = false;

    const submissionId = ActivatedRouteTools.getIdParam(activatedRoute);

    testSubmissionService.getSubmissionById(submissionId).subscribe(submission => {
      this.submission = submission;
      this.evaluatedTestSubmission = EvaluatedTestSubmissionVM.createFrom(submission);

      roleAuthService.isCourseTestAdmin(this.submission.testId).subscribe(result => {
        this.isAdmin = result.value;
      });
    });

    testSubmissionService.getCourseId(submissionId).subscribe(result => {
      this.courseId = result.value;
    });
  }

  ngOnInit() {
  }

  /**
   * get name of the CSS class for this answer based on the number of points
   * @param answer answer to check
   */
  public getAnswerClassName(answer: SubmissionAnswerWithCorrectAnswerVM): string {
    if (answer.receivedPoints >= answer.maximalPoints) {
      return 'correctAnswer';
    } else if (answer.receivedPoints === 0) {
      return 'wrongAnswer';
    } else {
      return 'partiallyCorrectAnswer';
    }
  }

  /**
   * get received points for the test
   * @param testSubmission test solution
   */
  public getReceivedPoints(testSubmission: TestWithSubmissionVM): number {
    return ArrayTools.sum(testSubmission.answers.map(answer => answer.receivedPoints));
  }

  /**
   * get maximal points for the test
   * @param testSubmission test solution
   */
  public getMaximalPoints(testSubmission: TestWithSubmissionVM): number {
    return ArrayTools.sum(testSubmission.answers.map(answer => answer.maximalPoints));
  }

  /**
   * get percentual score of this test
   * @param testSubmission test submission to evaluate
   */
  public getPercentualScore(testSubmission: TestWithSubmissionVM): string {
    const percentualScore = this.getReceivedPoints(testSubmission) / this.getMaximalPoints(testSubmission);
    return PercentCalculator.doubleToPercent(percentualScore, 2) + '%';
  }

  /**
   * get corresponding answer of the {@link evaluatedTestSubmission} to the test answer
   * @param answer answer in the test
   */
  public getCorrespondingEvaluatedAnswer(answer: SubmissionAnswerWithCorrectAnswerVM): EvaluatedAnswerVM {
    return this.evaluatedTestSubmission.evaluatedAnswers.find(ans => ans.questionNumber === answer.questionNumber);
  }

  /**
   * discard updates of the test submission
   */
  public discardUpdates(): void {
    this.confirmDialogManager.displayDialog(
      'Discard updates',
      'Are you sure you want to discard these updates?',
      () => {
        this.editing = false;
        this.evaluatedTestSubmission = EvaluatedTestSubmissionVM.createFrom(this.submission);
      });
  }

  /**
   * save updates of the test submission
   */
  public saveUpdates(): void {
    this.updateSubmission();
  }

  /**
   * start editing the submission
   */
  public startReview(): void {
    this.editing = true;
  }

  /**
   * mark the test submission as reviewed
   */
  public markAsReviewed(): void {
    this.updateSubmission();
  }

  /**
   * check if the answer has comment
   * @param answer selected answer to check
   */
  public hasComment(answer: SubmissionAnswerWithCorrectAnswerVM): boolean {
    return answer.comment && answer.comment.length >= 1;
  }

  /**
   * update the assignment submission
   * @private
   */
  private updateSubmission(): void {
    this.observableWrapper.subscribeOrShowError(
      this.testSubmissionService.updateSubmission(this.submission.testSubmissionId.toString(), this.evaluatedTestSubmission),
      () => {
        this.pageNavigator.reloadCurrentPage(this.activatedRoute, true);
      });
  }
}
