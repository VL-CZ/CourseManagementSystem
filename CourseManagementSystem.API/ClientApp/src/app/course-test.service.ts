import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {AddCourseTestVM, CourseTestDetailsVM} from './viewmodels/courseTestVM';
import {Observable} from 'rxjs';
import {TestSubmissionWithUserInfoVM} from './viewmodels/testSubmissionVM';

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
  public getById(testId: string): Observable<CourseTestDetailsVM> {
    return this.http.get<CourseTestDetailsVM>(testId);
  }

  /**
   * delete test by Id
   * @param testId
   */
  public delete(testId: string): Observable<{}> {
    return this.http.delete(testId);
  }

  /**
   * add new test to the given course
   * @param testToAdd test to add
   * @param courseId Id of the course
   */
  public addToCourse(testToAdd: AddCourseTestVM, courseId: string): Observable<{}> {
    return this.httpPost(courseId, testToAdd);
  }

  /**
   * get all (already submitted) test submissions that belong to this test
   * @param testId id of the test
   */
  public getAllTestSubmissions(testId: string): Observable<TestSubmissionWithUserInfoVM[]> {
    return this.httpGet<TestSubmissionWithUserInfoVM[]>(`${testId}/submissions`);
  }

  /**
   * update properties of the test
   * @param testId id of the test that we update
   * @param updatedTest new properties of the test
   */
  public updateTest(testId: string, updatedTest: AddCourseTestVM): Observable<{}> {
    return this.httpPut(testId, updatedTest);
  }

  /**
   * publish the test
   * @param testId id of the test to publish
   */
  public publishTest(testId: string): Observable<{}> {
    return this.httpPost(`${testId}/publish`, {});
  }
}
