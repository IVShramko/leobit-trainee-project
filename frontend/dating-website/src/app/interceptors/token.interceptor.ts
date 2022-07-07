import { TokenManager } from './../managers/TokenManager';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { ACCESS_TOKEN } from '../Constants';

@Injectable()
export class TokenInterceptor implements HttpInterceptor{

  constructor(private tokenManager: TokenManager) {}

  intercept(req: HttpRequest<any>,
      next: HttpHandler): Observable<HttpEvent<any>> {
  
      const token = this.tokenManager.GetToken(ACCESS_TOKEN);

      if(token) {
          req = req.clone( {headers :
          req.headers.set('Authorization', 'Bearer ' + token)
          });
      }

      return next.handle(req);
  }
}
