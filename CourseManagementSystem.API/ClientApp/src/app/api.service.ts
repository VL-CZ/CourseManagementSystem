import {HttpClient} from '@angular/common/http';
import {Inject} from '@angular/core';

/**
 * base class for all API services
 */
export abstract class ApiService {

  /**
   * HTTP client
   * @protected
   */
  protected readonly http: HttpClient;

  /**
   * base URL of the app
   * @protected
   */
  protected readonly baseUrl: string;

  protected constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
  }
}
