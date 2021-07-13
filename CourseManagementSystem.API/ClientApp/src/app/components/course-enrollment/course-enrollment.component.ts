import {Component, OnInit} from '@angular/core';
import {PeopleService} from '../../services/people.service';
import {CourseService} from '../../services/course.service';
import {ActivatedRoute, Router} from '@angular/router';
import {RouterUtils} from '../../utils/routerUtils';

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
  private router: Router;
  private activatedRoute: ActivatedRoute;

  constructor(courseService: CourseService, activatedRoute: ActivatedRoute, router: Router) {
    this.courseService = courseService;
    this.router = router;
    this.activatedRoute = activatedRoute;
  }

  ngOnInit() {
  }

  /**
   * enroll to a course
   */
  public enroll(): void {
    this.courseService.enrollTo(this.courseToEnrollId).subscribe(() => {
      RouterUtils.reloadPage(this.router, this.activatedRoute);
    });
  }
}
