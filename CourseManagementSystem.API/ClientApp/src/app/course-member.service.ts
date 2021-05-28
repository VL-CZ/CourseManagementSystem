import {Inject, Injectable} from '@angular/core';
import {CourseMemberVM} from './viewmodels/courseMemberVM';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AddGradeVM} from './viewmodels/gradeVM';
import {GradeDetailsVM} from './viewmodels/gradeVM';
import {ApiService} from './api.service';
import {TestSubmissionInfoVM} from './viewmodels/testSubmissionVM';
import {WrapperVM} from './viewmodels/wrapperVM';
import {QuizSubmissionInfoVM} from './viewmodels/quizSubmissionInfoVM';

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
  public getById(courseMemberId: string): Observable<CourseMemberVM> {
    return this.httpGet<CourseMemberVM>(courseMemberId);
  }

  /**
   * assign the `grade` to the person whose id equals `courseMemberId`
   * @param courseMemberId
   * @param grade created grade (contains id)
   */
  public assignGrade(courseMemberId: string, grade: AddGradeVM): Observable<{}> {
    return this.httpPost(`${courseMemberId}/assignGrade`, grade);
  }

  /**
   * get all test submissions of the given course member
   * @param courseMemberId id of the course member
   */
  public getTestSubmissions(courseMemberId: string): Observable<TestSubmissionInfoVM[]> {
    return this.httpGet<TestSubmissionInfoVM[]>(`${courseMemberId}/submissions`);
  }

  /**
   * get all quiz submissions of the given course member
   * @param courseMemberId id of the course member
   */
  public getQuizSubmissions(courseMemberId: string): Observable<QuizSubmissionInfoVM[]> {
    return this.httpGet<QuizSubmissionInfoVM[]>(`${courseMemberId}/quizSubmissions`);
  }

  /**
   * get all grades (except test submissions) of the given course member
   * @param courseMemberId id of the course member
   */
  public getGrades(courseMemberId: string): Observable<GradeDetailsVM[]> {
    return this.httpGet<GradeDetailsVM[]>(`${courseMemberId}/grades`);
  }

  /**
   * get average score (from tests and additional grades) of the given course member
   * @param courseMemberId id of the course member
   */
  public getAverageScore(courseMemberId: string): Observable<WrapperVM<number>> {
    return this.httpGet<WrapperVM<number>>(`${courseMemberId}/averageScore`);
  }
}
