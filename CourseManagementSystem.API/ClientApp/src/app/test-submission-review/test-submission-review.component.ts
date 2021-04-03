import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {TestSubmissionService} from '../test-submission.service';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';
import {SubmissionAnswerWithCorrectAnswerVM, TestWithSubmissionVM} from '../viewmodels/testWithSubmissionVM';
import {ArrayUtils} from '../utils/arrayUtils';
import {PercentCalculator} from '../utils/percentCalculator';
import {RoleAuthService} from '../role-auth.service';
import {EvaluatedAnswerVM, EvaluatedTestSubmissionVM} from '../viewmodels/evaluatedTestSubmissionVM';
import {RouterUtils} from '../utils/routerUtils';

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
  public submission: TestWithSubmissionVM = TestWithSubmissionVM.getDefault();

  /**
   * test solution with updates (manually evaluated answers)
   */
  public evaluatedTestSubmission: EvaluatedTestSubmissionVM = EvaluatedTestSubmissionVM.getDefault();

  /**
   * is the current user admin?
   */
  public isAdmin: boolean;

  private testSubmissionService: TestSubmissionService;
  private readonly router: Router;
  private readonly activatedRoute: ActivatedRoute;

  constructor(activatedRoute: ActivatedRoute, testSubmissionService: TestSubmissionService,
              roleAuthService: RoleAuthService, router: Router) {
    this.testSubmissionService = testSubmissionService;
    this.router = router;
    this.activatedRoute = activatedRoute;

    const submissionId = ActivatedRouteUtils.getIdParam(activatedRoute);

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });

    testSubmissionService.getSubmissionById(submissionId).subscribe(submission => {
      this.submission = submission;
      this.evaluatedTestSubmission = EvaluatedTestSubmissionVM.createFrom(submission);
    });
  }

  ngOnInit() {
  }

  /**
   * is the given answer correct?
   * @param answer answer to check
   */
  public isCorrect(answer: SubmissionAnswerWithCorrectAnswerVM): boolean {
    return answer.receivedPoints >= answer.maximalPoints;
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
    this.evaluatedTestSubmission = EvaluatedTestSubmissionVM.createFrom(this.submission);
  }

  /**
   * save updates of the test submission
   */
  public saveUpdates(): void {
    this.testSubmissionService.updateSubmission(this.submission.submissionId.toString(), this.evaluatedTestSubmission)
      .subscribe(() => {
        RouterUtils.reloadPage(this.router, this.activatedRoute);
      });
  }
}
