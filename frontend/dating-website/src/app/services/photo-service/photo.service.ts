import { Observable } from 'rxjs';
import { PHOTO_PATH } from './../../Paths';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PhotoMain } from 'src/app/models/PhotoMain';
import { PhotoCreate } from 'src/app/models/PhotoCreate';


@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(private server: HttpClient) { }

  private readonly path: string = PHOTO_PATH;

  GetAllPhotos(albumId: string): Observable<PhotoMain[]>
  {
    return this.server.get<PhotoMain[]>([this.path, "all/", albumId].join(""));
  }

  GetPhotoById(photoId: string): Observable<PhotoMain>
  {
    return this.server.get<PhotoMain>([this.path, photoId].join(""));
  }

  CreatePhoto(photo: PhotoCreate): Observable<any>
  {
    return this.server.post<any>(this.path, photo);
  }

  DeletePhoto(id: string): Observable<any>
  {
    return this.server.delete<any>(this.path + id);
  }

  UpdatePhoto(newPhoto: PhotoMain)
  {
    return this.server.put<any>(this.path, newPhoto);
  }

  CheckNameValidity(albumId: string, name: string): Observable<boolean>
  {
    return this.server.post<boolean>(
      this.path + 'check', null, {
        params: {
          name : name,
          albumId : albumId
        }});
  }
}
