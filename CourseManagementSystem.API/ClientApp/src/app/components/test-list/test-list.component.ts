import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {CourseTestDetailsVM, TestStatus} from '../../viewmodels/courseTestVM';
import {CourseService} from '../../services/course.service';
import {RoleAuthService} from '../../services/role-auth.service';
import {CourseTestService} from '../../services/course-test.service';
import {DateTimeFormatter} from '../../tools/datetime/dateTimeFormatter';
import {CourseTestTools} from '../../tools/courseTestTools';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ConfirmDialogManager} from '../../tools/dialog-managers/confirmDialogManager';
import {ConfirmButtonStyle} from '../confirm-dialog/confirm-dialog.component';
import {Router} from '@angular/router';
import {PageNavigator} from '../../tools/pageNavigator';

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
   * formatter of date-time values
   */
  public dateTimeFormatter: DateTimeFormatter = new DateTimeFormatter();

  /**
   * class for navigating between the pages
   */
  public readonly pageNavigator: PageNavigator;

  public courseTestUtils: CourseTestTools = new CourseTestTools();

  private courseService: CourseService;
  private courseTestService: CourseTestService;
  private roleAuthService: RoleAuthService;
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private confirmDialogManager: ConfirmDialogManager;

  constructor(courseService: CourseService, roleAuthService: RoleAuthService, courseTestService: CourseTestService,
              bsModalService: BsModalService, router: Router) {
    this.courseService = courseService;
    this.courseTestService = courseTestService;
    this.roleAuthService = roleAuthService;
    this.bsModalService = bsModalService;
    this.pageNavigator = new PageNavigator(router);
    this.confirmDialogManager = new ConfirmDialogManager(this.bsModalRef, this.bsModalService);
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
    this.confirmDialogManager.displayDialog(
      'Delete an assignment',
      'Are you sure you want to delete this assignment?',
      () => {
        this.courseTestService.delete(test.id).subscribe(() => {
          this.reloadTests();
        });
      });
  }

  /**
   * publish a test
   * @param test test to publish
   */
  public publishTest(test: CourseTestDetailsVM): void {
    this.confirmDialogManager.displayDialog(
      'Publish a test',
      'Are you sure you want to publish this test? Any member of the course will be able to see it.',
      () => {
        this.courseTestService.publishTest(test.id).subscribe(() => {
          this.reloadTests();
        });
      },
      ConfirmButtonStyle.Information
    );
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
