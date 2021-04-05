import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {CourseTestVM} from './viewmodels/courseTestVM';
import {Observable} from 'rxjs';
import {TestSubmissionWithUserInfoVM} from './viewmodels/testSubmissionWithUserInfoVM';

@Injectable({
  providedIn: 'root'
})
export class CourseTestService extends ApiService {
  private static controllerName = 'courseTests';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl, CourseTestService.controllerName);
  }

  /**
   * get test by Id
   * @param testId
   */
  public getById(testId: string): Observable<CourseTestVM> {
    return this.http.get<CourseTestVM>(this.controllerUrl + testId);
  }

  /**
   * delete test by Id
   * @param testId
   */
  public delete(testId: string): Observable<{}> {
    return this.http.delete(this.controllerUrl + testId);
  }

  /**
   * add new test to the given course
   * @param test test to add
   * @param courseId Id of the course
   */
  public addToCourse(test: CourseTestVM, courseId: string): Observable<CourseTestVM> {
    return this.http.post<CourseTestVM>(this.controllerUrl + courseId, test);
  }

  /**
   * get all (already submitted) test submissions that belong to this test
   * @param testId id of the test
   */
  public getAllTestSubmissions(testId: string): Observable<TestSubmissionWithUserInfoVM[]> {
    return this.http.get<TestSubmissionWithUserInfoVM[]>(this.controllerUrl + `${testId}/submissions`);
  }

  /**
   * update properties of the test
   * @param testId id of the test that we update
   * @param courseTest new properties of the test
   */
  public updateTest(testId: string, courseTest: CourseTestVM): Observable<{}> {
    return this.http.put(this.controllerUrl + testId, courseTest);
  }

  /**
   * publish the test
   * @param testId id of the test to publish
   */
  public publishTest(testId: string): Observable<{}> {
    return this.http.post(this.controllerUrl + `${testId}/publish`, {});
  }
}
