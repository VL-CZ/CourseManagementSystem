import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError, retry} from 'rxjs/operators';
import {ValidationError} from './viewmodels/errorsVM';

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
   * handle failed HTTP response
   * @param response HTTP error response object
   * @private
   */
  private handleError(response: HttpErrorResponse) {
    const errorObject: ValidationError = response;
    const errorMessages: string[] = [];

    const errors = errorObject.error.errors;
    for (const key of Object.keys(errors)) {
      errorMessages.push(errors[key]);
    }

    alert(errorMessages);
    console.log(errorMessages);

    return throwError(response);
  }

  /**
   * process HTTP response
   *
   * if it succeeds, return it, otherwise retry {@link retryCount}-times, then throw an error
   * @param response observable of the response
   * @private
   */
  private processResponse<T>(response: Observable<T>): Observable<T> {
    return response.pipe(
      retry(this.retryCount),
      catchError(this.handleError)
    );
  }

  /**
   * execute HTTP GET request to the given URL
   * @param actionUrl URL of the action (will be added to {@link controllerUrl})
   * @protected
   * @returns An Observable of the HTTPResponse, with a response body in the requested type
   */
  protected httpGet<T>(actionUrl: string): Observable<T> {
    return this.processResponse(this.http.get<T>(this.controllerUrl + actionUrl));
  }

  /**
   * execute HTTP GET request to the given URL where the result is {@link Blob}
   * @param actionUrl URL of the action (will be added to {@link controllerUrl})
   * @protected
   * @returns An Observable of the HTTPResponse, with a {@link Blob} response body
   */
  protected httpGetBlob(actionUrl: string): Observable<Blob> {
    return this.processResponse(this.http.get(this.controllerUrl + actionUrl, {responseType: 'blob'}));
  }

  /**
   * execute HTTP POST request to the given URL
   * @param actionUrl URL of the action (will be added to {@link controllerUrl})
   * @param body HTTP request body
   * @protected
   * @returns An Observable of the HTTPResponse, with a response body in the requested type
   */
  protected httpPost<T>(actionUrl: string, body: any): Observable<T> {
    return this.processResponse(this.http.post<T>(this.controllerUrl + actionUrl, body));
  }

  /**
   * execute HTTP PUT request to the given URL
   * @param actionUrl URL of the action (will be added to {@link controllerUrl})
   * @param body HTTP request body
   * @protected
   * @returns An Observable of the HTTPResponse, with a response body in the requested type
   */
  protected httpPut<T>(actionUrl: string, body: any): Observable<T> {
    return this.processResponse(this.http.put<T>(this.controllerUrl + actionUrl, body));
  }

  /**
   * execute HTTP DELETE request to the given URL
   * @param actionUrl URL of the action (will be added to {@link controllerUrl})
   * @protected
   * @returns An Observable of the response, with the response body of type Object
   */
  protected httpDelete(actionUrl: string): Observable<{}> {
    return this.processResponse(this.http.delete(this.controllerUrl + actionUrl));
  }
}
