import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError, retry} from 'rxjs/operators';
import {ErrorsVM} from './viewmodels/errorsVM';

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
   * describes how many times to repeat unsuccessful API request
   * @private
   */
  private readonly retryCount: number = 1;

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

  /**
   * handle HTTP response errors
   * @param response HTTP response object
   * @protected
   */
  protected handleError(response: HttpErrorResponse) {
    const errorVM: ErrorsVM = response.error;

    const errors: string[] = [];

    for (let key in errorVM.errors) {
      errors.push(errorVM.errors[key]);
    }

    alert(errors);
    console.log(errors);

    return throwError(response);
  }

  /**
   * execute HTTP GET request to the given URL
   * @param actionUrl URL of the action (will be added to {hostUrl}/{controllerUrl})
   * @protected
   * @returns An Observable of the HTTPResponse, with a response body in the requested type
   */
  protected httpGet<T>(actionUrl: string): Observable<T> {
    return this.http.get<T>(this.controllerUrl + actionUrl).pipe(
      retry(this.retryCount),
      catchError(this.handleError)
    );
  }

  /**
   * execute HTTP POST request to the given URL
   * @param actionUrl URL of the action (will be added to {hostUrl}/{controllerUrl})
   * @param body HTTP request body
   * @protected
   * @returns An Observable of the HTTPResponse, with a response body in the requested type
   */
  protected httpPost<T>(actionUrl: string, body: any): Observable<T> {
    return this.http.post<T>(this.controllerUrl + actionUrl, body).pipe(
      retry(this.retryCount),
      catchError(this.handleError)
    );
  }

  /**
   * execute HTTP PUT request to the given URL
   * @param actionUrl URL of the action (will be added to {hostUrl}/{controllerUrl})
   * @param body HTTP request body
   * @protected
   * @returns An Observable of the HTTPResponse, with a response body in the requested type
   */
  protected httpPut<T>(actionUrl: string, body: any): Observable<T> {
    return this.http.put<T>(this.controllerUrl + actionUrl, body).pipe(
      retry(this.retryCount),
      catchError(this.handleError)
    );
  }

  /**
   * execute HTTP DELETE request to the given URL
   * @param actionUrl URL of the action (will be added to {hostUrl}/{controllerUrl})
   * @protected
   * @returns An Observable of the response, with the response body of type Object
   */
  protected httpDelete(actionUrl: string): Observable<{}> {
    return this.http.delete(this.controllerUrl + actionUrl).pipe(
      retry(this.retryCount),
      catchError(this.handleError)
    );
  }
}
