import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {FileVM} from './viewmodels/fileVM';

@Injectable({
  providedIn: 'root'
})
export class FileService extends ApiService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  /**
   * download the file by its Id
   * @param fileId
   */
  public download(fileId: number): void {
    window.open(this.baseUrl + 'api/file/' + fileId);
  }

  /**
   * upload file into selected course
   * @param file file to upload
   * @param courseId id of the course where to upload it
   * @returns Id and name of the file
   */
  public uploadTo(file: File, courseId: string): Observable<FileVM> {
    const formData: FormData = new FormData();
    formData.append('file', file);

    return this.http.post<FileVM>(this.baseUrl + 'api/file/upload/' + courseId, formData);
  }

  /**
   * delete file by its Id
   * @param fileId
   */
  public delete(fileId: number): Observable<{}> {
    return this.http.delete(this.baseUrl + 'api/file/delete/' + fileId);
  }
}
