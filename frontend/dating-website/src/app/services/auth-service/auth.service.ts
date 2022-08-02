import { TokenManager } from '../../managers/tokenManager';
import { ACCESS_TOKEN } from '../../constants';
import { AUTH_PATH, HOME_PATH } from '../../paths';
import { RegisterDTO } from '../../models/registerDTO';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, mapTo, catchError, tap } from 'rxjs';
import { of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(private server: HttpClient,
    private tokenManager: TokenManager) { }

  private readonly authPath: string = AUTH_PATH;
  private readonly homePath: string = HOME_PATH;

  private _AuthenticationStatus$ = new BehaviorSubject<boolean>(false);
  AuthenticationStatus$ = this._AuthenticationStatus$.asObservable();

  Authenticate() {
    this.server.get(this.homePath + 'index')
      .pipe(
        mapTo(true),
        catchError((error) => of(false)),
        tap((val) => this._AuthenticationStatus$.next(val)));

    return !!this.tokenManager.GetToken(ACCESS_TOKEN);
  }

  LogIn(userName: string, password: string): Observable<boolean> {
    return this.server.get<any>(this.authPath + `login?userName=${userName}&password=${password}`)
      .pipe(
        tap((response) => this.tokenManager.SetToken(ACCESS_TOKEN, response.access_token)),
        mapTo(true),
        catchError((error) => of(false)),
        tap((val: boolean) => {
          this._AuthenticationStatus$.next(val);
        }));
  }

  LogOut(): Observable<boolean> {
    return this.server.get<any>(this.authPath + 'logout')
      .pipe(
        mapTo(true),
        catchError((error) => of(false)),
        tap((val) => {
          this._AuthenticationStatus$.next(!val);
          val ? this.tokenManager.DeleteToken(ACCESS_TOKEN) : val;
        }));
  }

  Register(data: RegisterDTO): Observable<boolean> {
    return this.server.post<RegisterDTO>(this.authPath + `register`, data)
      .pipe(
        mapTo(true),
        catchError((error) => of(false)));
  }
}
