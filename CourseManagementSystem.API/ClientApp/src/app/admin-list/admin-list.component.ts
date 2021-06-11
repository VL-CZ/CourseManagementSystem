import {Component, Input, OnInit} from '@angular/core';
import {CourseMemberOrAdminVM} from '../viewmodels/courseMemberOrAdminVM';
import {CourseService} from '../course.service';
import {RoleAuthService} from '../role-auth.service';
import {WrapperVM} from '../viewmodels/wrapperVM';

/**
 * component representing list of course admins
 */
@Component({
  selector: 'app-admin-list',
  templateUrl: './admin-list.component.html',
  styleUrls: ['./admin-list.component.css']
})
export class AdminListComponent implements OnInit {

  @Input()
  private courseId: string;

  /**
   * list of course admins
   */
  public admins: CourseMemberOrAdminVM[] = [];

  /**
   * identifier of the admin to add
   */
  public adminIdToAdd: WrapperVM<string> = new WrapperVM<string>();

  private readonly courseService: CourseService;

  constructor(courseService: CourseService) {
    this.courseService = courseService;
  }

  ngOnInit() {
    this.reload();
  }

  /**
   * add new admin
   */
  public addAdmin(): void {
    this.courseService.addAdmin(this.courseId, this.adminIdToAdd).subscribe(() => {
      this.reload();
    });
  }

  /**
   * reload admin data
   * @private
   */
  private reload(): void {
    this.courseService.getAllAdmins(this.courseId).subscribe(result => {
      this.admins = result;
    });
  }
}
