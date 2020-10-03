import { Component, OnInit } from '@angular/core';
import {Student} from '../viewmodels/student';
import { PersonService } from '../person.service';
import {RoleAuthService} from '../role-auth.service';

@Component({
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {

  public people: Student[];
  public isAdmin: boolean;

  constructor(personService: PersonService, roleAuthService: RoleAuthService) {
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
