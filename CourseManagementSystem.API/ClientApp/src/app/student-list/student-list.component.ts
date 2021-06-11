import {Component, Input, OnInit} from '@angular/core';
import {CourseMemberOrAdminVM} from '../viewmodels/courseMemberOrAdminVM';
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
  public students: CourseMemberOrAdminVM[] = [];

  private readonly courseService: CourseService;

  constructor(roleAuthService: RoleAuthService, courseService: CourseService) {
    this.courseService = courseService;
  }

  ngOnInit() {
    this.courseService.getAllMembers(this.courseId).subscribe(result => {
      this.students = result;
    });
  }
}
