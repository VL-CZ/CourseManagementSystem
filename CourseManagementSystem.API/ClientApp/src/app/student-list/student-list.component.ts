import { Component, OnInit } from '@angular/core';
import {Student} from '../viewmodels/student';
import { CourseMemberService } from '../course-member.service';
import {RoleAuthService} from '../role-auth.service';

@Component({
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {

  public people: Student[];
  public isAdmin: boolean;

  constructor(personService: CourseMemberService, roleAuthService: RoleAuthService) {
    personService.getAll().subscribe(result => {
      this.people = result;
    });

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });
  }

  ngOnInit() {
  }
}
