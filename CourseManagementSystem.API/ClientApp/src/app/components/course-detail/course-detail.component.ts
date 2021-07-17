import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseService} from '../../services/course.service';
import {RoleAuthService} from '../../services/role-auth.service';
import {PeopleService} from '../../services/people.service';
import {ActivatedRouteTools} from '../../tools/activatedRouteTools';
import {CourseInfoVM} from '../../viewmodels/courseVM';
import {CourseMemberService} from '../../services/course-member.service';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ConfirmDialogManager} from '../../tools/dialog-managers/confirmDialogManager';
import {PageNavigator} from '../../tools/pageNavigator';

/**
 * component representing details of the course
 */
@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  /**
   * identifier of the current course member
   */
  public currentCourseMemberId: string;

  /**
   * is the currently logged in user admin of the course?
   */
  public isCourseAdmin: boolean;

  /**
   * check if the currently logged in user is the only admin of this course
   */
  public isTheOnlyAdmin: boolean;

  /**
   * id of the course
   */
  public courseId: string;

  /**
   * info about this course
   */
  public courseInfo: CourseInfoVM = new CourseInfoVM();

  private readonly courseService: CourseService;
  private readonly courseMemberService: CourseMemberService;
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private confirmDialogManager: ConfirmDialogManager;
  private readonly pageNavigator: PageNavigator;

  constructor(route: ActivatedRoute, router: Router, courseService: CourseService, roleAuthService: RoleAuthService,
              peopleService: PeopleService, courseMemberService: CourseMemberService, bsModalService: BsModalService) {
    this.courseId = ActivatedRouteTools.getIdParam(route);
    this.courseService = courseService;
    this.courseMemberService = courseMemberService;
    this.pageNavigator = new PageNavigator(router);
    this.bsModalService = bsModalService;
    this.confirmDialogManager = new ConfirmDialogManager(this.bsModalRef, this.bsModalService);

    courseService.getById(this.courseId).subscribe(course => {
      this.courseInfo = course;
    });

    roleAuthService.isCourseAdmin(this.courseId).subscribe(result => {
      this.isCourseAdmin = result.value;

      if (this.isCourseAdmin) {
        courseService.getAllAdmins(this.courseId).subscribe(admins => {
          this.isTheOnlyAdmin = admins.length === 1;
        });
      } else {
        peopleService.getCourseMemberByCourse(this.courseId).subscribe(res => {
          this.currentCourseMemberId = res.value;
        });
      }
    });
  }

  ngOnInit() {
  }

  /**
   * remove the current admin from the course
   */
  public removeCurrentAdmin(): void {
    this.confirmDialogManager.displayDialog(
      'Leave this course',
      'Are you sure you want to leave this course?',
      () => {
        this.courseService.removeCurrentAdmin(this.courseId).subscribe(() => {
          this.pageNavigator.navigateHome();
        });
      });
  }

  /**
   * remove the current course member from the course
   */
  public removeCurrentMember(): void {
    this.confirmDialogManager.displayDialog(
      'Leave this course',
      'Are you sure you want to leave this course?',
      () => {
        this.courseService.removeCurrentMember(this.courseId).subscribe(() => {
          this.pageNavigator.navigateHome();
        });
      });
  }
}
