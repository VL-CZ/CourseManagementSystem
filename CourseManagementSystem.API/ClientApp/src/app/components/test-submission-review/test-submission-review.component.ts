import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {TestSubmissionService} from '../../services/test-submission.service';
import {ActivatedRouteUtils} from '../../utils/activatedRouteUtils';
import {TestWithSubmissionVM} from '../../viewmodels/testSubmissionVM';
import {ArrayUtils} from '../../utils/arrayUtils';
import {PercentCalculator} from '../../utils/percentCalculator';
import {RoleAuthService} from '../../services/role-auth.service';
import {EvaluatedAnswerVM, EvaluatedTestSubmissionVM} from '../../viewmodels/evaluatedTestSubmissionVM';
import {RouterUtils} from '../../utils/routerUtils';
import {SubmissionAnswerVM, SubmissionAnswerWithCorrectAnswerVM} from '../../viewmodels/testSubmissionAnswerVM';
import {DateTimeFormatter} from '../../utils/dateTimeFormatter';
import {CourseTestUtils} from '../../utils/courseTestUtils';

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

  public courseTestUtils: CourseTestUtils = new CourseTestUtils();

  private testSubmissionService: TestSubmissionService;
  private readonly router: Router;
  private readonly activatedRoute: ActivatedRoute;

  constructor(activatedRoute: ActivatedRoute, testSubmissionService: TestSubmissionService,
              roleAuthService: RoleAuthService, router: Router) {
    this.testSubmissionService = testSubmissionService;
    this.router = router;
    this.activatedRoute = activatedRoute;
    this.editing = false;

    const submissionId = ActivatedRouteUtils.getIdParam(activatedRoute);

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
    return ArrayUtils.sum(testSubmission.answers.map(answer => answer.receivedPoints));
  }

  /**
   * get maximal points for the test
   * @param testSubmission test solution
   */
  public getMaximalPoints(testSubmission: TestWithSubmissionVM): number {
    return ArrayUtils.sum(testSubmission.answers.map(answer => answer.maximalPoints));
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
    this.editing = false;
    this.evaluatedTestSubmission = EvaluatedTestSubmissionVM.createFrom(this.submission);
  }

  /**
   * save updates of the test submission
   */
  public saveUpdates(): void {
    this.editing = false;
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
    this.testSubmissionService.updateSubmission(this.submission.testSubmissionId.toString(), this.evaluatedTestSubmission)
      .subscribe(() => {
        RouterUtils.reloadPage(this.router, this.activatedRoute);
      });
  }
}
