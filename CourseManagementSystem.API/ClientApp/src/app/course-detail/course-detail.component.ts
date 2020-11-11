import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseService} from '../course.service';
import {RoleAuthService} from '../role-auth.service';
import {PeopleService} from '../people.service';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  private currentCourseMemberId: string;

  private readonly courseService: CourseService;

  public isAdmin: boolean;
  public courseId: string;

  constructor(route: ActivatedRoute, courseService: CourseService, roleAuthService: RoleAuthService, peopleService: PeopleService) {
    this.courseId = route.snapshot.paramMap.get('id');
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
