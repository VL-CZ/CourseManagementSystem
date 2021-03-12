import {HttpClient} from '@angular/common/http';

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
   * url of the controller to fetch data (ends with /)
   * @protected
   */
  protected readonly controllerUrl: string;

  /**
   * create new ApiService
   * @param http http client
   * @param baseUrl base url of the app
   * @param controllerName name of controller that the service fetches data from
   * @protected
   */
  protected constructor(http: HttpClient, baseUrl: string, controllerName: string) {
    this.controllerUrl = baseUrl + `api/${controllerName}/`;
    this.http = http;
  }
}
