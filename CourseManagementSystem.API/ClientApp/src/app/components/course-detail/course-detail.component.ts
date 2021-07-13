import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseService} from '../../services/course.service';
import {RoleAuthService} from '../../services/role-auth.service';
import {PeopleService} from '../../services/people.service';
import {ActivatedRouteUtils} from '../../utils/activatedRouteUtils';
import {CourseInfoVM} from '../../viewmodels/courseVM';
import {CourseMemberService} from '../../services/course-member.service';

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
  private readonly courseMemberService: CourseMemberService;
  private router: Router;

  constructor(route: ActivatedRoute, router: Router, courseService: CourseService, roleAuthService: RoleAuthService,
              peopleService: PeopleService, courseMemberService: CourseMemberService) {
    this.courseId = ActivatedRouteUtils.getIdParam(route);
    this.courseService = courseService;
    this.courseMemberService = courseMemberService;
    this.router = router;

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

  /**
   * remove the current admin from the course
   */
  public removeCurrentAdmin(): void {
    this.courseService.removeCurrentAdmin(this.courseId).subscribe(() => {
      this.router.navigate(['/']);
    });
  }

  /**
   * remove the current course member from the course
   */
  public removeCurrentMember(): void {
    this.courseService.removeCurrentMember(this.courseId).subscribe(() => {
      this.router.navigate(['/']);
    });
  }
}
