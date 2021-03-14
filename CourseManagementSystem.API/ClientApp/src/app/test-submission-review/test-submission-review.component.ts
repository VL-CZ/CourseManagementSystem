import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {TestSubmissionService} from '../test-submission.service';
import {TestSubmissionVM} from '../viewmodels/testSubmissionVM';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';

@Component({
  selector: 'app-test-submission-review',
  templateUrl: './test-submission-review.component.html',
  styleUrls: ['./test-submission-review.component.css']
})
export class TestSubmissionReviewComponent implements OnInit {
  public submission: TestSubmissionVM;

  constructor(route: ActivatedRoute, testSubmissionService: TestSubmissionService) {
    const submissionId = ActivatedRouteUtils.getIdParam(route);
    testSubmissionService.getSubmissionById(submissionId).subscribe(submission => {
      this.submission = submission;
    });
  }

  ngOnInit() {
  }
}
