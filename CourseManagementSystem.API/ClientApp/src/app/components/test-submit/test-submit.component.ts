import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseTestService} from '../../services/course-test.service';
import {SubmitTestVM} from '../../viewmodels/submitTestVM';
import {TestSubmissionService} from '../../services/test-submission.service';
import {ActivatedRouteUtils} from '../../utils/activatedRouteUtils';
import {CourseTestUtils} from '../../utils/courseTestUtils';

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

  public courseTestUtils: CourseTestUtils = new CourseTestUtils();

  private router: Router;
  private testSubmitService: TestSubmissionService;

  constructor(route: ActivatedRoute, courseTestService: CourseTestService, testSubmissionService: TestSubmissionService, router: Router) {
    const testId = ActivatedRouteUtils.getIdParam(route);
    this.testSubmitService = testSubmissionService;
    this.router = router;

    testSubmissionService.loadSubmission(testId).subscribe(submission => {
      this.testSubmission = submission;
      if (submission.isSubmitted) {
        this.navigateToSubmissionDetail();
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
    this.testSubmitService.saveAnswers(this.testSubmission.testSubmissionId, this.testSubmission.answers).subscribe(
      () => {
      }
    );
  }

  /**
   * submit the test
   */
  public submit(): void {
    // save answers
    this.testSubmitService.saveAnswers(this.testSubmission.testSubmissionId, this.testSubmission.answers).subscribe(() => {
      // submit the test
      this.testSubmitService.submit(this.testSubmission).subscribe(
        // navigate
        () => this.navigateToSubmissionDetail()
      );
    });
  }

  /**
   * navigate to submission detail page
   * @private
   */
  private navigateToSubmissionDetail(): void {
    this.router.navigate(['/submissions', this.testSubmission.testSubmissionId]);
  }
}
