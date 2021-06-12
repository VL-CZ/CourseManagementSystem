import {Component, Input, OnInit} from '@angular/core';
import {CourseMemberOrAdminVM} from '../viewmodels/courseMemberOrAdminVM';
import {CourseService} from '../course.service';
import {RoleAuthService} from '../role-auth.service';
import {WrapperVM} from '../viewmodels/wrapperVM';
import {CourseAdminService} from '../course-admin.service';

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
  private readonly courseAdminService: CourseAdminService;

  constructor(courseService: CourseService, courseAdminService: CourseAdminService) {
    this.courseService = courseService;
    this.courseAdminService = courseAdminService;
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
   * remove selected admin by its id
   * @param adminId identifier of the admin to remove
   */
  public removeAdmin(adminId: string) {
    this.courseAdminService.removeById(adminId).subscribe(() => {
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
