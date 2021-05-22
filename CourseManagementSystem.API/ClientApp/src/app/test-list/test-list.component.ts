import {Component, Input, OnInit} from '@angular/core';
import {CourseTestDetailsVM, TestStatus} from '../viewmodels/courseTestVM';
import {CourseService} from '../course.service';
import {RoleAuthService} from '../role-auth.service';
import {CourseTestService} from '../course-test.service';
import {DateTimeFormatter} from '../utils/dateTimeFormatter';

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
   * list of test in this course
   */
  public tests: CourseTestDetailsVM[] = [];

  /**
   * formatter of date-time
   */
  public dateTimeFormatter: DateTimeFormatter = new DateTimeFormatter();

  /**
   * is the current user admin?
   */
  public isAdmin: boolean;

  private courseService: CourseService;
  private courseTestService: CourseTestService;

  constructor(courseService: CourseService, roleAuthService: RoleAuthService, courseTestService: CourseTestService) {
    this.courseService = courseService;
    this.courseTestService = courseTestService;

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.value;
    });
  }

  ngOnInit() {
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
    this.courseService.getAllTests(this.courseId).subscribe(tests => {
      this.tests = tests;
    });
  }
}
