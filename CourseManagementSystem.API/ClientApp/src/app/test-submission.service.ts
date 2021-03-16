import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ApiService} from './api.service';
import {TestSubmissionVM} from './viewmodels/testSubmissionVM';
import {TestWithSubmissionVM} from './viewmodels/testWithSubmissionVM';

@Injectable({
  providedIn: 'root'
})
export class TestSubmissionService extends ApiService {
  private static controllerUrl = 'testSubmissions';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl, TestSubmissionService.controllerUrl);
  }

  /**
   * submit student's solution
   * @param submission test to submit
   */
  public submit(submission: TestSubmissionVM): Observable<string> {
    return this.http.post<string>(this.controllerUrl, submission);
  }

  /**
   * get new empty submission for given test
   * @param testId id of the test we submit
   */
  public getEmptySubmission(testId: string): Observable<TestSubmissionVM> {
    return this.http.get<TestSubmissionVM>(this.controllerUrl + `emptyTest/${testId}`);
  }

  /**
   * get test submission by its id
   * @param submissionId id of the test submission
   */
  public getSubmissionById(submissionId: string): Observable<TestWithSubmissionVM> {
    return this.http.get<TestWithSubmissionVM>(this.controllerUrl + submissionId);
  }
}
