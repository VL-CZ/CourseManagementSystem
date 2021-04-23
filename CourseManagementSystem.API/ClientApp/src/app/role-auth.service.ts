import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ApiService} from './api.service';
import {Observable} from 'rxjs';
import {WrapperVM} from './viewmodels/wrapperVM';

@Injectable({
  providedIn: 'root'
})
export class RoleAuthService extends ApiService {
  private static controllerName = 'auth';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl, RoleAuthService.controllerName);
  }

  /**
   * is the currently logged-in user admin?
   */
  public isAdmin(): Observable<WrapperVM<boolean>> {
    return this.httpGet<WrapperVM<boolean>>('isAdmin');
  }

  /**
   * get id of currently logged-in user
   */
  public getCurrentUserId(): Observable<WrapperVM<string>> {
    return this.httpGet<WrapperVM<string>>('getId');
  }
}
