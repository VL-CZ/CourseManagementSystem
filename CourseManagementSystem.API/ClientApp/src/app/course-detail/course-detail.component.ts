import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseService} from '../course.service';
import {RoleAuthService} from '../role-auth.service';
import {PeopleService} from '../people.service';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';

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
  public isAdmin: boolean;

  /**
   * id of the course
   */
  public courseId: string;

  private readonly courseService: CourseService;

  constructor(route: ActivatedRoute, courseService: CourseService, roleAuthService: RoleAuthService, peopleService: PeopleService) {
    this.courseId = ActivatedRouteUtils.getIdParam(route);
    this.courseService = courseService;

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;

      if (!this.isAdmin) {
        peopleService.getCourseMemberByCourse(this.courseId).subscribe(res => {
          this.currentCourseMemberId = res;
        });
      }
    });
  }

  ngOnInit() {
  }

}
