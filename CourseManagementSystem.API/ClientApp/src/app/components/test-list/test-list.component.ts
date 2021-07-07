import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {CourseTestDetailsVM, TestStatus} from '../../viewmodels/courseTestVM';
import {CourseService} from '../../services/course.service';
import {RoleAuthService} from '../../services/role-auth.service';
import {CourseTestService} from '../../services/course-test.service';
import {DateTimeFormatter} from '../../utils/dateTimeFormatter';
import {CourseTestUtils} from '../../utils/courseTestUtils';

/**
 * component representing list of tests in a course
 */
@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.css']
})
export class TestListComponent implements OnInit, OnChanges {

  /**
   * id of the course
   */
  @Input()
  public courseId: string;

  /**
   * is the current user admin of the course?
   */
  @Input()
  public isCourseAdmin: boolean;

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
   * formatter of date-time
   */
  public dateTimeFormatter: DateTimeFormatter = new DateTimeFormatter();

  public courseTestUtils: CourseTestUtils = new CourseTestUtils();

  private courseService: CourseService;
  private courseTestService: CourseTestService;
  private roleAuthService: RoleAuthService;

  constructor(courseService: CourseService, roleAuthService: RoleAuthService, courseTestService: CourseTestService) {
    this.courseService = courseService;
    this.courseTestService = courseTestService;
    this.roleAuthService = roleAuthService;
  }

  ngOnInit() {
      this.reloadTests();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.reloadTests();
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

    if (this.isCourseAdmin) {
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
