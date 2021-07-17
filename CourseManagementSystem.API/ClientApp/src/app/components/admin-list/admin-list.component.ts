import {Component, Input, OnInit} from '@angular/core';
import {CourseMemberOrAdminVM} from '../../viewmodels/courseMemberOrAdminVM';
import {CourseService} from '../../services/course.service';
import {RoleAuthService} from '../../services/role-auth.service';
import {WrapperVM} from '../../viewmodels/wrapperVM';
import {CourseAdminService} from '../../services/course-admin.service';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ObservableWrapper} from '../../utils/observableWrapper';

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
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private observableWrapper: ObservableWrapper;

  constructor(courseService: CourseService, courseAdminService: CourseAdminService, bsModalService: BsModalService) {
    this.courseService = courseService;
    this.courseAdminService = courseAdminService;
    this.bsModalService = bsModalService;
    this.observableWrapper = new ObservableWrapper(this.bsModalRef, this.bsModalService);
  }

  ngOnInit() {
    this.reload();
  }

  /**
   * add new admin
   */
  public addAdmin(): void {
    this.observableWrapper.subscribeOrShowError(
      this.courseService.addAdmin(this.courseId, this.adminIdToAdd),
      () => {
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
    this.adminIdToAdd = new WrapperVM<string>();
    this.courseService.getAllAdmins(this.courseId).subscribe(result => {
      this.admins = result;
    });
  }
}
