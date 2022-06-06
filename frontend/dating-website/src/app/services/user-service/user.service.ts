import { AuthService } from 'src/app/services/authService/auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserProfile } from 'src/app/models/UserProfile';
import { MainData } from 'src/app/models/MainData';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private server: HttpClient, private authService: AuthService) { }

  private readonly path: string = "https://localhost:44362/api/profile/";

  GetFullProfile(): Observable<UserProfile>
  {
    const headers  = this.authService.GetAuthHeader();
    return this.server.get<UserProfile>(this.path + "full", {headers : headers})
  }

  GetMainProfile(): Observable<MainData>
  {
    const headers  = this.authService.GetAuthHeader();
    return this.server.get<MainData>(this.path + "main", {headers : headers})
  }

  ChangeProfile(newProfile: UserProfile): Observable<any>
  {
    const headers  = this.authService.GetAuthHeader();
    return this.server.post<UserProfile>(this.path + "change", newProfile )
  }
}
