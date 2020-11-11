import {Inject, Injectable} from '@angular/core';
import {Person, Student} from './viewmodels/student';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AddGradeVM} from './viewmodels/addGradeVM';
import {Grade} from './viewmodels/grade';
import {ApiService} from './api.service';

@Injectable({
  providedIn: 'root'
})
export class CourseMemberService extends ApiService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  /**
   * get person by its id
   * @param personId
   */
  public getById(personId: string): Observable<Student> {
    return this.http.get<Student>(this.baseUrl + 'api/courseMembers/' + personId);
  }

  /**
   * assign the `grade` to the person whose id equals `personId`
   * @param personId
   * @param grade created grade (contains id)
   */
  public assignGrade(personId: string, grade: AddGradeVM): Observable<Grade> {
    return this.http.post<Grade>(this.baseUrl + 'api/courseMembers/' + personId + '/assignGrade', grade);
  }
}
