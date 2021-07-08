import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CourseAdminService extends ApiService {
  private static controllerName = 'courseAdmins';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl, CourseAdminService.controllerName);
  }

  /**
   * remove admin by its id
   * @param adminId id of the admin to remove
   */
  public removeById(adminId: string): Observable<{}> {
    return this.httpDelete(adminId);
  }
}
