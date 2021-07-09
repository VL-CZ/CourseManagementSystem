import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseService} from '../../services/course.service';
import {RoleAuthService} from '../../services/role-auth.service';
import {PeopleService} from '../../services/people.service';
import {ActivatedRouteUtils} from '../../utils/activatedRouteUtils';
import {CourseInfoVM} from '../../viewmodels/courseVM';

/**
 * component representing details of the course
 */
@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  /**
   * identifier of the current course member
   */
  public currentCourseMemberId: string;

  /**
   * is the currently logged course member admin of the course?
   */
  public isCourseAdmin: boolean;

  /**
   * id of the course
   */
  public courseId: string;

  /**
   * info about this course
   */
  public courseInfo: CourseInfoVM = new CourseInfoVM();

  private readonly courseService: CourseService;

  constructor(route: ActivatedRoute, courseService: CourseService, roleAuthService: RoleAuthService, peopleService: PeopleService) {
    this.courseId = ActivatedRouteUtils.getIdParam(route);
    this.courseService = courseService;

    courseService.getById(this.courseId).subscribe(course => {
      this.courseInfo = course;
    });

    roleAuthService.isCourseAdmin(this.courseId).subscribe(result => {
      this.isCourseAdmin = result.value;

      if (!this.isCourseAdmin) {
        peopleService.getCourseMemberByCourse(this.courseId).subscribe(res => {
          this.currentCourseMemberId = res.value;
        });
      }
    });
  }

  ngOnInit() {
  }

}
