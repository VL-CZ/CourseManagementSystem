import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseTestService} from '../../services/course-test.service';
import {SubmitTestVM} from '../../viewmodels/submitTestVM';
import {TestSubmissionService} from '../../services/test-submission.service';
import {ActivatedRouteUtils} from '../../utils/activatedRouteUtils';
import {CourseTestUtils} from '../../utils/courseTestUtils';
import {DateTimeFormatter} from '../../utils/dateTimeFormatter';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ObservableWrapper} from '../../utils/observableWrapper';

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

  public courseTestUtils: CourseTestUtils = new CourseTestUtils();

  private router: Router;
  private testSubmitService: TestSubmissionService;
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private observableWrapper: ObservableWrapper;

  constructor(route: ActivatedRoute, courseTestService: CourseTestService, testSubmissionService: TestSubmissionService, router: Router,
              bsModalService: BsModalService) {
    const testId = ActivatedRouteUtils.getIdParam(route);
    this.testSubmitService = testSubmissionService;
    this.router = router;
    this.bsModalService = bsModalService;
    this.observableWrapper = new ObservableWrapper(this.bsModalRef, this.bsModalService);

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
    this.observableWrapper.subscribeOrShowError(
      this.testSubmitService.saveAnswers(this.testSubmission.testSubmissionId, this.testSubmission.answers),
      () => {
      }
    );
  }

  /**
   * submit the test
   */
  public submit(): void {
    this.observableWrapper.subscribeOrShowError(
      // save answers
      this.testSubmitService.saveAnswers(this.testSubmission.testSubmissionId, this.testSubmission.answers),
      () => {
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
