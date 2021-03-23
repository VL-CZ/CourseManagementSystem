import {Inject, Injectable} from '@angular/core';
import {Student} from './viewmodels/student';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AddGradeVM} from './viewmodels/addGradeVM';
import {GradeDetailsVM} from './viewmodels/gradeDetailsVM';
import {ApiService} from './api.service';
import {TestSubmissionInfoVM} from './viewmodels/testSubmisionInfoVM';

@Injectable({
  providedIn: 'root'
})
export class CourseMemberService extends ApiService {
  private static controllerName = 'courseMembers';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl, CourseMemberService.controllerName);
  }

  /**
   * get course member by its id
   * @param courseMemberId id of the course member
   */
  public getById(courseMemberId: string): Observable<Student> {
    return this.http.get<Student>(this.controllerUrl + courseMemberId);
  }

  /**
   * assign the `grade` to the person whose id equals `courseMemberId`
   * @param courseMemberId
   * @param grade created grade (contains id)
   */
  public assignGrade(courseMemberId: string, grade: AddGradeVM): Observable<GradeDetailsVM> {
    return this.http.post<GradeDetailsVM>(this.controllerUrl + `${courseMemberId}/assignGrade`, grade);
  }

  /**
   * get all test submissions of the given course member
   * @param courseMemberId id of the course member
   */
  public getTestSubmissions(courseMemberId: string): Observable<TestSubmissionInfoVM[]> {
    return this.http.get<TestSubmissionInfoVM[]>(this.controllerUrl + `${courseMemberId}/submissions`);
  }
}
