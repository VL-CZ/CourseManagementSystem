import {Component, OnInit} from '@angular/core';
import {CourseInfoVM} from '../../viewmodels/courseVM';
import {CourseService} from '../../services/course.service';
import {RoleAuthService} from '../../services/role-auth.service';
import {AddCourseVM} from '../../viewmodels/courseVM';
import {PeopleService} from '../../services/people.service';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ObservableWrapper} from '../../tools/observableWrapper';
import {ConfirmDialogManager} from '../../tools/dialog-managers/confirmDialogManager';

/**
 * component representing list of courses
 */
@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {

  /**
   * course to add
   */
  public newCourse: AddCourseVM = new AddCourseVM();

  /**
   * list of courses where the current user is a member
   */
  public memberCourses: CourseInfoVM[] = [];

  /**
   * list of courses managed by the currently logged user
   */
  public managedCourses: CourseInfoVM[] = [];

  /**
   * identifier of the current user
   */
  public currentUserId: string;

  private readonly courseService: CourseService;
  private readonly peopleService: PeopleService;
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private observableWrapper: ObservableWrapper;
  private confirmDialogManager: ConfirmDialogManager;

  constructor(courseService: CourseService, peopleService: PeopleService, roleAuthService: RoleAuthService,
              bsModalService: BsModalService) {
    this.courseService = courseService;
    this.peopleService = peopleService;
    this.bsModalService = bsModalService;

    this.observableWrapper = new ObservableWrapper(this.bsModalRef, this.bsModalService);
    this.confirmDialogManager = new ConfirmDialogManager(this.bsModalRef, this.bsModalService);

    roleAuthService.getCurrentUserId().subscribe(id => {
      this.currentUserId = id.value;
    });

    this.reloadCourseInfo();
  }

  ngOnInit() {
  }

  /**
   * remove course with the given id
   * @param courseId identifier of the course to delete
   */
  public removeCourse(courseId: string): void {
    this.confirmDialogManager.displayDialog(
      'Remove a course',
      'Are you sure you want to remove the selected course?',
      () => {
        this.courseService.delete(courseId).subscribe(() => {
          this.reloadCourseInfo();
        });
      });
  }

  /**
   * add a new course
   */
  public addCourse(): void {
    this.observableWrapper.subscribeOrShowError(
      this.courseService.create(this.newCourse),
      () => {
        this.reloadCourseInfo();
        this.newCourse = new AddCourseVM();
      });
  }

  /**
   * reload info about courses
   */
  private reloadCourseInfo(): void {
    this.peopleService.getMemberCourses().subscribe(result => {
      this.memberCourses = result;
    });

    this.peopleService.getManagedCourses().subscribe(result => {
      this.managedCourses = result;
    });
  }
}
