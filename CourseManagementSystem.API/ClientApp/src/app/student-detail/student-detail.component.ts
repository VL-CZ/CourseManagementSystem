import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseMemberService} from '../course-member.service';
import {RoleAuthService} from '../role-auth.service';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';
import {CourseMemberOrAdminVM} from '../viewmodels/courseMemberOrAdminVM';

@Component({
  selector: 'app-student-detail',
  templateUrl: './student-detail.component.html',
  styleUrls: ['./student-detail.component.css']
})
export class StudentDetailComponent implements OnInit {
  /**
   * id of the current course member
   */
  public readonly courseMemberId: string;

  /**
   * is the current user admin of the course?
   */
  public isCourseAdmin: boolean;

  /**
   * current course member
   */
  public courseMember: CourseMemberOrAdminVM = new CourseMemberOrAdminVM();

  constructor(route: ActivatedRoute, courseMemberService: CourseMemberService, roleAuthService: RoleAuthService) {
    this.courseMemberId = ActivatedRouteUtils.getIdParam(route);
    courseMemberService.getById(this.courseMemberId).subscribe(cm => {
      this.courseMember = cm;
    });

    roleAuthService.isCourseMemberAdmin(this.courseMemberId).subscribe(result => {
      this.isCourseAdmin = result.value;
    });
  }

  ngOnInit() {
  }
}
