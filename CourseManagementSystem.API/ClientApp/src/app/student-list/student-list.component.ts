import {Component, Input, OnInit} from '@angular/core';
import {CourseMemberVM} from '../viewmodels/courseMemberVM';
import {RoleAuthService} from '../role-auth.service';
import {CourseService} from '../course.service';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {

  @Input()
  private courseId: string;

  private readonly courseService: CourseService;

  public people: CourseMemberVM[];
  public isAdmin: boolean;

  constructor(roleAuthService: RoleAuthService, courseService: CourseService) {
    this.courseService = courseService;

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });
  }

  ngOnInit() {
    this.courseService.getAllMembers(this.courseId).subscribe(result => {
      this.people = result;
    });
  }
}
