import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EnrollmentRequestService extends ApiService {
  private static controllerName = 'enrollmentRequests';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl, EnrollmentRequestService.controllerName);
  }

  /**
   * approve the request for enrollment
   * @param requestId identifier of the request
   */
  public approve(requestId: string): Observable<{}> {
    return this.httpPost(`approve/${requestId}`, {});
  }

  /**
   * decline the request for enrollment
   * @param requestId identifier of the request
   */
  public decline(requestId: string): Observable<{}> {
    return this.httpPost(`decline/${requestId}`, {});
  }
}
