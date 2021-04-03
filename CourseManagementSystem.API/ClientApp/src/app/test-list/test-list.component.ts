import {Component, Input, OnInit} from '@angular/core';
import {CourseTestVM} from '../viewmodels/courseTestVM';
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
   * list of test in this course
   */
  public tests: CourseTestVM[] = [];

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
      this.isAdmin = result.isAdmin;
    });
  }

  ngOnInit() {
    this.courseService.getAllTests(this.courseId).subscribe(tests => {
      this.tests = tests;
    });
  }

  public deleteTest(testId: number): void {
    this.courseTestService.delete(testId.toString()).subscribe();
    this.tests = this.tests.filter(test => test.id !== testId);
  }
}
