import {Component, Input, OnInit} from '@angular/core';
import {Person, Student} from '../viewmodels/student';
import { CourseMemberService } from '../course-member.service';
import {RoleAuthService} from '../role-auth.service';
import {CourseService} from '../course.service';

@Component({
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {

  @Input()
  private courseId: string;

  public people: Person[];
  public isAdmin: boolean;

  constructor(roleAuthService: RoleAuthService, courseService: CourseService) {
    courseService.getAllMembers(this.courseId).subscribe(result => {
      this.people = result;
    });

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });
  }

  ngOnInit() {
  }
}
