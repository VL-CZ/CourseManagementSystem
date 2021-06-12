import {Component, OnInit} from '@angular/core';
import {PeopleService} from '../people.service';
import {CourseService} from '../course.service';

/**
 * component with enrollment to course
 */
@Component({
  selector: 'app-course-enrollment',
  templateUrl: './course-enrollment.component.html',
  styleUrls: ['./course-enrollment.component.css']
})
export class CourseEnrollmentComponent implements OnInit {

  /**
   * id of the course that we enroll to
   */
  public courseToEnrollId: string;

  private readonly courseService: CourseService;

  constructor(courseService: CourseService) {
    this.courseService = courseService;
  }

  ngOnInit() {
  }

  /**
   * enroll to a course
   */
  public enroll(): void {
    this.courseService.enrollTo(this.courseToEnrollId).subscribe(() => {
      console.log('Enrolled!\n');
    });
  }
}
