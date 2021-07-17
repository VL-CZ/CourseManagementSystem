import {Component, Input, OnInit} from '@angular/core';
import {CourseMemberOrAdminVM} from '../../viewmodels/courseMemberOrAdminVM';
import {RoleAuthService} from '../../services/role-auth.service';
import {CourseService} from '../../services/course.service';
import {CourseMemberService} from '../../services/course-member.service';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ActivatedRoute, Router} from '@angular/router';
import {ConfirmDialogManager} from '../../tools/dialog-managers/confirmDialogManager';

/**
 * component representing list of students
 */
@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {

  /**
   * id of the course
   */
  @Input()
  public courseId: string;

  /**
   * list of students
   */
  public students: CourseMemberOrAdminVM[] = [];

  private readonly courseService: CourseService;
  private readonly courseMemberService: CourseMemberService;
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private confirmDialogManager: ConfirmDialogManager;

  constructor(roleAuthService: RoleAuthService, courseService: CourseService, courseMemberService: CourseMemberService,
              bsModalService: BsModalService) {
    this.courseService = courseService;
    this.courseMemberService = courseMemberService;
    this.bsModalService = bsModalService;
    this.confirmDialogManager = new ConfirmDialogManager(this.bsModalRef, this.bsModalService);
  }

  ngOnInit() {
    this.reloadData();
  }

  /**
   * remove the selected course member
   * @param courseMember course member to remove
   */
  public removeMember(courseMember: CourseMemberOrAdminVM) {
    this.confirmDialogManager.displayDialog(
      'Remove a member',
      'Are you sure you want to remove the selected member?',
      () => {
        this.courseMemberService.removeById(courseMember.id).subscribe(() => {
          this.reloadData();
        });
      });
  }

  /**
   * reload student list
   * @private
   */
  private reloadData(): void {
    this.courseService.getAllMembers(this.courseId).subscribe(result => {
      this.students = result;
    });
  }
}
