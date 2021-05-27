import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ApiService} from './api.service';
import {SubmitTestVM} from './viewmodels/submitTestVM';
import {TestWithSubmissionVM} from './viewmodels/testSubmissionVM';
import {EvaluatedTestSubmissionVM} from './viewmodels/evaluatedTestSubmissionVM';
import {SubmissionAnswerVM} from './viewmodels/testSubmissionAnswerVM';

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
  public submit(submission: SubmitTestVM): Observable<{}> {
    return this.httpPost(`${submission.testSubmissionId}/submit`, submission);
  }

  /**
   * save answers to the test submission
   * @param testSubmissionId identifier of the test submission
   * @param updatedAnswers updated text of the answers
   */
  public saveAnswers(testSubmissionId: string, updatedAnswers: SubmissionAnswerVM[]): Observable<{}> {
    return this.httpPut(`${testSubmissionId}/save`, updatedAnswers);
  }

  /**
   * get new empty submission for given test
   * @param testId id of the test we submit
   */
  public loadSubmission(testId: string): Observable<SubmitTestVM> {
    return this.httpPost<SubmitTestVM>(`load/${testId}`, {});
  }

  /**
   * get test submission by its id
   * @param submissionId id of the test submission
   */
  public getSubmissionById(submissionId: string): Observable<TestWithSubmissionVM> {
    return this.httpGet<TestWithSubmissionVM>(submissionId);
  }

  /**
   * update the given submission - update points and comments of the submission
   * @param submissionId id of the given submission
   * @param evaluatedTestSubmission evaluated and commented test submission
   */
  public updateSubmission(submissionId: string, evaluatedTestSubmission: EvaluatedTestSubmissionVM): Observable<{}> {
    return this.httpPut(submissionId, evaluatedTestSubmission);
  }
}
