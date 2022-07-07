import { Observable } from 'rxjs';
import { PHOTO_PATH } from './../../Paths';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PhotoMain } from 'src/app/models/PhotoMain';

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
}
