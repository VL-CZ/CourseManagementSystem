import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {TestSubmissionService} from '../test-submission.service';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';
import {SubmissionAnswerWithCorrectAnswerVM, TestWithSubmissionVM} from '../viewmodels/testWithSubmissionVM';
import {ArrayUtils} from '../utils/arrayUtils';

@Component({
  selector: 'app-test-submission-review',
  templateUrl: './test-submission-review.component.html',
  styleUrls: ['./test-submission-review.component.css']
})
export class TestSubmissionReviewComponent implements OnInit {
  public submission: TestWithSubmissionVM;

  constructor(route: ActivatedRoute, testSubmissionService: TestSubmissionService) {
    const submissionId = ActivatedRouteUtils.getIdParam(route);
    testSubmissionService.getSubmissionById(submissionId).subscribe(submission => {
      this.submission = submission;
    });
  }

  ngOnInit() {
  }

  public isCorrect(answer: SubmissionAnswerWithCorrectAnswerVM): boolean {
    return answer.answerText === answer.correctAnswer;
  }

  public getReceivedPoints(testSubmission: TestWithSubmissionVM): number {
    return ArrayUtils.sum(testSubmission.answers.map(answer => answer.receivedPoints));
  }

  public getMaximalPoints(testSubmission: TestWithSubmissionVM): number {
    return ArrayUtils.sum(testSubmission.answers.map(answer => answer.maximalPoints));
  }

  public getPercentualScore(testSubmission: TestWithSubmissionVM): number {
    const percentualScore = this.getReceivedPoints(testSubmission) / this.getMaximalPoints(testSubmission) * 100;
    return Math.round(percentualScore * 100) / 100; // round to 2 decimal places
  }
}
