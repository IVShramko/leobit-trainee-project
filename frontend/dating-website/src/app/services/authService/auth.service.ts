import { Router } from '@angular/router';
import { RegisterDTO } from './../../models/RegisterData';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(private server: HttpClient, private router: Router) { }

  private readonly ACCESS_TOKEN = "ACCESS_TOKEN";

  private authPath : string = "https://localhost:44362/api/auth";
  private homePath : string = "https://localhost:44362/api/home/index";

  private _isRegistered = new BehaviorSubject<boolean>(false);
  isRegistered = this._isRegistered.asObservable();

  LogIn(userName: string, password: string, callback: Function)
  { 
    this.LogInRequest(userName, password)
    .subscribe(
      (response) => {
        this.SetToken(this.ACCESS_TOKEN, response.access_token)},
      (error: HttpErrorResponse) => {
        if(error.status === 401)
        {

        }
      },
      () => callback());
  }

  LogOut(callback: Function)
  {
    this.LogOutrequest()
    .subscribe(
      () => this.DeleteToken(this.ACCESS_TOKEN),
      (error: HttpErrorResponse) => {
        alert('something went wrong. Try again')},
      () => callback());
  }

  Register(data: any)
  {
    this.RegisterRequest(data).subscribe(
      (response) => {
        this._isRegistered.next(true);
      },
      (error: HttpErrorResponse) => {
        if (error.status === 400) {
          this._isRegistered.next(false);
        }
      }
    );
  }

  private GetToken(name: string) : string
  {
    return localStorage.getItem(name) ?? "";
  }

  private SetToken(name: string, token: string) : void
  {
    localStorage.setItem(name, token);
  }

  private DeleteToken(name: string)
  {
    localStorage.removeItem(name);
  }

  GetAuthHeader(): HttpHeaders
  {
    return new HttpHeaders().append("Authorization", 'Bearer ' + this.GetToken(this.ACCESS_TOKEN));
  }

  AuthRequest()
  {
    return this.server.get<any>(this.homePath, {headers: this.GetAuthHeader()});
  }

  private RegisterRequest(data: any) : Observable<any>
  {
    return this.server.post<RegisterDTO>(this.authPath + `/register`, data);
  }

  private LogInRequest(userName: string, password: string): Observable<any>
  {
    return this.server.get<any>(this.authPath + `/login?userName=${userName}&password=${password}`)
  }

  private LogOutrequest(): Observable<any>
  {
    const headers  = this.GetAuthHeader();
    return this.server.get<any>(this.authPath + '/LogOut', {headers : headers});
  }

}
