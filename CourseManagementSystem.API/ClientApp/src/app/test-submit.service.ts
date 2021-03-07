import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ApiService} from './api.service';
import {TestSubmissionVM} from './viewmodels/testSubmissionVM';

@Injectable({
  providedIn: 'root'
})
export class TestSubmitService extends ApiService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  /**
   * submit student's solution
   * @param submission test to submit
   */
  public submit(submission: TestSubmissionVM): Observable<{}> {
    return this.http.post(this.baseUrl + 'api/testSubmissions/', submission);
  }

  /**
   * get new empty submission for given test
   * @param testId id of the test we submit
   */
  public getEmptySubmission(testId: string): Observable<TestSubmissionVM> {
    return this.http.get<TestSubmissionVM>(this.baseUrl + 'api/testSubmissions/' + testId);
  }
}
