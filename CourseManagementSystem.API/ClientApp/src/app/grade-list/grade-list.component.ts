import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {RoleAuthService} from '../role-auth.service';

@Component({
  selector: 'app-grade-list',
  templateUrl: './grade-list.component.html',
  styleUrls: ['./grade-list.component.css']
})
export class GradeListComponent implements OnInit {

  constructor(router: Router, roleAuthService: RoleAuthService) {
    roleAuthService.getCurrentUserId().subscribe(result => {
      router.navigate(['students', result.id]);
    });
  }

  ngOnInit() {
  }
}
