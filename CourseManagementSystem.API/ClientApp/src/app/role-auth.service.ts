import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ApiService} from './api.service';
import {Observable} from 'rxjs';
import {WrapperVM} from './viewmodels/wrapperVM';

@Injectable({
  providedIn: 'root'
})
export class RoleAuthService extends ApiService {
  private static controllerName = 'auth';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl, RoleAuthService.controllerName);
  }

  /**
   * is the currently logged-in user admin of the selected course?
   * @param courseId identifier of the course
   */
  public isCourseAdmin(courseId: string): Observable<WrapperVM<boolean>> {
    return this.httpGet<WrapperVM<boolean>>(`isCourseAdmin/${courseId}`);
  }

  /**
   * is the currently logged-in user admin of the selected course member?
   * @param courseMemberId identifier of the course member
   */
  public isCourseMemberAdmin(courseMemberId: string): Observable<WrapperVM<boolean>> {
    return this.httpGet<WrapperVM<boolean>>(`isCourseMemberAdmin/${courseMemberId}`);
  }

  /**
   * is the currently logged-in user admin of the selected course test?
   * @param testId identifier of the test
   */
  public isCourseTestAdmin(testId: string): Observable<WrapperVM<boolean>> {
    return this.httpGet<WrapperVM<boolean>>(`isCourseTestAdmin/${testId}`);
  }

  /**
   * get id of currently logged-in user
   */
  public getCurrentUserId(): Observable<WrapperVM<string>> {
    return this.httpGet<WrapperVM<string>>('getId');
  }
}
