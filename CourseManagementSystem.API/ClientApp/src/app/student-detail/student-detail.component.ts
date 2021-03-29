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
  public readonly userId: string;
  public isAdmin: boolean;
  public courseMemberVM: CourseMemberVM = new CourseMemberVM();

  constructor(route: ActivatedRoute, courseMemberService: CourseMemberService, roleAuthService: RoleAuthService) {
    this.userId = ActivatedRouteUtils.getIdParam(route);
    courseMemberService.getById(this.userId).subscribe(cm => {
      this.courseMemberVM = cm;
    });

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });
  }

  ngOnInit() {
  }
}
