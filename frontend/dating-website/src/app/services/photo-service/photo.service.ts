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
    return this.server.get<PhotoMain[]>(this.path + albumId);
  }

  CreatePhoto(photo: PhotoCreate): Observable<any>
  {
    return this.server.post<any>(this.path, photo);
  }

  DeletePhoto(id: string): Observable<any>
  {
    return this.server.delete<any>(this.path + id);
  }
}
