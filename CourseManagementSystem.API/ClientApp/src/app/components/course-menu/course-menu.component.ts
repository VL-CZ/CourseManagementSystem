import {Component, Input, OnInit} from '@angular/core';
import {CourseService} from '../../services/course.service';
import {CourseInfoVM} from '../../viewmodels/courseVM';

@Component({
  selector: 'app-course-menu',
  templateUrl: './course-menu.component.html',
  styleUrls: ['./course-menu.component.css']
})
export class CourseMenuComponent implements OnInit {

  @Input()
  private courseId: string;

  public courseInfo: CourseInfoVM = new CourseInfoVM();

  private courseService: CourseService;

  constructor(courseService: CourseService) {
    this.courseService = courseService;
  }

  ngOnInit() {
    this.courseService.getById(this.courseId).subscribe(res => {
      this.courseInfo = res;
    });
  }
}
