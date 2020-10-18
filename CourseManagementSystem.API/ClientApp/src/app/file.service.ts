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
   * upload file
   * @param file
   * @returns Id and name of the file
   */
  public upload(file: File): Observable<FileVM> {
    const formData: FormData = new FormData();
    formData.append('file', file);

    return this.http.post<FileVM>(this.baseUrl + 'api/file/upload', formData);
  }

  /**
   * get all files
   */
  public getAll(): Observable<FileVM[]> {
    return this.http.get<FileVM[]>(this.baseUrl + 'api/file');
  }

  /**
   * delete file by its Id
   * @param fileId
   */
  public delete(fileId: number): Observable<{}> {
    return this.http.delete(this.baseUrl + 'api/file/delete/' + fileId);
  }
}
