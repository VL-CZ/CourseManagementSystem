import {Component, Input, OnInit} from '@angular/core';
import {CourseTestDetailsVM, TestStatus} from '../viewmodels/courseTestVM';
import {CourseService} from '../course.service';
import {RoleAuthService} from '../role-auth.service';
import {CourseTestService} from '../course-test.service';

/**
 * component representing list of tests in a course
 */
@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.css']
})
export class TestListComponent implements OnInit {

  /**
   * id of the course
   */
  @Input()
  public courseId: string;

  /**
   * active tests in this course
   */
  public activeTests: CourseTestDetailsVM[] = [];

  /**
   * tests that haven't been published yet in this course
   */
  public nonPublishedTests: CourseTestDetailsVM[] = [];

  /**
   * tests after deadline in this course
   */
  public testsAfterDeadline: CourseTestDetailsVM[] = [];

  /**
   * is the current user admin?
   */
  public isAdmin: boolean;

  private courseService: CourseService;
  private courseTestService: CourseTestService;
  private roleAuthService: RoleAuthService;

  constructor(courseService: CourseService, roleAuthService: RoleAuthService, courseTestService: CourseTestService) {
    this.courseService = courseService;
    this.courseTestService = courseTestService;
    this.roleAuthService = roleAuthService;
  }

  ngOnInit() {
    this.roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.value;
      this.reloadTests();
    });
  }

  /**
   * delete a test
   * @param test test to delete
   */
  public deleteTest(test: CourseTestDetailsVM): void {
    this.courseTestService.delete(test.id.toString()).subscribe(() => {
      this.reloadTests();
    });
  }

  /**
   * publish a test
   * @param test test to publish
   */
  public publishTest(test: CourseTestDetailsVM): void {
    this.courseTestService.publishTest(test.id.toString()).subscribe(() => {
      this.reloadTests();
    });
  }

  /**
   * check if the test has already been published
   * @param test given test
   */
  public isPublished(test: CourseTestDetailsVM): boolean {
    return test.status === TestStatus.Published;
  }

  /**
   * reload the tests (set `tests` variable)
   * @private
   */
  private reloadTests(): void {

    // get active tests
    this.courseService.getActiveTests(this.courseId).subscribe(tests => {
      this.activeTests = tests;
    });

    if (this.isAdmin) {
      // get non-published tests
      this.courseService.getNonPublishedTests(this.courseId).subscribe(tests => {
        this.nonPublishedTests = tests;
      });

      // get tests after deadline
      this.courseService.getTestsAfterDeadline(this.courseId).subscribe(tests => {
        this.testsAfterDeadline = tests;
      });
    }
  }
}
