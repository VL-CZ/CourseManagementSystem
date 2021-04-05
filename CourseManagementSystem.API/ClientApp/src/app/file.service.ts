import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileService extends ApiService {
  private static controllerName = 'files';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl, FileService.controllerName);
  }

  /**
   * download the file by its Id
   * @param fileId
   */
  public download(fileId: number): void {
    window.open(this.controllerUrl + fileId);
  }

  /**
   * upload file into selected course
   * @param file file to upload
   * @param courseId id of the course where to upload it
   * @returns Id and name of the file
   */
  public uploadTo(file: File, courseId: string): Observable<{}> {
    const formData: FormData = new FormData();
    formData.append('file', file);

    return this.http.post<{}>(this.controllerUrl + `upload/${courseId}`, formData);
  }

  /**
   * delete file by its Id
   * @param fileId
   */
  public delete(fileId: number): Observable<{}> {
    return this.http.delete(this.controllerUrl + `delete/${fileId}`);
  }
}
