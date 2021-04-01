import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseMemberService} from '../course-member.service';
import {RoleAuthService} from '../role-auth.service';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';
import {CourseMemberVM} from '../viewmodels/courseMemberVM';

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
   * is the current course member admin?
   */
  public isAdmin: boolean;

  /**
   * current course member
   */
  public courseMember: CourseMemberVM = new CourseMemberVM();

  constructor(route: ActivatedRoute, courseMemberService: CourseMemberService, roleAuthService: RoleAuthService) {
    this.courseMemberId = ActivatedRouteUtils.getIdParam(route);
    courseMemberService.getById(this.courseMemberId).subscribe(cm => {
      this.courseMember = cm;
    });

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });
  }

  ngOnInit() {
  }
}
