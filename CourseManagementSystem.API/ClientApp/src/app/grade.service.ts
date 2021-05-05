import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GradeService extends ApiService {
  private static controllerName = 'grades';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl, GradeService.controllerName);
  }

  /**
   * delete the grade, whose id equals `gradeId`
   * @param gradeId
   */
  public delete(gradeId: string): Observable<{}> {
    return this.httpDelete(`delete/${gradeId}`);
  }
}
