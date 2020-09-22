import { HttpClient } from '@angular/common/http';
import {Component, Inject, Input, OnInit} from '@angular/core';
import {Grade} from '../viewmodels/grade';

@Component({
  selector: 'app-grade-list',
  templateUrl: './grade-list.component.html',
  styleUrls: ['./grade-list.component.css']
})
export class GradeListComponent implements OnInit {

  private http: HttpClient;
  private baseUrl: string;

  @Input()
  public grades: Grade[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;

  }

  ngOnInit() {
  }

  public removeGrade(gradeID: number): void {
    this.http.delete(this.baseUrl + 'api/grades/delete/' + gradeID).subscribe();
    this.grades = this.grades.filter(g => g.id !== gradeID);
  }
}
