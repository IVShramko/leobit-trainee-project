import { PROFILE_PATH } from '../../paths';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProfileFullDTO } from 'src/app/models/profileFullDTO';
import { ProfileMainDTO } from 'src/app/models/profileMainDTO';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private server: HttpClient) { }

  private readonly path: string = PROFILE_PATH;

  GetFullProfile(): Observable<ProfileFullDTO> {
    return this.server.get<ProfileFullDTO>(this.path + "full")
  }

  GetMainProfile(): Observable<ProfileMainDTO> {
    return this.server.get<ProfileMainDTO>(this.path + "main")
  }

  ChangeProfile(newProfile: ProfileFullDTO): Observable<any> {
    return this.server.post<ProfileFullDTO>(this.path + "change", newProfile)
  }

  SetProfileAvatar(photoId: string) {
    return this.server.post<any>(
      this.path + "avatar",
      null,
      { params: { photoId: photoId } });
  }
}
