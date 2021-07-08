import {Component, Input, OnInit} from '@angular/core';
import {CourseMemberOrAdminVM} from '../../viewmodels/courseMemberOrAdminVM';
import {RoleAuthService} from '../../services/role-auth.service';
import {CourseService} from '../../services/course.service';
import {CourseMemberService} from '../../services/course-member.service';

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
  private readonly courseMemberService: CourseMemberService;

  constructor(roleAuthService: RoleAuthService, courseService: CourseService, courseMemberService: CourseMemberService) {
    this.courseService = courseService;
    this.courseMemberService = courseMemberService;
  }

  ngOnInit() {
    this.reloadData();
  }

  /**
   * remove the selected course member
   * @param courseMember course member to remove
   */
  public removeMember(courseMember: CourseMemberOrAdminVM) {
    this.courseMemberService.removeById(courseMember.id).subscribe(() => {
      this.reloadData();
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
