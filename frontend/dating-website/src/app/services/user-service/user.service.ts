import { PROFILE_PATH } from './../../Paths';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserProfile } from 'src/app/models/UserProfile';
import { MainData } from 'src/app/models/MainData';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private server: HttpClient) { }

  private readonly path: string = PROFILE_PATH;

  GetFullProfile(): Observable<UserProfile>
  {
    return this.server.get<UserProfile>(this.path + "full")
  }

  GetMainProfile(): Observable<MainData>
  {
    return this.server.get<MainData>(this.path + "main")
  }

  ChangeProfile(newProfile: UserProfile): Observable<any>
  {
    return this.server.post<UserProfile>(this.path + "change", newProfile)
  }

  SetProfileAvatar(photoId: string)
  {
    return this.server.post<any>(
      this.path + "avatar", 
      null, 
      {params: {photoId: photoId}});
  }
}
