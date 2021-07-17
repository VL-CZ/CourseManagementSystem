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

  /**
   * navigate to home-page
   */
  public navigateHome(): void {
    this.router.navigate(['/']);
  }

  /**
   * navigate to student detail page
   * @param studentId identifier of the student
   */
  public navigateToStudentDetail(studentId: string): void {
    this.router.navigate(['/students', studentId]);
  }

  /**
   * navigate to page that contains list of courses
   */
  public navigateToCourseList(): void {
    this.router.navigate(['/courses']);
  }

  /**
   * navigate to details of the course
   * @param courseId identifier of the course
   */
  public navigateToCourseDetail(courseId: string): void {
    this.router.navigate(['/courses', courseId]);
  }

  /**
   * navigate to the list of enrollment requests related to the given course
   * @param courseId identifier of the course
   */
  public navigateToCourseEnrollmentRequests(courseId: string): void {
    this.router.navigate(['/courses', courseId, 'enrollmentRequests']);
  }

  /**
   * navigate to assignment detail
   * @param assignmentId identifier of the assignment
   */
  public navigateToAssignmentDetail(assignmentId: string): void {
    this.router.navigate(['/tests', assignmentId]);
  }

  /**
   * navigate to assignment edit page
   * @param assignmentId identifier of the assigment
   */
  public navigateToAssignmentEditation(assignmentId: string): void {
    this.router.navigate(['/tests', 'edit', assignmentId]);
  }

  /**
   * navigate to assignment create page
   * @param assignmentId identifier of the assigment
   */
  public navigateToAssignmentCreation(assignmentId: string): void {
    this.router.navigate(['/tests', 'create', assignmentId]);
  }

  /**
   * navigate to assignemnt submission page
   * @param assignmentId identifier of the assigment
   */
  public navigateToAssignmentSubmission(assignmentId: string): void {
    this.router.navigate(['/tests', 'submit', assignmentId]);
  }

  /**
   * navigate to assignment submission review page
   * @param submissionId identifier of the assigment
   */
  public navigateToSubmissionReview(submissionId: string): void {
    this.router.navigate(['/submissions', submissionId]);
  }
}
