import {ActivatedRoute, Router} from '@angular/router';

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
  public reloadCurrentPage(activatedRoute: ActivatedRoute, scrollToTop: boolean = false): void {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate(['./'], {
      relativeTo: activatedRoute,
      queryParamsHandling: 'preserve'
    }).then(_ => {
      if (scrollToTop) {
        this.scrollToTop();
      }
    });
  }

  // navigation methods

  /**
   * navigate to home-page
   */
  public navigateHome(): void {
    this.router.navigate(['/']).then(this.scrollToTop);
  }

  /**
   * navigate to student detail page
   * @param studentId identifier of the student
   */
  public navigateToStudentDetail(studentId: string): void {
    this.router.navigate(['/students', studentId]).then(this.scrollToTop);
  }

  /**
   * navigate to page that contains list of courses
   */
  public navigateToCourseList(): void {
    this.router.navigate(['/courses']).then(this.scrollToTop);
  }

  /**
   * navigate to details of the course
   * @param courseId identifier of the course
   */
  public navigateToCourseDetail(courseId: string): void {
    this.router.navigate(['/courses', courseId]).then(this.scrollToTop);
  }

  /**
   * navigate to the list of enrollment requests related to the given course
   * @param courseId identifier of the course
   */
  public navigateToCourseEnrollmentRequests(courseId: string): void {
    this.router.navigate(['/courses', courseId, 'enrollmentRequests']).then(this.scrollToTop);
  }

  /**
   * navigate to assignment detail
   * @param assignmentId identifier of the assignment
   */
  public navigateToAssignmentDetail(assignmentId: string): void {
    this.router.navigate(['/tests', assignmentId]).then(this.scrollToTop);
  }

  /**
   * navigate to assignment edit page
   * @param assignmentId identifier of the assigment
   */
  public navigateToAssignmentEditation(assignmentId: string): void {
    this.router.navigate(['/tests', 'edit', assignmentId]).then(this.scrollToTop);
  }

  /**
   * navigate to assignment create page
   * @param assignmentId identifier of the assigment
   */
  public navigateToAssignmentCreation(assignmentId: string): void {
    this.router.navigate(['/tests', 'create', assignmentId]).then(this.scrollToTop);
  }

  /**
   * navigate to assignemnt submission page
   * @param assignmentId identifier of the assigment
   */
  public navigateToAssignmentSubmission(assignmentId: string): void {
    this.router.navigate(['/tests', 'submit', assignmentId]).then(this.scrollToTop);
  }

  /**
   * navigate to assignment submission review page
   * @param submissionId identifier of the assigment
   */
  public navigateToSubmissionReview(submissionId: string): void {
    this.router.navigate(['/submissions', submissionId]).then(this.scrollToTop);
  }

  /**
   * scroll to top of the page
   * @private
   */
  private scrollToTop(): void {
    window.scroll(0, 0);
  }
}
