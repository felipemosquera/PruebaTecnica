import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthModel} from '../Models/authModel.model';
import { Observable } from 'rxjs';
import { Person } from '../Models/Person.model';
import { User } from '../Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = "http://localhost:62011/api";


  constructor(
    private http:HttpClient
  ) { }

  login(authModel:AuthModel):Observable<any>{
    return this.http.post<any>(`${this.apiUrl}/Auth/login`, authModel);
  }

  addNewPerson(person:Person):Observable<any>{
    return this.http.post<any>(`${this.apiUrl}/Person/addNewPerson`, person);
  }

  AddNewUser(user:any):Observable<any>{
    return this.http.post<any>(`${this.apiUrl}/user/AddNewUser`, user);
  }

  getPersons():Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/Person/getPersons`); 
  }


}
