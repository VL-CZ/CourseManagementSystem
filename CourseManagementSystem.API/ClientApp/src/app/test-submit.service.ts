import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ApiService} from './api.service';

@Injectable({
  providedIn: 'root'
})
export class TestSubmitService extends ApiService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  /**
   * submit student's solution to the given test
   * @param testId id of the test that answers belong to
   */
  public submit(testId: number): Observable<{}> {
    return this.http.delete(this.baseUrl + 'api/testSubmissions/' + testId);
  }
}
