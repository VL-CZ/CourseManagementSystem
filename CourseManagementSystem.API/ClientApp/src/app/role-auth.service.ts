import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ApiService} from './api.service';
import {Observable} from 'rxjs';
import {IsAdminVM} from './viewmodels/isAdminVM';
import {PersonIdVM} from './viewmodels/courseMemberVM';

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
  public isAdmin(): Observable<IsAdminVM> {
    return this.http.get<IsAdminVM>(this.controllerUrl + 'isAdmin');
  }

  /**
   * get id of currently logged-in user
   */
  public getCurrentUserId(): Observable<PersonIdVM> {
    return this.http.get<PersonIdVM>(this.controllerUrl + 'getId');
  }
}
