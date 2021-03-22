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

@Component({
  selector: 'app-test-submission-review',
  templateUrl: './test-submission-review.component.html',
  styleUrls: ['./test-submission-review.component.css']
})
export class TestSubmissionReviewComponent implements OnInit {
  public submission: TestWithSubmissionVM;
  public evaluatedTestSubmission: EvaluatedTestSubmissionVM;
  public isAdmin: boolean;

  private testSubmissionService: TestSubmissionService;
  private router: Router;
  private route: ActivatedRoute;

  constructor(route: ActivatedRoute, testSubmissionService: TestSubmissionService, roleAuthService: RoleAuthService, router: Router) {
    this.testSubmissionService = testSubmissionService;
    this.router = router;
    this.route = route;

    const submissionId = ActivatedRouteUtils.getIdParam(route);

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

  public isCorrect(answer: SubmissionAnswerWithCorrectAnswerVM): boolean {
    return answer.receivedPoints >= answer.maximalPoints;
  }

  public getReceivedPoints(testSubmission: TestWithSubmissionVM): number {
    return ArrayUtils.sum(testSubmission.answers.map(answer => answer.receivedPoints));
  }

  public getMaximalPoints(testSubmission: TestWithSubmissionVM): number {
    return ArrayUtils.sum(testSubmission.answers.map(answer => answer.maximalPoints));
  }

  public getPercentualScore(testSubmission: TestWithSubmissionVM): number {
    const percentualScore = this.getReceivedPoints(testSubmission) / this.getMaximalPoints(testSubmission);
    return PercentCalculator.doubleToPercent(percentualScore, 2);
  }

  public getCorrespondingEvaluatedAnswer(answer: SubmissionAnswerWithCorrectAnswerVM): EvaluatedAnswerVM {
    return this.evaluatedTestSubmission.evaluatedAnswers.find(ans => ans.questionNumber === answer.questionNumber);
  }

  public discardUpdates(): void {
    this.evaluatedTestSubmission = EvaluatedTestSubmissionVM.createFrom(this.submission);
  }

  public saveUpdates(): void {
    this.testSubmissionService.updateSubmission(this.submission.submissionId.toString(), this.evaluatedTestSubmission)
      .subscribe(() => {
        RouterUtils.reloadPage(this.router, this.route);
      });
  }

}
