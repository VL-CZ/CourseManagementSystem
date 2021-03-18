import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseService} from '../course.service';
import {RoleAuthService} from '../role-auth.service';
import {PeopleService} from '../people.service';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  public currentCourseMemberId: string;

  private readonly courseService: CourseService;

  public isAdmin: boolean;
  public courseId: string;

  constructor(route: ActivatedRoute, courseService: CourseService, roleAuthService: RoleAuthService, peopleService: PeopleService) {
    this.courseId = ActivatedRouteUtils.getIdParam(route);
    this.courseService = courseService;

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;

      if (!this.isAdmin) {
        peopleService.getCourseMemberByCourse(parseInt(this.courseId, 10)).subscribe(res => {
          this.currentCourseMemberId = res;
        });
      }
    });
  }

  ngOnInit() {
  }

}
