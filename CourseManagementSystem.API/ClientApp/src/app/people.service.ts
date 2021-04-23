import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CourseInfoVM} from './viewmodels/courseVM';
import {WrapperVM} from './viewmodels/wrapperVM';

@Injectable({
  providedIn: 'root'
})
export class PeopleService extends ApiService {
  private static controllerName = 'people';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl, PeopleService.controllerName);
  }

  /**
   * enroll currently logged-in user to the course with given id
   * @param courseId id of the course to enroll
   */
  public enrollToCourse(courseId: string): Observable<{}> {
    return this.httpPost(this.controllerUrl + `enroll/${courseId}`, {});
  }

  /**
   * get all courses, whose member the current user is
   */
  public getMemberCourses(): Observable<CourseInfoVM[]> {
    return this.httpGet<CourseInfoVM[]>(this.controllerUrl + 'memberCourses');
  }

  /**
   * get all courses, whose admin the current user is
   */
  public getManagedCourses(): Observable<CourseInfoVM[]> {
    return this.httpGet<CourseInfoVM[]>(this.controllerUrl + 'managedCourses');
  }

  /**
   * get course member Id of current user in selected course
   * @param courseId Id of the selected course
   */
  public getCourseMemberByCourse(courseId: string): Observable<WrapperVM<string>> {
    return this.httpGet<WrapperVM<string>>(this.controllerUrl + `getCourseMember/${courseId}`);
  }
}
