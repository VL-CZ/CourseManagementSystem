import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {CourseTest} from './viewmodels/courseTest';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CourseTestService extends ApiService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  /**
   * get test by Id
   * @param testId
   */
  public getById(testId: string): Observable<CourseTest> {
    return this.http.get<CourseTest>(this.baseUrl + `api/courseTests/${testId}`);
  }

  /**
   * delete test by Id
   * @param testId
   */
  public delete(testId: string): Observable<{}> {
    return this.http.delete(this.baseUrl + `api/courseTests/${testId}`);
  }
}
