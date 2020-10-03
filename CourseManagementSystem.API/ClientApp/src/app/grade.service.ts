import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GradeService extends ApiService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  /**
   * delete the grade, whose id equals `gradeId`
   * @param gradeId
   */
  public delete(gradeId: number): Observable<{}> {
    return this.http.delete(this.baseUrl + 'api/grades/delete/' + gradeId);
  }
}
