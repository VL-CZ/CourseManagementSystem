import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CourseInfoVM} from './viewmodels/courseInfoVM';
import {AddCourseVM} from './viewmodels/addCourseVM';

@Injectable({
  providedIn: 'root'
})
export class PeopleService extends ApiService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  /**
   * enroll currently logged-in user to the course with given id
   * @param courseId id of the course to enroll
   */
  public enrollToCourse(courseId: number): Observable<{}> {
    return this.http.post<{}>(this.baseUrl + 'api/people/' + courseId, {});
  }

  /**
   * get all courses, whose member the current user is
   */
  getMemberCourses(): Observable<CourseInfoVM[]> {
    return this.http.get<CourseInfoVM[]>(this.baseUrl + 'api/people/memberCourses');
  }

  /**
   * get all courses, whose admin the current user is
   */
  getManagedCourses(): Observable<CourseInfoVM[]> {
    return this.http.get<CourseInfoVM[]>(this.baseUrl + 'api/people/managedCourses');
  }
}
