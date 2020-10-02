import {Inject, Injectable} from '@angular/core';
import {Person, Student} from './viewmodels/student';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  public getAll(): Person[] {

    this.http.get<Person[]>(this.baseUrl + 'api/students').subscribe(result => {
      return result;
    }, error => console.error(error));
  }
}
