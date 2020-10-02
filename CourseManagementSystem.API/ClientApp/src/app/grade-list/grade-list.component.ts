import {HttpClient} from '@angular/common/http';
import {Component, Inject, Input, OnInit} from '@angular/core';
import {IdVM} from '../viewmodels/idVM';
import {Router} from '@angular/router';

@Component({
  selector: 'app-grade-list',
  templateUrl: './grade-list.component.html',
  styleUrls: ['./grade-list.component.css']
})
export class GradeListComponent implements OnInit {

  private http: HttpClient;
  private baseUrl: string;
  private userId: Object;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, router: Router) {
    this.http = http;
    this.baseUrl = baseUrl;
    http.get<IdVM>(this.baseUrl + 'api/students/getId').subscribe(result => {
      router.navigate(['students', result.id]);
    }, error => console.error(error));
  }

  ngOnInit() {
  }
}
