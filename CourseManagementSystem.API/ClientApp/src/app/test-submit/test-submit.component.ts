import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseTestService} from '../course-test.service';
import {SubmitTestVM} from '../viewmodels/submitTestVM';
import {TestSubmissionService} from '../test-submission.service';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';

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

  private router: Router;
  private testSubmitService: TestSubmissionService;

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

  /**
   * submit the test
   */
  public submit(): void {
    this.testSubmitService.submit(this.testSubmission).subscribe(
      id => this.router.navigate(['/submissions', id]));
  }
}
