import { Inject, Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient } from '@angular/common/http';
import { CourseTestVM } from './viewmodels/courseTestVM';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CourseTestService extends ApiService
{
  private static controllerName = 'courseTests';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string)
  {
    super(http, baseUrl, CourseTestService.controllerName);
  }

  /**
   * get test by Id
   * @param testId
   */
  public getById(testId: string): Observable<CourseTestVM>
  {
    return this.http.get<CourseTestVM>(this.controllerUrl + testId);
  }

  /**
   * delete test by Id
   * @param testId
   */
  public delete(testId: string): Observable<{}>
  {
    return this.http.delete(this.controllerUrl + testId);
  }

  /**
   * add new test to the given course
   * @param test test to add
   * @param courseId Id of the course
   */
  public addToCourse(test: CourseTestVM, courseId: string): Observable<CourseTestVM>
  {
    return this.http.post<CourseTestVM>(this.controllerUrl + courseId, test);
  }
}
