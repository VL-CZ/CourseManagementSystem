import {Component, OnInit} from '@angular/core';
import {CourseInfoVM} from '../viewmodels/courseInfoVM';
import {CourseService} from '../course.service';
import {RoleAuthService} from '../role-auth.service';
import {AddCourseVM} from '../viewmodels/addCourseVM';
import {PeopleService} from '../people.service';

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

  /**
   * is the current user admin?
   */
  public isAdmin: boolean;

  private readonly courseService: CourseService;
  private currentUserId: string;

  constructor(courseService: CourseService, peopleService: PeopleService, roleAuthService: RoleAuthService) {
    this.newCourse = new AddCourseVM();
    this.newCourse.adminId = '';
    this.managedCourses = [];
    this.memberCourses = [];
    this.courseService = courseService;

    peopleService.getMemberCourses().subscribe(result => {
      this.memberCourses = result;
    });

    peopleService.getManagedCourses().subscribe(result => {
      this.managedCourses = result;
    });

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });

    roleAuthService.getCurrentUserId().subscribe(result => {
      this.currentUserId = result.id;
    });
  }

  ngOnInit() {
  }

  /**
   * remove course with the given id
   * @param courseId identifier of the course to delete
   */
  public removeCourse(courseId: number): void {
    this.courseService.delete(courseId).subscribe();
    this.memberCourses = this.memberCourses.filter(c => c.id !== courseId);
    this.managedCourses = this.managedCourses.filter(c => c.id !== courseId);
  }

  /**
   * add a new course
   */
  public addCourse(): void {
    this.newCourse.adminId = this.currentUserId;
    this.courseService.create(this.newCourse).subscribe(result => {
      this.memberCourses.push(result);
    });
  }
}
