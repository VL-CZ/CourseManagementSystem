import {HttpClient} from '@angular/common/http';
import {Component, Inject, Input, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {PersonIdVM} from '../viewmodels/student';

@Component({
  selector: 'app-grade-list',
  templateUrl: './grade-list.component.html',
  styleUrls: ['./grade-list.component.css']
})
export class GradeListComponent implements OnInit {

  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, router: Router) {
    this.http = http;
    this.baseUrl = baseUrl;
    http.get<PersonIdVM>(this.baseUrl + 'api/students/getId').subscribe(result => {
      router.navigate(['students', result.id]);
    }, error => console.error(error));
  }

  ngOnInit() {
  }
}
