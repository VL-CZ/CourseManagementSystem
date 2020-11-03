import {Component, OnInit} from '@angular/core';
import {CourseInfoVM} from '../viewmodels/courseInfoVM';
import {CourseService} from '../course.service';
import {RoleAuthService} from '../role-auth.service';
import {AddCourseVM} from '../viewmodels/addCourseVM';
import {PeopleService} from '../people.service';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {

  private readonly courseService: CourseService;
  private currentUserId: string;

  public newCourse: AddCourseVM;
  public courses: CourseInfoVM[];
  public isAdmin: boolean;

  constructor(courseService: CourseService, peopleService: PeopleService, roleAuthService: RoleAuthService) {
    this.newCourse = new AddCourseVM();
    this.newCourse.adminId = '';
    this.courseService = courseService;

    peopleService.getCourses().subscribe(result => {
      this.courses = result;
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

  removeCourse(courseId: number): void {
    this.courseService.delete(courseId).subscribe();
    this.courses = this.courses.filter(c => c.id !== courseId);
  }

  addCourse(): void {
    this.courseService.create(this.newCourse).subscribe(result => {
      this.courses.push(result);
    });
  }
}
