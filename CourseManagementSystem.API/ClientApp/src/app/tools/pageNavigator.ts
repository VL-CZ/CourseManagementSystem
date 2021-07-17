import {ActivatedRoute, Router} from '@angular/router';
import {TestDetailComponent} from '../components/test-detail/test-detail.component';
import {TestEditComponent} from '../components/test-edit/test-edit.component';
import {TestCreateComponent} from '../components/test-create/test-create.component';
import {TestSubmitComponent} from '../components/test-submit/test-submit.component';
import {TestSubmissionReviewComponent} from '../components/test-submission-review/test-submission-review.component';

/**
 * class containing additional methods for router
 */
export class PageNavigator {
  private readonly router: Router;

  constructor(router: Router) {
    this.router = router;
  }

  /**
   * reload current page
   * @param activatedRoute current URL route
   */
  public reloadCurrentPage(activatedRoute: ActivatedRoute): void {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate(['./'], {relativeTo: activatedRoute, queryParamsHandling: 'preserve'});
  }

  // navigation methods

  public navigateHome(): void {
    this.router.navigate(['/']);
  }

  public navigateToStudentDetail(studentId: string): void {
    this.router.navigate(['/students', studentId]);
  }

  public navigateToCourseList(): void {
    this.router.navigate(['/courses']);
  }

  public navigateToCourseDetail(courseId: string): void {
    this.router.navigate(['/courses', courseId]);
  }

  public navigateToCourseEnrollmentRequests(courseId: string): void {
    this.router.navigate(['/courses', courseId, 'enrollmentRequests']);
  }

  public navigateToAssignmentDetail(assignmentId: string): void {
    this.router.navigate(['/tests', assignmentId]);
  }

  public navigateToAssignmentEditation(assignmentId: string): void {
    this.router.navigate(['/tests', 'edit', assignmentId]);
  }

  public navigateToAssignmentCreation(assignmentId: string): void {
    this.router.navigate(['/tests', 'create', assignmentId]);
  }

  public navigateToAssignmentSubmission(assignmentId: string): void {
    this.router.navigate(['/tests', 'submit', assignmentId]);
  }

  public navigateToSubmissionReview(submissionId: string): void {
    this.router.navigate(['/submissions', submissionId]);
  }
}
