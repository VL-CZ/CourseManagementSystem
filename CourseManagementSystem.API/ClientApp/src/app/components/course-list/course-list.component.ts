import {Component, OnInit} from '@angular/core';
import {CourseInfoVM} from '../../viewmodels/courseVM';
import {CourseService} from '../../services/course.service';
import {RoleAuthService} from '../../services/role-auth.service';
import {AddCourseVM} from '../../viewmodels/courseVM';
import {PeopleService} from '../../services/people.service';

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
  public newCourse: AddCourseVM;

  /**
   * list of courses where the current user is a member
   */
  public memberCourses: CourseInfoVM[];

  /**
   * list of courses managed by the currently logged user
   */
  public managedCourses: CourseInfoVM[];

  private readonly courseService: CourseService;
  private readonly peopleService: PeopleService;

  constructor(courseService: CourseService, peopleService: PeopleService, roleAuthService: RoleAuthService) {
    this.newCourse = new AddCourseVM();
    this.managedCourses = [];
    this.memberCourses = [];
    this.courseService = courseService;
    this.peopleService = peopleService;

    this.reloadCourseInfo();
  }

  ngOnInit() {
  }

  /**
   * remove course with the given id
   * @param courseId identifier of the course to delete
   */
  public removeCourse(courseId: string): void {
    this.courseService.delete(courseId).subscribe(() => {
      this.reloadCourseInfo();
    });
  }

  /**
   * add a new course
   */
  public addCourse(): void {
    this.courseService.create(this.newCourse).subscribe(() => {
      this.reloadCourseInfo();
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
