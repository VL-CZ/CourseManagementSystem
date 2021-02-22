import {Component, Input, OnInit} from '@angular/core';
import {CourseTest} from '../viewmodels/courseTest';
import {CourseService} from '../course.service';
import {RoleAuthService} from '../role-auth.service';
import {CourseTestService} from '../course-test.service';

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.css']
})
export class TestListComponent implements OnInit {
  @Input()
  public courseId: string;

  private courseService: CourseService;
  private courseTestService: CourseTestService;

  public tests: CourseTest[];
  public isAdmin: boolean;

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

  public deleteTest(testId: number) {
    this.courseTestService.delete(testId.toString()).subscribe();
    this.tests = this.tests.filter(test => test.id !== testId);
  }
}
