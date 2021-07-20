import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseMemberService} from '../../services/course-member.service';
import {RoleAuthService} from '../../services/role-auth.service';
import {ActivatedRouteTools} from '../../tools/activatedRouteTools';
import {CourseMemberOrAdminVM} from '../../viewmodels/courseMemberOrAdminVM';

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
   * id of the course that this course member belongs to
   */
  public courseId: string;

  /**
   * current course member
   */
  public courseMember: CourseMemberOrAdminVM = new CourseMemberOrAdminVM();

  constructor(route: ActivatedRoute, courseMemberService: CourseMemberService, roleAuthService: RoleAuthService) {
    this.courseMemberId = ActivatedRouteTools.getIdParam(route);
    courseMemberService.getById(this.courseMemberId).subscribe(cm => {
      this.courseMember = cm;
    });

    courseMemberService.getCourseId(this.courseMemberId).subscribe(result => {
      this.courseId = result.value;
    });

    roleAuthService.isCourseMemberAdmin(this.courseMemberId).subscribe(result => {
      this.isCourseAdmin = result.value;
    });
  }

  ngOnInit() {
  }
}
