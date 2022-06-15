import { ACCESS_TOKEN } from './../../Constants';
import { AUTH_PATH, HOME_PATH } from './../../Paths';
import { Router } from '@angular/router';
import { RegisterDTO } from '../../models/RegisterData';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, mapTo, catchError, tap, ReplaySubject } from 'rxjs';
import { of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(private server: HttpClient, private router: Router) { }

  private readonly ACCESS_TOKEN = ACCESS_TOKEN;
  private readonly authPath : string = AUTH_PATH;
  private readonly homePath: string = HOME_PATH;

  private _AuthenticationStatus$ = new BehaviorSubject<boolean>(false);
  AuthenticationStatus$ = this._AuthenticationStatus$.asObservable();

  Authenticate()
  {    
    const headers  = this.GetAuthHeader();
    this.server.get(this.homePath + '/index', {headers: headers}).pipe(
      mapTo(true),
      catchError((error) => of(false)),
      tap((val) => this._AuthenticationStatus$.next(val))
    )
    return !!this.GetToken(this.ACCESS_TOKEN);
  }

  LogIn(userName: string, password: string) : Observable<boolean>
  { 
    return this.server.get<any>(this.authPath + `/login?userName=${userName}&password=${password}`)
    .pipe(
      tap((token) => this.SetToken(this.ACCESS_TOKEN,token.access_token)),
      mapTo(true),
      catchError((error) => of(false)),
      tap((val:boolean) => {
        this._AuthenticationStatus$.next(val)
      })
    )
  }

  LogOut(): Observable<boolean>
  {
    const headers  = this.GetAuthHeader();
    return this.server.get<any>(this.authPath + '/LogOut', {headers : headers})
    .pipe(
      mapTo(true),
      catchError((error) => of(false)),
      tap((val) => {
        console.log(val)
        this._AuthenticationStatus$.next(!val);
        val ? this.DeleteToken(this.ACCESS_TOKEN) : val;
      }),
    )
  }

  Register(data: RegisterDTO) : Observable<boolean>
  {
    return this.server.post<RegisterDTO>(this.authPath + `/register`, data)
    .pipe(
      mapTo(true),
      catchError((error) => of(false))
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

}
