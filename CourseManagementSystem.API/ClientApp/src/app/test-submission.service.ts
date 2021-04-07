import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ApiService} from './api.service';
import {SubmitTestVM} from './viewmodels/submitTestVM';
import {TestWithSubmissionVM} from './viewmodels/testSubmissionVM';
import {EvaluatedTestSubmissionVM} from './viewmodels/evaluatedTestSubmissionVM';

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
  public submit(submission: SubmitTestVM): Observable<string> {
    return this.http.post<string>(this.controllerUrl, submission);
  }

  /**
   * get new empty submission for given test
   * @param testId id of the test we submit
   */
  public getEmptySubmission(testId: string): Observable<SubmitTestVM> {
    return this.http.get<SubmitTestVM>(this.controllerUrl + `emptyTest/${testId}`);
  }

  /**
   * get test submission by its id
   * @param submissionId id of the test submission
   */
  public getSubmissionById(submissionId: string): Observable<TestWithSubmissionVM> {
    return this.http.get<TestWithSubmissionVM>(this.controllerUrl + submissionId);
  }

  /**
   * update the given submission - update points and comments of the submission
   * @param submissionId id of the given submission
   * @param evaluatedTestSubmission evaluated and commented test submission
   */
  public updateSubmission(submissionId: string, evaluatedTestSubmission: EvaluatedTestSubmissionVM): Observable<{}> {
    return this.http.put<{}>(this.controllerUrl + submissionId, evaluatedTestSubmission);
  }
}
