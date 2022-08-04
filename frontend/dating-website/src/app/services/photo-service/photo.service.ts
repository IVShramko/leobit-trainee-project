import { Observable } from 'rxjs';
import { PHOTO_PATH } from '../../paths';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PhotoMainDTO } from 'src/app/models/photoMainDTO';
import { PhotoCreateDTO } from 'src/app/models/photoCreateDTO';


@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(private server: HttpClient) { }

  private readonly path: string = PHOTO_PATH;

  GetAllPhotos(albumId: string): Observable<PhotoMainDTO[]> {
    return this.server.get<PhotoMainDTO[]>([this.path, "all/", albumId].join(""));
  }

  GetPhotoById(photoId: string): Observable<PhotoMainDTO> {
    return this.server.get<PhotoMainDTO>([this.path, photoId].join(""));
  }

  CreatePhoto(photo: PhotoCreateDTO): Observable<any> {
    return this.server.post<any>(this.path, photo);
  }

  DeletePhoto(id: string): Observable<any> {
    return this.server.delete<any>(this.path + id);
  }

  UpdatePhoto(newPhoto: PhotoMainDTO) {
    return this.server.put<any>(this.path, newPhoto);
  }

  UpdatePhotoDataUrl(newPhoto: PhotoMainDTO) {
    return this.server.put<any>([this.path, 'data'].join(''), newPhoto);
  }

  CheckNameValidity(albumId: string, name: string): Observable<boolean> {
    return this.server.post<boolean>(
      this.path + 'check', null, {
      params: {
        name: name,
        albumId: albumId
      }
    });
  }
}
