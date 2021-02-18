import {Component, Input, OnInit} from '@angular/core';
import {CourseTest} from '../viewmodels/courseTest';
import {CourseService} from '../course.service';

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.css']
})
export class TestListComponent implements OnInit {
  @Input()
  private courseId: string;

  private courseService: CourseService;
  public tests: CourseTest[];

  constructor(courseService: CourseService) {
    this.courseService = courseService;
  }

  ngOnInit() {
    this.courseService.getAllTests(this.courseId).subscribe(tests => {
      this.tests = tests;
    });
  }
}
