import {Component, Input, OnInit} from '@angular/core';
import {CourseMemberVM} from '../viewmodels/courseMemberVM';
import {RoleAuthService} from '../role-auth.service';
import {CourseService} from '../course.service';

/**
 * component representing list of students
 */
@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {

  @Input()
  private courseId: string;

  /**
   * list of students
   */
  public students: CourseMemberVM[] = [];

  /**
   * is the current user admin?
   */
  public isAdmin: boolean;

  private readonly courseService: CourseService;

  constructor(roleAuthService: RoleAuthService, courseService: CourseService) {
    this.courseService = courseService;

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.value;
    });
  }

  ngOnInit() {
    this.courseService.getAllMembers(this.courseId).subscribe(result => {
      this.students = result;
    });
  }
}
