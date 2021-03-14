import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseTestService} from '../course-test.service';
import {TestSubmissionVM} from '../viewmodels/testSubmissionVM';
import {TestSubmissionService} from '../test-submission.service';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';

@Component({
  selector: 'app-test-submit',
  templateUrl: './test-submit.component.html',
  styleUrls: ['./test-submit.component.css']
})
export class TestSubmitComponent implements OnInit {

  private router: Router;
  private testSubmitService: TestSubmissionService;
  public testSubmission: TestSubmissionVM;

  constructor(route: ActivatedRoute, courseTestService: CourseTestService, testSubmitService: TestSubmissionService, router: Router) {
    const testId = ActivatedRouteUtils.getIdParam(route);
    this.testSubmitService = testSubmitService;
    this.router = router;

    testSubmitService.getEmptySubmission(testId).subscribe(submission => {
      this.testSubmission = submission;
    });
  }

  ngOnInit() {
  }

  public submit() {
    this.testSubmitService.submit(this.testSubmission).subscribe(
      id => this.router.navigate(['/submissions', id]));
  }
}
