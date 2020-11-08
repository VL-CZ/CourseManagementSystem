import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseService} from '../course.service';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  private readonly courseService: CourseService;

  public courseId: string;

  constructor(route: ActivatedRoute, courseService: CourseService) {
    this.courseId = route.snapshot.paramMap.get('id');
    this.courseService = courseService;
  }

  ngOnInit() {
  }

}
