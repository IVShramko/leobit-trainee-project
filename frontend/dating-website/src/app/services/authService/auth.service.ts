import { UserService } from './../user/user.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, mapTo, Observable, takeLast, tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpResponse } from '@angular/common/http';
import { HttpErrorResponse } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(private server: HttpClient, private userService: UserService) { }

  private readonly ACCESS_TOKEN = "ACCESS_TOKEN";

  private path : string = "https://localhost:44362/api/auth";

  private _isLoggedIn = new BehaviorSubject<boolean>(false);
  isLoggedIn = this._isLoggedIn.asObservable();

  private _isRegistered = new BehaviorSubject<boolean>(false);
  isRegistered = this._isRegistered.asObservable();


  ValidateToken()
  {
    const token = localStorage.getItem(this.ACCESS_TOKEN);

    if (token !== null) 
    {
      const helper = new JwtHelperService();
      const isExpired = helper.isTokenExpired(token);

      if (!isExpired) {
        this._isLoggedIn.next(true);

        const decodedToken = helper.decodeToken(token);
        this.GetInfoFromAccessToken(decodedToken);
      }else
      {
        this._isLoggedIn.next(false);
      }

    }else
    {
      this._isLoggedIn.next(false);
    }
    
  }

  private GetInfoFromAccessToken(token: any)
  {
    this.userService.user.UserName = token.UserName;
  }

  LogIn(userName: string, password: string)
  {
        this.logInRequest(userName, password).subscribe(
        (response) => {
          localStorage.setItem(this.ACCESS_TOKEN, response.access_token);
          this.ValidateToken();
        },
        (error: HttpErrorResponse) => {
          if(error.status === 401)
          {
            this._isLoggedIn.next(false);
          }
        }
      );
  }

  LogOut()
  {
    this.logOutrequest().subscribe(
      (response) => {
        console.log(response);
        localStorage.removeItem(this.ACCESS_TOKEN);
        this._isLoggedIn.next(false);
      },
      (error: HttpErrorResponse) => {
        alert('something went wrong. Try again')
      }
    );
  }

  Register(userName : string, password: string)
  {
    this.registerRequest(userName, password).subscribe(
      (response) => {
        this._isRegistered.next(true);
      },
      (error: HttpErrorResponse) => {
        if (error.status === 400) {
          this._isRegistered.next(false);
        }
        console.log(error);
      }
    );
  }

  private registerRequest(userName : string, password: string) : Observable<any>
  {
    return this.server.get<string>(this.path + `/register?userName=${userName}&password=${password}`);
  }

  private logInRequest(userName: string, password: string): Observable<any>
  {
    return this.server.get<any>(this.path + `/login?userName=${userName}&password=${password}`);
  }

  private logOutrequest(): Observable<any>
  {
    return this.server.get<any>(this.path + '/LogOut');
  }
}
